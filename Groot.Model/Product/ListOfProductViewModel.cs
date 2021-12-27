using System;
namespace Groot.Model.Product
{
    public class ListOfProductViewModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
      
    }
}
