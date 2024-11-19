using Blockchain.Business.Interfaces;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Models;
using Blockchain.Business.Services;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.Utils;

public class BasicProofOfWorkFactory : IProofOfWorkFactory<ProofOfWorkArgs>
{
    private readonly IRandomNumerical<int> _random;
    private readonly ILogger<IProofOfWork> _logger;

    public BasicProofOfWorkFactory(IRandomNumerical<int> random, ILogger<IProofOfWork> logger)
    {
        _random = random;
        _logger = logger;
    }

    public IProofOfWork CreateProofOfWork(ProofOfWorkArgs args)
    {
        return new BasicProofOfWork(_random, args, _logger);
    }
}
