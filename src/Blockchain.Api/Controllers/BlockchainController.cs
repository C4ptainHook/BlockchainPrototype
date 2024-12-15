using Blockchain.Api.DTOs;
using Blockchain.Business.Caching;
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

    [HttpGet("fullchain")]
    public async Task<ActionResult<IReadOnlyCollection<BlockModel>>> GetFullChain(
        [FromServices] IBlockService<BlockModel> blockchain
    )
    {
        return Ok(await blockchain.GetFullChainAsync());
    }

    [HttpGet("localchain")]
    public async Task<ActionResult<IReadOnlyCollection<BlockModel>>> GetLocalChain(
        [FromServices] BlockCachingService blockchain
    )
    {
        return Ok(await blockchain.GetLocalChainAsync());
    }

    [HttpPost("mine")]
    public async Task<ActionResult> MineBlock(
        [FromServices] IHttpContextAccessor httpContextAccessor,
        [FromServices] IMinerService miner
    )
    {
        var port = httpContextAccessor.HttpContext?.Request.Host.Port;
        if (port is null)
            return BadRequest();
        await miner.MineBlockAsync(port.ToString()!);
        return Created();
    }
}
