using System;
using System.Collections.Generic;

#nullable disable

namespace Groot.DB.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IdateTime { get; set; }
        public DateTime? UdateTime { get; set; }
        public int Iuser { get; set; }
        public int? Uuser { get; set; }

        public virtual Product Product { get; set; }
    }
}
