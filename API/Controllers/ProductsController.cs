using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.IRepository;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<Brand> _brandRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo,
        IGenericRepository<Brand> brandRepo, IGenericRepository<Category> categoryRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductReturnDto>>> Getproducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductWithCateAndBrandSpecification(productParams);
            var countSpec = new ProductWithFilterForCountSpecification(productParams);
            var totalItems = await _productRepo.CountAsync(spec);
            var products = await _productRepo.ListWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnDto>>(products); 
            return Ok(new Pagination<ProductReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithCateAndBrandSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<ProductReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}