using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Groot.API.Infrastructure;
using Groot.Model;
using Groot.Model.Product;
using Groot.Service.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Groot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        //12.12.2021
        //product will be listed but we look at the user information on cache.
        //then we will list that products who owned that product.
        //do not need to get user information again for getting products.
        //After login operation, we will write filter to follow that user.
        private readonly IDistributedCache distributedCache;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService _productService, IMapper _mapper, IDistributedCache _distributedCache)
        {
            productService = _productService;
            mapper = _mapper;
            distributedCache = _distributedCache;
        }

        [HttpGet]

        public General<List<ListOfProductViewModel>> GetProducts()
        {
            return productService.GetProducts();
        }

        

        [HttpGet("{id}")]

        public General<Groot.Model.Product.DetailedProductViewModel> GetProductById(int id)
        {
            return productService.GetProductById(id);
        }

        [HttpPost]//Post

        public General<Groot.Model.Product.DetailedProductViewModel> Insert([FromBody] Groot.Model.Product.InsertProductViewModel newProduct)
        {

            return productService.Insert(newProduct);// added new product

        }

        [HttpPut]// Put

        public General<Groot.Model.Product.DetailedProductViewModel> UpdateProduct([FromBody] Model.Product.DetailedProductViewModel product)
        {
            return productService.UpdateProduct(product);
        }

        [HttpDelete("{id}")]//Delete

        public General<Model.Product.DetailedProductViewModel> DeleteProduct(int id)
        {
            return productService.DeleteProduct(id);
        }
    }
}