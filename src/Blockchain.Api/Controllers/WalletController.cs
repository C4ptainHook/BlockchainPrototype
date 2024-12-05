using Blockchain.Api.DTOs;
using Blockchain.Api.Mappers;
using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Api.Controllers;

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
    public async Task<IActionResult> AddWalletAsync(IWalletService walletService, WalletDto wallet)
    {
        var walletModel = _mapper.Map(wallet);
        await walletService.AddAsync(walletModel);
        return Ok();
    }
}
