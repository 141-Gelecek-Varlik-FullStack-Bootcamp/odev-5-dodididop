using System;

namespace Groot.Model.Product
{
    public class DetailedProductViewModel
    {
        
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IdateTime { get; set; }
        public DateTime? UdateTime { get; set; }
        public int Iuser { get; set; }
        public int? Uuser { get; set; }

      //  public string CategoryName { get; set; }
    }
}
