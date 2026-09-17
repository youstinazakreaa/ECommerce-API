using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using onine.Repository.Data;

namespace onineproject.Controllers
{

    public class BuggyController : ApiBaseController

    {
        private readonly Oninecontext _context;

        public BuggyController(Oninecontext context)
        {
            _context = context;
        }

        [HttpGet("NotFound")]
        public async Task<ActionResult> GetNotFoundResult()
        {
            var product = await _context.Products.FindAsync(900);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("ServerError")]

        public ActionResult GetServerError()
        {
            return BadRequest();
        
        }

    }
}
