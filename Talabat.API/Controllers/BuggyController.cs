using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.Repository.Data;

namespace Talabat.API.Controllers
{
 
    public class BuggyController : ApiBaseController
    {
        private readonly G01DbContext g01DbContext;

        public BuggyController(G01DbContext g01DbContext) {
            this.g01DbContext = g01DbContext;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = g01DbContext.Products.Find(100);
            if(product is null)
            {
                return NotFound(new ApiResponse(404));
            }
          return Ok(product);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var product = g01DbContext.Products.Find(100);
            var productToReturn = product.ToString(); // error null reference exception
            return Ok(productToReturn);
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok(id);
        }
    }
}
