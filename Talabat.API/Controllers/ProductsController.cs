using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.API.Controllers
{
    
    public class ProductsController : ApiBaseController
    {
        private readonly IGenericRepository<Product> genericRepository;

        public ProductsController(IGenericRepository<Product> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecifications();
            var products = await genericRepository.GetAllWithSpecAsync(Spec);
            //var products = await genericRepository.GetAllAsync();
            //OkObjectResult res = new OkObjectResult(products);
            //return res;
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var res = await genericRepository.GetByIdWithSpecAsync(Spec);
            //var res = await genericRepository.GetByIdAsync(id);
            return Ok(res);
        }
    }
}
