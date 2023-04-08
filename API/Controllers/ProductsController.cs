

using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interface;
using Core.Specification;
using API.DTOS;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public IGenericRepository<Product> _prodcutRepo; 
        public IGenericRepository<ProductBrand> _productBrandRepo;
        public IGenericRepository<ProductType> _productTypeRepo; 

        public ProductsController(IGenericRepository<Product> prodcutRepo, 
            IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo)
        {
            _prodcutRepo = prodcutRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
               
             var products = await _prodcutRepo.ListAsync(spec);


             return products.Select(product => new ProductToReturnDTO
             {
                Id = product.Id, 
                Name = product.Name, 
                Description = product.Description, 
                PictureUrl = product.PictureUrl, 
                Price = product.Price, 
                productBrand = product.productBrand.Name, 
                ProductType = product.ProductType.Name
             
             }).ToList(); 

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

          return new ProductToReturnDTO 
          {
            Id = product.Id, 
            Name = product.Name, 
            Description = product.Description, 
            PictureUrl = product.PictureUrl, 
            Price = product.Price, 
            productBrand = product.productBrand.Name, 
            ProductType = product.ProductType.Name
          };



        }
     
    }

    
}