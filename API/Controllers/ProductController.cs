
using API.Controllers;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _ProductRepository;
        private readonly IGenericRepository<ProductBrand> _BrandsRepository;
        private readonly IGenericRepository<ProductType> _TypesRepository;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<ProductType> typesRepo ,
            IGenericRepository<Product> productsRepo,
            IMapper mapper,
            IGenericRepository<ProductBrand> brandsRepo
            )
        {
            _ProductRepository = productsRepo;
            _BrandsRepository = brandsRepo;
            _mapper = mapper;
            _TypesRepository = typesRepo;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProdcutToReturnDto>>> GetAll( [FromQuery] ProductSpecificationParams prodSpec)
        {
        
            var spec = new ProductsWithTypesandBrands(prodSpec);

            // why do we need to get the countspec from the class similar to the class above
            /*
                  made only for getting the count with pagination
             */

            var countSpec = new ProductsWithFilterForCountSpec(prodSpec);

            var count = await _ProductRepository.CountAsync(countSpec);
            var products = await _ProductRepository.ListEntityWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProdcutToReturnDto>>(products);
            return Ok( new Pagination<ProdcutToReturnDto>(count , prodSpec.pageSize , prodSpec.pageIndex , data) );
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetOne(int id)
        {
            var spec = new ProductsWithTypesandBrands(pro => pro.Id == id);
            var product = await _ProductRepository.GetOneEntityWithSpec(spec);
            if (product is null)
            {
                return BadRequest(new { message = "Product Not found" });
            }
            
            return Ok(_mapper.Map<Product , ProdcutToReturnDto>(product));
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {

            return Ok(await _TypesRepository.ListAllAsync());
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {

            return Ok(await _BrandsRepository.ListAllAsync());
        }
    }
}
