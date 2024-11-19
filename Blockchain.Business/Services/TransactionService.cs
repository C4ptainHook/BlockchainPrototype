using System.Runtime.CompilerServices;
using Blockchain.Business.Models;

namespace Blockchain.Business.Services;

public class TransactionService(ICollection<Transaction> transactions){
    private ICollection<Transaction> _transactions = transactions;

    public async Task AddAsync(Transaction transaction){

    }
}