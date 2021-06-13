using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShop.Core.Enums;

namespace AutoShop.Core.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserRoleEnum Role { get; set; }

        public List<Car> Cars { get; set; }

        public User()
        {
            CreatedDate = DateTime.UtcNow;
            Cars = new List<Car>();
        }
    }
}
