using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Groot.DB.Entities.DatabaseContext;
using Groot.Model;
using Groot.Model.Product;

namespace Groot.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;

        public ProductService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public General<Model.Product.ProductViewModel> Insert(Model.Product.ProductViewModel newProduct)
        {
            var result = new General<Groot.Model.Product.ProductViewModel>() { IsSuccess = false };
            var model = mapper.Map<Groot.DB.Entities.Product>(newProduct);
            using (var srv = new GrootContext())
            {

                model.IdateTime = DateTime.Now;
                srv.Product.Add(model);
                srv.SaveChanges();
                result.Entity = mapper.Map<Groot.Model.Product.ProductViewModel>(model);
                result.IsSuccess = true;

            }
            return result;
        }
        public General<ProductViewModel> GetProducts()
        {
            var result = new General<ProductViewModel>();
            using (var srv = new GrootContext())
            {
                var data = srv.Product.Where(a => a.IsActive && !a.IsDeleted).OrderBy(a => a.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<ProductViewModel>>(data);
                    result.IsSuccess = true;
                }
                else
                    result.IsSuccess = false;
            }
            return result;
        }

        public General<ProductViewModel> UpdateProduct(int id, ProductViewModel updatedProduct)
        {
            var result = new General<Groot.Model.Product.ProductViewModel>();
            using (var srv = new GrootContext())
            {
                bool isAuthenticated = srv.Product.Any(a => a.IsActive && !a.IsDeleted && a.Id == updatedProduct.Id && a.Iuser == updatedProduct.Iuser);
                var product = srv.Product.SingleOrDefault(a => a.Id == id);

                if (isAuthenticated && product is not null)
                {
                    product.Name = updatedProduct.Name;
                    product.Price = updatedProduct.Price;
                    product.IdateTime = DateTime.Now;
                    product.DisplayName = updatedProduct.DisplayName;
                    product.Stock = updatedProduct.Stock;
                    srv.SaveChanges();
                    result.Entity = mapper.Map<Groot.Model.Product.ProductViewModel>(product);
                    result.IsSuccess = true;
                }
                else
                    result.IsSuccess = false;

                return result;
            }
        }

        public General<Model.Product.ProductViewModel> DeleteProduct(int id)
        {
            var result = new General<Groot.Model.Product.ProductViewModel>() { IsSuccess = false };

            using (var srv = new GrootContext())
            {
                var product = srv.Product.Where(a => a.Id == id).SingleOrDefault();
                srv.Product.Remove(product);
                srv.SaveChanges();
                result.Entity = mapper.Map<ProductViewModel>(product);
                result.IsSuccess = true;
            }
            return result;
        }
    }
}
