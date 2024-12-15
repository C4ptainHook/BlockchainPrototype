using Blockchain.Api.DTOs;
using Blockchain.Business.Decorators;
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
    private readonly IMapper<TransactionDto, TransactionModel> _mapper;

    public TransactionController(IMapper<TransactionDto, TransactionModel> mapper) =>
        _mapper = mapper;

    [HttpPost("new")]
    public async Task<IActionResult> AddTransaction(
        [FromServices] TransactionServiceMappingDecorator transactionService,
        [FromBody] TransactionDto transaction
    )
    {
        var transactionModel = _mapper.Map(transaction);
        await transactionService.AddAsync(transactionModel);
        return Ok();
    }

    [HttpGet("mempool")]
    public async Task<IActionResult> GetMempool(
        [FromServices] TransactionServiceMappingDecorator transactionService
    )
    {
        var transactions = await transactionService.GetAttachedToTheBlock();
        return Ok(_mapper.Map(transactions));
    }
}
