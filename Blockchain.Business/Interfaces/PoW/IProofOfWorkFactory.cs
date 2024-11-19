namespace Blockchain.Business.Interfaces.PoW;

public interface IProofOfWorkFactory<ArgType>
    where ArgType : struct
{
    IProofOfWork CreateProofOfWork(ArgType args);
}
