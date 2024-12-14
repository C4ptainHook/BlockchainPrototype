using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Models;

namespace Blockchain.Business.Decorators;

public class TransactionServiceMappingDecorator : ITransactionService
{
    private readonly ITransactionService _transactionService;
    private readonly IWalletService _walletService;

    public TransactionServiceMappingDecorator(
        ITransactionService transactionService,
        IWalletService walletService
    )
    {
        _transactionService = transactionService;
        _walletService = walletService;
    }

    public async Task<TransactionModel> ReplaceWalletNickNamesWithIds(TransactionModel transaction)
    {
        transaction.SenderWallet = string.IsNullOrEmpty(transaction.SenderWallet)
            ? transaction.RecipientWallet
            : transaction.SenderWallet;
        var senderWallet = await _walletService.GetByNickNameAsync(transaction.SenderWallet);
        var recipientWallet = await _walletService.GetByNickNameAsync(transaction.RecipientWallet);
        transaction.SenderWallet = senderWallet.Id;
        transaction.RecipientWallet = recipientWallet.Id;
        return transaction;
    }

    public async Task<IEnumerable<TransactionModel>> ReplaceWalletIdsWithNickNames(
        IEnumerable<TransactionModel> transactions
    )
    {
        var transactionsList = transactions.ToList();
        foreach (var transaction in transactionsList)
        {
            var senderWallet = await _walletService.GetByIdAsync(transaction.SenderWallet);
            var recipientWallet = await _walletService.GetByIdAsync(transaction.RecipientWallet);
            transaction.SenderWallet = senderWallet.NickName;
            transaction.RecipientWallet = recipientWallet.NickName;
        }
        return transactionsList;
    }

    public async Task<TransactionModel> AddAsync(TransactionModel transaction)
    {
        transaction = await ReplaceWalletNickNamesWithIds(transaction);
        return await _transactionService.AddAsync(transaction);
    }

    public async Task<IEnumerable<TransactionModel>> GetAttachedToTheBlock(BlockModel block = null)
    {
        var transactions = await _transactionService.GetAttachedToTheBlock(block);
        transactions = await ReplaceWalletIdsWithNickNames(transactions);
        return transactions;
    }

    public async Task UpdateAsync(TransactionModel transaction)
    {
        await _transactionService.UpdateAsync(transaction);
    }
}
