using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Blockchain.Business.Interfaces;
using Blockchain.Business.Models;

namespace Blockchain.Business.Services;

public class TransactionService : ITransactionService
{
    private Queue<Transaction> _transactions = new();
    private static object _lock = new();

    public async Task AddAsync(Transaction transaction)
    {
        await Task.Run(() =>
        {
            lock (_lock)
            {
                _transactions.Enqueue(transaction);
            }
        });
    }

    public IEnumerable<Transaction> Get(int? numberOfTransactions = null)
    {
        lock (_lock)
        {
            if (_transactions.Count == 0)
                throw new InvalidOperationException("Transaction pool is empty");

            numberOfTransactions ??= _transactions.Count;

            if (_transactions.Count < numberOfTransactions)
                throw new ArgumentOutOfRangeException(
                    nameof(numberOfTransactions),
                    "The number of transactions requested exceeds the available transactions."
                );

            for (int i = 0; i < numberOfTransactions; i++)
            {
                yield return _transactions.Dequeue();
            }
        }
    }

    public async Task ClearAsync()
    {
        await Task.Run(() =>
        {
            lock (_lock)
            {
                _transactions.Clear();
            }
        });
    }
}
