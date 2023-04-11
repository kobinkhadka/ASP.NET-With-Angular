

using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interface;
using Core.Specification;
using API.DTOS;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IGenericRepository<Product> _prodcutRepo; 
        private IGenericRepository<ProductBrand> _productBrandRepo;
        private IGenericRepository<ProductType> _productTypeRepo; 
    
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> prodcutRepo, 
            IGenericRepository<ProductBrand> productBrandRepo, 
            IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _prodcutRepo = prodcutRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
               
             var products = await _prodcutRepo.ListAsync(spec);


             return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products));
             

        }


        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
               
             var productBrands = await _productBrandRepo.ListAllAsync(); 


             return Ok(productBrands); 

        }


       [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
               
             var productTypes = await _productTypeRepo.ListAllAsync(); 


             return Ok(productTypes); 

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
          
          var product = await  _prodcutRepo.GetEntityWithSpec(spec);

          return _mapper.Map<Product, ProductToReturnDTO>(product);
         



        }
     
    }

    
}