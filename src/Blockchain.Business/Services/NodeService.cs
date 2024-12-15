using System.Net.Http.Json;
using Blockchain.Business.Caching;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Interfaces.Network;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Models;
using Blockchain.Business.Resources;

namespace Blockchain.Business.Services;

public class NodeService : INodeService
{
    private readonly IBlockService<BlockModel> _blockService;
    private readonly BlockCachingService _blockCachingService;
    private readonly ITransactionService _transactionService;
    private readonly IProofOfWorkService _proofOfWorkService;

    private class ApiResponse
    {
        public List<BlockModel> Item1 { get; set; } = new(); // Default to an empty list
        public int Item2 { get; set; }
    }

    public NodeService(
        IBlockService<BlockModel> blockService,
        BlockCachingService blockCachingService,
        ITransactionService transactionService,
        IProofOfWorkServiceFactory<ProofOfWorkServiceArgsModel> proofOfWorkService
    )
    {
        _blockService = blockService;
        _blockCachingService = blockCachingService;
        _blockCachingService.Length = _blockService.GetChainLength().Result;
        _transactionService = transactionService;
        var proofOfWorkArgs = new ProofOfWorkServiceArgsModel(
            TBConfig.DD,
            int.Parse(TBConfig.MMYYYY)
        );
        _proofOfWorkService = proofOfWorkService.CreateProofOfWork(proofOfWorkArgs);
    }

    public void RegisterNode(string address)
    {
        _blockCachingService.Nodes.Add(address);
    }

    public async Task<bool> ResolveAsync()
    {
        var neighbours = _blockCachingService
            .Nodes.Select(node => new Uri(
                $"http://localhost:{node}/api/v1.0/blockchain/localchain"
            ))
            .ToList();
        var localchainData = await _blockCachingService.GetLocalChainAsync();
        var localChain = localchainData.Item1;
        var newChain = new List<BlockModel>();
        var maxLength = localchainData.Item2;
        var replaced = false;
        using var httpClient = new HttpClient();

        foreach (var neighbour in neighbours)
        {
            var response = await httpClient.GetAsync(neighbour);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<ApiResponse>();
                var chain = responseData.Item1;
                var length = responseData.Item2;

                if (chain.Count() > localChain.Count() && _proofOfWorkService.IsChainValid(chain))
                {
                    maxLength = length;
                    newChain = chain;
                    replaced = true;
                }
            }
        }

        if (replaced)
        {
            var intersect = localChain.Intersect(newChain, new BlockEqualityComparer());
            if (!intersect.Any())
            {
                foreach (var block in localChain)
                {
                    var attachedTransactions = await _transactionService.GetAttachedToTheBlock(
                        block
                    );
                    foreach (var transaction in attachedTransactions)
                    {
                        if (transaction.SenderWallet == transaction.RecipientWallet)
                        {
                            await _transactionService.RemoveAsync(transaction);
                            continue;
                        }
                        transaction.BlockId = null;
                        await _transactionService.UpdateAsync(transaction);
                    }
                    await _blockService.RemoveBlockAsync(block);
                }
            }
            _blockCachingService.Blocks = newChain;
            _blockCachingService.Length = maxLength;
        }

        return replaced;
    }
}
