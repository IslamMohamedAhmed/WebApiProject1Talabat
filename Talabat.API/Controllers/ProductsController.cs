using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Talabat.API.DTOS;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.API.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IGenericRepository<Product> genericRepository;
        private readonly IMapper mapper;
        private readonly IGenericRepository<ProductType> types;
        private readonly IGenericRepository<ProductBrand> brands;

        public ProductsController(IGenericRepository<Product> genericRepository, IMapper mapper, IGenericRepository<ProductType> types,IGenericRepository<ProductBrand> brands)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.types = types;
            this.brands = brands;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams Params)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(Params);
            var products = await genericRepository.GetAllWithSpecAsync(Spec);
            var MappedProduct = mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            //var products = await genericRepository.GetAllAsync();
            //OkObjectResult res = new OkObjectResult(products);
            //return res;
            return Ok(MappedProduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var res = await genericRepository.GetByIdWithSpecAsync(Spec);
            if (res is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var MappedProduct = mapper.Map<Product, ProductToReturnDto>(res);
            //var res = await genericRepository.GetByIdAsync(id);
            return Ok(MappedProduct);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var type = await types.GetAllAsync();
            return Ok(type);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brand = await brands.GetAllAsync();
            return Ok(brand);
        }
    }
}
