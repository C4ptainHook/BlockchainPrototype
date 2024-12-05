using Blockchain.Api.DTOs;
using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blockchain.Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class TransactionController : Controller
{
    private readonly ILogger<TransactionController> _logger;
    private readonly IMapper<TransactionDto, TransactionModel> _mapper;

    public TransactionController(
        ILogger<TransactionController> logger,
        IMapper<TransactionDto, TransactionModel> mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("new")]
    public async Task<IActionResult> AddTransaction(
        ITransactionService transactionService,
        TransactionDto transaction
    )
    {
        var transactionModel = _mapper.Map(transaction);
        await transactionService.AddAsync(transactionModel);
        return Ok();
    }
}
