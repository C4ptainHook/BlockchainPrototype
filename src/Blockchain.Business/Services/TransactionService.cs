using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;

namespace Blockchain.Business.Services;

public class TransactionService(
    IUnitOfWork unitOfWork,
    IMapper<TransactionModel, Transaction> transactionMapper,
    IMapper<BlockModel, Block> blockMapper,
    IWalletService walletService
) : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper<TransactionModel, Transaction> _transactionMapper = transactionMapper;
    private readonly IMapper<BlockModel, Block> _blockMapper = blockMapper;
    private readonly IWalletService _walletService = walletService;

    private async Task<TransactionModel> ReplaceWalletNickNamesWithIds(TransactionModel transaction)
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

    private async Task<bool> IsValid(TransactionModel transaction)
    {
        if (string.IsNullOrEmpty(transaction.SenderWallet))
        {
            return await Task.FromResult(true);
        }
        var senderWallet = await _walletService.GetByNickNameAsync(transaction.SenderWallet);
        return await Task.FromResult(senderWallet.Balance >= transaction.Amount);
    }

    public async Task<TransactionModel> AddAsync(TransactionModel transaction)
    {
        if (!await IsValid(transaction))
        {
            throw new InvalidOperationException("Insufficient funds");
        }
        var senderWallet = await _walletService.GetByNickNameAsync(transaction.SenderWallet);
        var recipientWallet = await _walletService.GetByNickNameAsync(transaction.RecipientWallet);
        var transactionEntity = _transactionMapper.Map(
            await ReplaceWalletNickNamesWithIds(transaction)
        );
        transactionEntity = await _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>()
            .AddAsync(transactionEntity);
        senderWallet?.UpdateBalance(-transaction.Amount);
        if (senderWallet is not null)
            await _walletService.UpdateAsync(senderWallet);
        recipientWallet.UpdateBalance(transaction.Amount);
        await _walletService.UpdateAsync(recipientWallet);
        await _unitOfWork.CommitAsync();
        return _transactionMapper.Map(transactionEntity);
    }

    public async Task<IEnumerable<TransactionModel>> GetAttachedToTheBlock(BlockModel block = null!)
    {
        var blockEntity = block is null ? null : _blockMapper.Map(block);
        var transactionEntities = await _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>()
            .GetAttachedToTheBlock(blockEntity);

        return _transactionMapper.Map(transactionEntities);
    }

    public async Task UpdateAsync(TransactionModel transaction)
    {
        _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>()
            .Update(_transactionMapper.Map(transaction));
        await _unitOfWork.CommitAsync();
    }
}
