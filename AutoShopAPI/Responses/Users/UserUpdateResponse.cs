using System;
using System.Collections.Generic;
using AutoShop.Core.Models;

namespace AutoShop.API.Responses.Users
{
    public class UserUpdateResponse
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserRole { get; set; }

        public List<Car> Cars { get; set; }
    }
}
