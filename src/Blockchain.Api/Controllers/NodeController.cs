using Blockchain.Business.Interfaces.Network;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NodeController : Controller
{
    [HttpPost("register")]
    public IActionResult RegisterNode(INodeService nodeService)
    {
        nodeService.RegisterNode(Request.Host.Value);
        return Created();
    }
}
