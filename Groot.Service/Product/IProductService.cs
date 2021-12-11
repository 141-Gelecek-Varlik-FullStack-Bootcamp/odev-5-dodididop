using System;
using Groot.Model;

namespace Groot.Service.Product
{
    public interface IProductService
    {
        General<Model.Product.ProductViewModel> Insert(Model.Product.ProductViewModel newProduct);
        General<Model.Product.ProductViewModel> GetProducts();
        General<Model.Product.ProductViewModel> UpdateProduct(int id, Model.Product.ProductViewModel product);
        General<Model.Product.ProductViewModel> DeleteProduct(int id);
    }
}
