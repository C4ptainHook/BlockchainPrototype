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
        IBlockService<BlockModel> blockchain
    )
    {
        return Ok(await blockchain.GetFullChainAsync());
    }

    [HttpPost("mine")]
    public async Task<ActionResult> MineBlock(
        IHttpContextAccessor httpContextAccessor,
        IMinerService miner
    )
    {
        var port = httpContextAccessor.HttpContext?.Request.Host.Port;
        if (port is null)
            return BadRequest();
        await miner.MineBlockAsync(port.ToString()!);
        return Ok();
    }
}
