using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Interfaces.Utils;
using Blockchain.Business.Models;
using Blockchain.Business.Services;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.Utils;

public class ProofOfWorkServiceFactory : IProofOfWorkServiceFactory<ProofOfWorkServiceArgs>
{
    private readonly IRandomNumerical<int> _random;
    private readonly ICultureService _cultureService;

    public ProofOfWorkServiceFactory(ICultureService cultureService, IRandomNumerical<int> random)
    {
        _random = random;
        _cultureService = cultureService;
    }

    public IProofOfWorkService CreateProofOfWork(ProofOfWorkServiceArgs args)
    {
        return new ProofOfWorkService(_cultureService, _random, args);
    }
}
