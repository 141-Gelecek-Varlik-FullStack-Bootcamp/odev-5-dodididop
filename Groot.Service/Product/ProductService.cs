using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Groot.DB.Entities.DatabaseContext;
using Groot.Model;
using Groot.Model.Product;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Groot.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;

        public ProductService(IMapper _mapper, IDistributedCache _distributedCache)
        {
            mapper = _mapper;
            distributedCache = _distributedCache;
        }

        public General<Model.Product.DetailedProductViewModel> Insert(Model.Product.InsertProductViewModel newProduct)
        {
            var response = new General<Groot.Model.Product.DetailedProductViewModel>() { IsSuccess = false };
            var model = mapper.Map<Groot.DB.Entities.Product>(newProduct);
            using (var srv = new GrootContext())
            {
                model.IdateTime = DateTime.Now;
                srv.Product.Add(model);
                srv.SaveChanges();
                response.Entity = mapper.Map<Groot.Model.Product.DetailedProductViewModel>(model);
                response.IsSuccess = true;

            }
            return response;
        }

        public General<DetailedProductViewModel> UpdateProduct(DetailedProductViewModel updatedProduct)
        {
            var response = new General<Groot.Model.Product.DetailedProductViewModel>();
            using (var srv = new GrootContext())
            {
                var product = srv.Product.Find(updatedProduct.Id);

                if (product is not null)
                {
                    mapper.Map(updatedProduct, product);
                    srv.SaveChanges();
                    response.Entity = mapper.Map<DetailedProductViewModel>(product);
                    response.IsSuccess = true;
                }
                else
                {
                    response.ExceptionMessage = "Bir hata oluştu.";
                    response.IsSuccess = false;
                }
                return response;
            }
        }

        public General<List<ListOfProductViewModel>> GetProducts()
        {
            var response = new General<List<ListOfProductViewModel>>();

            try//redis is on.
            {
                if (string.IsNullOrEmpty(distributedCache.GetString("products")))
                {

                    using (var srv = new GrootContext())
                    {
                        var data = srv.Product.Where(a => a.IsActive && !a.IsDeleted).OrderBy(a => a.Id).ToList();
                        if (data.Any())
                        {
                            var productList = JsonConvert.SerializeObject(data);
                            distributedCache.SetString("products", productList);
                            response.IsSuccess = true;
                            response.Entity = mapper.Map<List<ListOfProductViewModel>>(data);
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.ExceptionMessage = "Bir hata oluştu.";
                        }
                    }
                }
                else
                {
                    var data = distributedCache.GetString("products");
                    var obj = JsonConvert.DeserializeObject<List<Groot.DB.Entities.Product>>(data);
                    response.Entity = mapper.Map<List<ListOfProductViewModel>>(obj);
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception ex)//redis is off.
            {
                using (var srv = new GrootContext())
                {
                    var data = srv.Product.Where(a => a.IsActive && !a.IsDeleted).OrderBy(a => a.Id).ToList();
                    if (data.Any())
                    {
                        response.IsSuccess = true;
                        response.Entity = mapper.Map<List<ListOfProductViewModel>>(data);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ExceptionMessage = "Bir hata oluştu.";
                    }
                    return response;
                }
            }
            
            
        }


        public General<DetailedProductViewModel> GetProductById(int id)
        {
            var response = new General<DetailedProductViewModel>();
            using (var srv = new GrootContext())
            {
                var data = srv.Product.Where(a => a.Id == id).FirstOrDefault();

                if (data is not null)
                {
                    response.IsSuccess = true;
                    response.Entity = mapper.Map<DetailedProductViewModel>(data);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ExceptionMessage = "Bir hata oluştu.";
                }
            }
            return response;
        }

        public General<Model.Product.DetailedProductViewModel> DeleteProduct(int id)
        {
            var response = new General<Groot.Model.Product.DetailedProductViewModel>() { IsSuccess = false };
            
            using (var srv = new GrootContext())
            {
                var product = srv.Product.Where(a => a.Id == id).SingleOrDefault();
                srv.Product.Remove(product);
                srv.SaveChanges();
                response.Entity = mapper.Map<DetailedProductViewModel>(product);
                response.IsSuccess = true;
            }
            return response;
        }      
    }
}
