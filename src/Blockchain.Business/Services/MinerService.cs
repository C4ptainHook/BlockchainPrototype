using Blockchain.Business.Extensions;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Models;
using Blockchain.Business.Resources;
using Microsoft.Extensions.Logging;

namespace Blockchain.Business.Services;

public class MinerService : IMinerService
{
    private readonly IProofOfWorkService _proofOfWork;
    private readonly ILogger<IMinerService> _logger;
    private readonly IBlockService<BlockModel> _blockchainService;
    private readonly ITransactionService _transactionService;
    private readonly ITransactionHashingService _transactionHashingService;
    private readonly IWalletService _walletService;
    private readonly Func<int, decimal> _getReward;

    public MinerService(
        IBlockService<BlockModel> blockchainService,
        IProofOfWorkServiceFactory<ProofOfWorkServiceArgsModel> proofOfWorkFactory,
        ITransactionService transactionService,
        ITransactionHashingService transactionHashingService,
        IWalletService walletService,
        ILogger<IMinerService> logger
    )
    {
        _blockchainService = blockchainService;
        _transactionService = transactionService;
        _transactionHashingService = transactionHashingService;
        _walletService = walletService;
        var proofOfWorkArgs = new ProofOfWorkServiceArgsModel(
            TBConfig.DD,
            int.Parse(TBConfig.MMYYYY)
        );
        _proofOfWork = proofOfWorkFactory.CreateProofOfWork(proofOfWorkArgs);
        _logger = logger;
        _getReward = CalculateReward(int.Parse(TBConfig.YYYY));
    }

    public async Task<BlockModel> MineBlockAsync(string walletNickName)
    {
        return await Task.Run(async () =>
        {
            var nonce = int.Parse(TBConfig.DD + TBConfig.MM);
            var minedSuccesfully = false;
            BlockModel? lastBlock = await _blockchainService.GetLastBlockAsync()!;
            var newBlockIndex = await _blockchainService.GetChainLength();
            var lastBlockHash = _proofOfWork.GetHash(lastBlock) ?? string.Empty;
            var iteration = 0;
            var reward = _getReward(newBlockIndex);
            BlockModel newBlock = default!;
            var wallet = await _walletService.GetByNickNameAsync(walletNickName);
            var coinbaseTransaction = await _transactionService.AddAsync(
                new TransactionModel(string.Empty, walletNickName, reward)
            );
            var mempool = new List<TransactionModel>() { coinbaseTransaction };
            var freeTransactions = await _transactionService.GetAttachedToTheBlock();
            mempool.AddRange(freeTransactions);

            _logger.LogInformation("START mining block {newBlockIndex}", newBlockIndex);
            while (!minedSuccesfully)
            {
                var blockArgs = new BlockArgsModel(
                    newBlockIndex,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    nonce,
                    lastBlockHash
                );

                newBlock = new BlockModel(
                    blockArgs,
                    _transactionHashingService.GetMerkleRoot(mempool)
                );
                if (_proofOfWork.IsHashValid(_proofOfWork.GetHash(newBlock)!))
                {
                    newBlock = await _blockchainService.AddBlockAsync(newBlock);
                    _logger.LogInformation(
                        "Block {newBlockIndex} mined after {iteration} iterations",
                        newBlockIndex,
                        iteration
                    );
                    _logger.LogInformation("Proof number: {proof}", nonce);
                    var currentHash =
                        _proofOfWork.GetHash(newBlock)
                        ?? throw new InvalidOperationException("Block could not be mined");
                    _logger.LogInformation(
                        "Previous hash: ..{previousHash} <-> Current hash: ..{currentHash}",
                        lastBlockHash.GetLastCharacters(5),
                        currentHash.GetLastCharacters(5)
                    );
                    minedSuccesfully = true;
                    foreach (var transaction in mempool)
                    {
                        transaction.BlockId = newBlock.Id;
                        await _transactionService.UpdateAsync(transaction);
                    }
                    wallet.UpdateBalance(reward);
                    await _walletService.UpdateAsync(wallet);
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
