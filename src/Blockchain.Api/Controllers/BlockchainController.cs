using Blockchain.Api.DTOs;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class BlockchainController : Controller
{
    private readonly ILogger<BlockchainController> _logger;

    public BlockchainController(ILogger<BlockchainController> logger)
    {
        _logger = logger;
    }

    [HttpGet("chain")]
    public async Task<ActionResult<IReadOnlyCollection<BlockModel>>> GetFullChain(
        IBlockchainService<BlockModel> blockchain
    )
    {
        return Ok(await blockchain.GetFullChainAsync());
    }

    [HttpPost("mine")]
    public async Task<ActionResult> MineBlock(IMinerService miner, WalletDto wallet)
    {
        await miner.MineBlockAsync(wallet.NickName);
        return Ok();
    }
}
