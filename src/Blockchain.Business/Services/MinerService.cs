using Blockchain.Business.Extensions;
using Blockchain.Business.Interfaces;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Models;
using Blockchain.Business.Resources;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.Services;

public class MinerService : IMinerService
{
    private readonly IProofOfWorkService _proofOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<IMinerService> _logger;
    public IBlockchainService<BlockModel> _blockchain { get; }
    private readonly Func<int, decimal> _getReward;

    public MinerService(
        IBlockchainService<BlockModel> blockchain,
        IProofOfWorkServiceFactory<ProofOfWorkServiceArgs> proofOfWorkFactory,
        ITransactionService transactionService,
        ILogger<IMinerService> logger
    )
    {
        _blockchain = blockchain;
        var proofOfWorkArgs = new ProofOfWorkServiceArgs(TBConfig.DD, int.Parse(TBConfig.MMYYYY));
        _proofOfWork = proofOfWorkFactory.CreateProofOfWork(proofOfWorkArgs);
        _transactionService = transactionService;
        _logger = logger;
        _getReward = CalculateReward(int.Parse(TBConfig.YYYY));
    }

    public async Task<BlockModel> MineBlockAsync()
    {
        return await Task.Run(() =>
        {
            var nonce = int.Parse(TBConfig.DD + TBConfig.MM);
            var minedSuccesfully = false;
            var newBlockIndex = _blockchain.LastIndex + 1;
            var lastBlockHash = _proofOfWork.GetHash(_blockchain.LastBlock) ?? string.Empty;
            var iteration = 0;
            var reward = _getReward(_blockchain.Count());
            BlockModel newBlock = default!;
            _logger.LogInformation("START mining block {newBlockIndex}", newBlockIndex);
            while (!minedSuccesfully)
            {
                var blockArgs = new BlockArgs(newBlockIndex, DateTime.Now, nonce, lastBlockHash);
                newBlock = new BlockModel(
                    new object(),
                    blockArgs,
                    [new TransactionModel(0, 0, reward)]
                );
                if (_proofOfWork.IsHashValid(_proofOfWork.GetHash(newBlock)!))
                {
                    _blockchain.AddBlock(newBlock);
                    _logger.LogInformation(
                        "Block {newBlockIndex} mined after {iteration} iterations",
                        newBlockIndex,
                        iteration
                    );
                    _logger.LogInformation("Proof number: {proof}", nonce);
                    var currentHash =
                        _proofOfWork.GetHash(_blockchain.LastBlock)
                        ?? throw new InvalidOperationException("Block could not be mined");
                    _logger.LogInformation(
                        "Previous hash: ..{previousHash} <-> Current hash: ..{currentHash}",
                        lastBlockHash.GetLastCharacters(5),
                        currentHash.GetLastCharacters(5)
                    );
                    minedSuccesfully = true;
                }
                nonce = _proofOfWork.GetNewNonce();
                iteration++;
            }
            _logger.LogInformation("+ FINISHED mining block {newBlockIndex}", newBlockIndex);
            return newBlock ?? throw new InvalidOperationException("");
        });
    }

    public virtual Func<int, decimal> CalculateReward(decimal initialReward)
    {
        var currentReward = initialReward;
        return (int blockchainLength) =>
        {
            var isEven = blockchainLength % 2 == 0;
            if (blockchainLength > 0 && isEven)
            {
                currentReward /= decimal.Parse(TBConfig.MM) + 1;
            }
            return currentReward;
        };
    }
}
