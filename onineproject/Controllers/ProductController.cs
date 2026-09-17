using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using onine.core.Entites;
using onine.core.Repositories;
using onine.core.Specifications;
using onine.Repository.Data;
using onineproject.DTOs;
using onineproject.Errors;
using onineproject.Helpers;
using System.Net;

namespace onineproject.Controllers
{

    public class ProductController : ApiBaseController
    {
      private readonly IGenericRepository<Product> _productRepo;
private readonly IMapper _mapper;
private readonly IGenericRepository<ProductType> _typeRepo;
private readonly IGenericRepository<ProductBrand> _brandRepo;

public ProductController(
    IGenericRepository<Product> productRepo,
    IMapper mapper,
    IGenericRepository<ProductType> typeRepo,
    IGenericRepository<ProductBrand> brandRepo)
{
    _productRepo = productRepo;
    _mapper = mapper;
    _typeRepo = typeRepo;
    _brandRepo = brandRepo;
}
        //GET ALL
        [Authorize (AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme )]
        [HttpGet]
        public async Task <ActionResult<Pagination<ProductToReturnDTOs>>> GetProducts([FromQuery] ProductSpecParams parameters)
        {
            var spec = new ProductwithBrandandType(parameters);
            var products = await _productRepo.GetAllwithspecAsync(spec);
            var mappedProducts =
                               _mapper.Map< IReadOnlyList<ProductToReturnDTOs>>(products);

            var countSpec = new ProductWithFilterationForCount(parameters);
            var count = await _productRepo.GetCountWithSpecAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDTOs>(
                parameters.PageIndex, parameters.PageSize,count, mappedProducts
                ));

        }
        //GET BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDTOs),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductToReturnDTOs), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task <ActionResult> GetProducts(int id)
        {
            var spec = new ProductwithBrandandType(id);
            var product = await _productRepo.GetByIdwithspecAsync(spec);
            var mappedProduct = _mapper.Map<ProductToReturnDTOs>(product);

            return Ok(product);

        }
        //Get all types

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typeRepo.GetAllwithspecAsync();

            return Ok(types);
        }
        //Get all brands

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllwithspecAsync();

            return Ok(brands);
        }

    }
}
