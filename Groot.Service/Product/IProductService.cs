using System;
using Groot.Model;

namespace Groot.Service.Product
{
    public interface IProductService
    {
        General<Model.Product.DetailedProductViewModel> Insert(Model.Product.InsertProductViewModel newProduct);
        General<Model.Product.ListOfProductViewModel> GetProducts();
        General<Model.Product.DetailedProductViewModel> GetProductById(int id);
        General<Model.Product.DetailedProductViewModel> UpdateProduct(Model.Product.DetailedProductViewModel product);
        General<Model.Product.DetailedProductViewModel> DeleteProduct(int id);
        
    }
}
