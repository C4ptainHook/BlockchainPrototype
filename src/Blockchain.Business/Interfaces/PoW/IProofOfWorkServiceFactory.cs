namespace Blockchain.Business.Interfaces.PoW;

public interface IProofOfWorkServiceFactory<ArgType>
    where ArgType : struct
{
    IProofOfWorkService CreateProofOfWork(ArgType args);
}
