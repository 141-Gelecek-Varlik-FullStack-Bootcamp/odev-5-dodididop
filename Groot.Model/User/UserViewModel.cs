using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Groot.Model.User
{
    public class UserViewModel
    { 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
