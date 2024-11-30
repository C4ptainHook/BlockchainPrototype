using Blockchain.Business.Interfaces;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class BlockchainController : Controller
{
    [HttpGet("chain")]
    public ActionResult<IReadOnlyCollection<Block>> GetFullChain(IBlockChain<Block> blockchain)
    {
        return Ok(blockchain.CheckChain());
    }

    [HttpPost("transactions/new")]
    public async Task<IActionResult> AddTransaction(
        ITransactionService transactionService,
        Transaction transaction
    )
    {
        await transactionService.AddAsync(transaction);
        return Ok();
    }

    [HttpPost("mine")]
    public async Task<ActionResult> MineBlock(IMiner miner)
    {
        await miner.MineBlockAsync();
        return Ok();
    }
}
