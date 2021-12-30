using System.Collections.Generic;
using AutoMapper;
using Groot.API.Infrastructure;
using Groot.Model;
using Groot.Model.Product;
using Groot.Service.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Groot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [ServiceFilter(typeof(LoginFilter))]
    public class ProductController : BaseController
    {
        //12.12.2021
        //product will be listed but we look at the user information on cache.
        //then we will list that products who owned that product.
        //do not need to get user information again for getting products.
        //After login operation, we will write filter to follow that user.
        
        private readonly IProductService productService;
        private readonly IMapper mapper; 

        public ProductController(IProductService _productService, IMapper _mapper, IMemoryCache _memoryCache) :base(_memoryCache)
        {
            productService = _productService;
            mapper = _mapper; 
        }

        
        [HttpGet]

        public IActionResult GetProducts()
        {  
           return Ok(productService.GetProducts());
        }

        [HttpGet("{id}")]

        public IActionResult GetProductById(int id)
        {
            return Ok(productService.GetProductById(id));
        }

        [HttpPost]//Post

        public IActionResult Insert([FromBody] Groot.Model.Product.InsertProductViewModel newProduct)
        {
            if(CurrentUser.UserRole==UserRole.Admin)
                return Ok(productService.Insert(newProduct));// added new product
            return BadRequest();
        }

        [HttpPut]// Put

        public IActionResult UpdateProduct([FromBody] Model.Product.DetailedProductViewModel product)
        {
            if (CurrentUser.UserRole == UserRole.Admin)
                return Ok(productService.UpdateProduct(product));
            return BadRequest();
        }

        [HttpDelete("{id}")]//Delete

        public IActionResult DeleteProduct(int id)
        {
            return Ok(productService.DeleteProduct(id));
        }
    }
}