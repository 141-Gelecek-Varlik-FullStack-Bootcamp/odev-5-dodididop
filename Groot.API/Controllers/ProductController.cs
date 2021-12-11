using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Groot.Model;
using Groot.Model.Product;
using Groot.Service.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Groot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductController(IProductService _productService, IMapper _mapper)
        {
            productService = _productService;
            mapper = _mapper;
        }

        [HttpGet]

        public General<Groot.Model.Product.ProductViewModel> GetProducts()
        {
            return productService.GetProducts();
        }

        [HttpPost]//Post

        public General<Groot.Model.Product.ProductViewModel> Insert([FromBody] Groot.Model.Product.ProductViewModel newProduct)
        {

            return productService.Insert(newProduct);// added new product

        }

        [HttpPut("{id}")]// Put

        public General<Groot.Model.Product.ProductViewModel> UpdateProduct(int id, [FromBody] ProductViewModel product)
        {
            return productService.UpdateProduct(id, product);
        }

        [HttpDelete("{id}")]//Delete

        public General<Model.Product.ProductViewModel> DeleteProduct(int id)
        {
            return productService.DeleteProduct(id);
        }
    }
}