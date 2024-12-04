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
    public ActionResult<IReadOnlyCollection<Block>> GetFullChain(
        IBlockchainService<Block> blockchain
    )
    {
        return Ok(blockchain.CheckChain());
    }

    [HttpPost("mine")]
    public async Task<ActionResult> MineBlock(IMinerService miner)
    {
        await miner.MineBlockAsync();
        return Ok();
    }
}
