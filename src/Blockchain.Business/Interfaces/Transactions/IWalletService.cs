using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Business.Models;

namespace Blockchain.Business.Interfaces.Transactions;

public interface IWalletService
{
    Task AddAsync(WalletModel wallet);
}
