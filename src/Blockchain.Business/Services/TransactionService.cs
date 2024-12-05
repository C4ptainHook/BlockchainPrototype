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

    public async Task AddAsync(TransactionModel transaction)
    {
        var transactionEntity = _mapper.Map(transaction);
        await _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>($"{nameof(Transaction)}Repository")
            .AddAsync(transactionEntity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<TransactionModel>> Get(int? numberOfTransactions = null)
    {
        var transactionEntities = await _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>($"{nameof(Transaction)}Repository")
            .GetAllAsync();
        return _mapper.Map(transactionEntities);
    }

    public async Task ClearAsync()
    {
        var transactions = await this.Get();
        _unitOfWork
            .GetRepository<ITransactionRepository<Transaction>>($"{nameof(Transaction)}Repository")
            .RemoveRange(_mapper.Map(transactions));
    }
}
