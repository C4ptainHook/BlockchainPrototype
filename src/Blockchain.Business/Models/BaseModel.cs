using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockchain.Business.Models;

public abstract record BaseModel
{
    public string Id { get; init; }
}
