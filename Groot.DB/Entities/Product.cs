using System;
using System.Collections.Generic;

#nullable disable

namespace Groot.DB.Entities
{
    public partial class Product
    {
        public Product()
        {
            User = new HashSet<User>();
        }

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
        public string Iuser { get; set; }
        public string? Uuser { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
