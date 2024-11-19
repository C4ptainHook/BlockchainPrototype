using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Business.ProofOfWork.Factories
{
    public interface IProofOfWorkFactory<ArgType> where ArgType : struct
    {
        IProofOfWork CreateProofOfWork(ArgType args);
    }
}
