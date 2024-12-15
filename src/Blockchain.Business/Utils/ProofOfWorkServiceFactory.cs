using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Interfaces.Utils;
using Blockchain.Business.Models;
using Blockchain.Business.Services;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.Utils;

public class ProofOfWorkServiceFactory : IProofOfWorkServiceFactory<ProofOfWorkServiceArgsModel>
{
    private readonly IRandomNumerical<int> _random;
    private readonly ILogger<IProofOfWorkService> _logger;

    public ProofOfWorkServiceFactory(
        IRandomNumerical<int> random,
        ILogger<IProofOfWorkService> logger
    )
    {
        _random = random;
        _logger = logger;
    }

    public IProofOfWorkService CreateProofOfWork(ProofOfWorkServiceArgsModel args)
    {
        return new ProofOfWorkService(_random, args, _logger);
    }
}
