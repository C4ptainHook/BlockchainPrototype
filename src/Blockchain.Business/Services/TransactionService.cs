using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;

namespace Blockchain.Business.Services;

public class TransactionService(
    IUnitOfWork unitOfWork,
    IMapper<TransactionModel, Transaction> mapper
) : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper<TransactionModel, Transaction> _mapper = mapper;

    public async Task<TransactionModel> AddAsync(TransactionModel transaction)
    {
        var transactionEntity = _mapper.Map(transaction);
        await _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>()
            .AddAsync(transactionEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map(transactionEntity);
    }

    public async Task<IEnumerable<TransactionModel>> GetAttachedToTheBlock(string blockId = null!)
    {
        var transactionEntities = await _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>()
            .GetAllAsync();
        return _mapper.Map(transactionEntities);
    }

    public async Task UpdateAsync(TransactionModel transaction)
    {
        var transactionEntity = _mapper.Map(transaction);
        _unitOfWork.GetRepository<ITransactionRepository<Transaction>>().Update(transactionEntity);
        await _unitOfWork.CommitAsync();
    }
}
