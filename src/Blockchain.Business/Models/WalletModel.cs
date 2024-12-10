using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockchain.Business.Models;

public record WalletModel : BaseModel
{
    public string NickName { get; init; }
    public decimal Amount { get; init; }

    public WalletModel(string nickName)
    {
        NickName = nickName;
        Amount = default;
    }
}
