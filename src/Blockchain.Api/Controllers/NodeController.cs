using Blockchain.Business.Interfaces.Network;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class NodeController : Controller
{
    [HttpPost("register")]
    public IActionResult RegisterNode(INodeService nodeService, [FromQuery] string node)
    {
        nodeService.RegisterNode(node);
        return Created();
    }

    [HttpGet("resolve")]
    public async Task<IActionResult> ResolveConflictsAsync(INodeService nodeService)
    {
        var replaced = await nodeService.ResolveAsync();
        return Ok(replaced);
    }
}
