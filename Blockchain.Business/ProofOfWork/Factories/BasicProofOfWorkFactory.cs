using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockchain.Business.RandomWrappers;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.ProofOfWork.Factories
{
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
}
