using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockchain.Business.Models;

public record WalletModel : BaseModel
{
    public string NickName { get; init; }
    public decimal Balance { get; private set; }

    public WalletModel(string nickName)
    {
        NickName = nickName;
        Balance = default;
    }
    public void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }
}
