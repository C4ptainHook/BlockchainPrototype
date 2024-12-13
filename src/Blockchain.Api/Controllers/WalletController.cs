using Blockchain.Api.DTOs;
using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class WalletController : Controller
{
    private readonly ILogger<WalletController> _logger;
    private readonly IMapper<WalletDto, WalletModel> _mapper;

    public WalletController(
        ILogger<WalletController> logger,
        IMapper<WalletDto, WalletModel> mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("new")]
    public async Task<IActionResult> AddWalletAsync(
        [FromServices] IWalletService walletService,
        [FromQuery] WalletDto wallet
    )
    {
        var walletModel = _mapper.Map(wallet);
        await walletService.AddAsync(walletModel);
        return Ok();
    }

    [HttpGet("balance")]
    public async Task<ActionResult<decimal>> GetBalanceAsync(
        [FromServices] IWalletService walletService,
        [FromQuery(Name = "name")] string walletNickName
    )
    {
        var wallet = await walletService.GetByNickNameAsync(walletNickName);
        return wallet.Balance;
    }
}
