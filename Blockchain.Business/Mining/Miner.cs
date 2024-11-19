using System.Text;
using Blockchain.Business.Resources;
using Blockchain.Business.CryptoChain;
using Blockchain.Business.ProofOfWork;
using Blockchain.Business.ProofOfWork.Factories;
using Microsoft.Extensions.Logging;
using Blockchain.Business.Extensions;
namespace Blockchain.Business.Mining;

public class Miner : IMiner
{
    private readonly IProofOfWork _proofOfWork;
    private readonly ILogger<IMiner> _logger;
    public readonly IBlockChain _blockchain;
    public Miner(IBlockChain blockchain, IProofOfWorkFactory<ProofOfWorkArgs> proofOfWorkFactory, ILogger<IMiner> logger)
    {
        _blockchain = blockchain;
        var proofOfWorkArgs = new ProofOfWorkArgs(TBConfig.DD, int.Parse(TBConfig.MMYYYY));
        _proofOfWork = proofOfWorkFactory.CreateProofOfWork(proofOfWorkArgs);
        _logger = logger;
    }
    public Block MineBlock()
    {
        var nonce = int.Parse(TBConfig.DD + TBConfig.MM);
        var minedSuccesfully = false;
        var newBlockIndex = _blockchain.LastIndex + 1;
        var lastBlockHash = _proofOfWork.GetHash(_blockchain.LastBlock) ?? String.Empty;
        var iteration = 0;
        Block newBlock = default!;
        _logger.LogInformation("START mining block {newBlockIndex}", newBlockIndex);
        while (!minedSuccesfully)
        {
            var blockArgs = new BlockArgs(newBlockIndex, DateTime.Now, nonce, lastBlockHash);
            newBlock = new Block(new object(), blockArgs);
            if (_proofOfWork.IsHashValid(_proofOfWork.GetHash(newBlock)!))
            {
                _blockchain.AddBlock(newBlock);
                _logger.LogInformation("Block {newBlockIndex} mined after {iteration} iterations", newBlockIndex, iteration);
                _logger.LogInformation("Proof number: {proof}", nonce);
                var currentHash = _proofOfWork.GetHash(_blockchain.LastBlock) ?? throw new InvalidOperationException("Block could not be mined");
                _logger.LogInformation("Previous hash: ..{previousHash} <-> Current hash: ..{currentHash}",
                    lastBlockHash.GetLastCharacters(5),
                    currentHash.GetLastCharacters(5));
                minedSuccesfully = true;
            }
            nonce = _proofOfWork.GetNewNonce();
            iteration++;
        }
        _logger.LogInformation("+ FINISHED mining block {newBlockIndex}", newBlockIndex);
        return newBlock ?? throw new InvalidOperationException("");
    }
}