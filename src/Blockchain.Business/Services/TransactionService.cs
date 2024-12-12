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

    private TransactionModel ReplaceWalletNickNamesWithIds(TransactionModel transaction)
    {
        var senderWallet = _walletService.GetByNickNameAsync(transaction.SenderWallet).Result;
        var recipientWallet = _walletService.GetByNickNameAsync(transaction.RecipientWallet).Result;
        transaction.SenderWallet = senderWallet.Id;
        transaction.RecipientWallet = recipientWallet.Id;
        return transaction;
    }

    public async Task<TransactionModel> AddAsync(TransactionModel transaction)
    {
        var transactionEntity = _transactionMapper.Map(ReplaceWalletNickNamesWithIds(transaction));
        await _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>()
            .AddAsync(transactionEntity);
        await _unitOfWork.CommitAsync();
        return _transactionMapper.Map(transactionEntity);
    }

    public async Task<IEnumerable<TransactionModel>> GetAttachedToTheBlock(BlockModel block = null!)
    {
        var transactionEntities = await _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>()
            .GetAttachedToTheBlock(_blockMapper.Map(block));

        return _transactionMapper.Map(transactionEntities);
    }

    public async Task UpdateAsync(TransactionModel transaction)
    {
        var transactionEntity = _transactionMapper.Map(ReplaceWalletNickNamesWithIds(transaction));
        _unitOfWork.GetRepository<ITransactionRepository<Transaction>>().Update(transactionEntity);
        await _unitOfWork.CommitAsync();
    }
}
