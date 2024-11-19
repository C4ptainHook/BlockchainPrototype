using Blockchain.Business.Interfaces.Mining;
using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Api.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class Blockchain : Controller
    {
        [HttpGet("chain")]
        public IActionResult GetFullChain()
        {
            throw new NotImplementedException();
        }

        [HttpGet("transactions/new")]
        public IActionResult AddTransaction()
        {
            throw new NotImplementedException();
        }

        [HttpPost("mine")]
        public ActionResult MineBlock(IMiner miner)
        {
            miner.MineBlock();
            return Ok();
        }
    }
}
