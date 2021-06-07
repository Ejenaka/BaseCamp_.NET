using System.Collections.Generic;
using AutoShop.Core.Models;

namespace AutoShop.API.Requests.Users
{
    public class UserUpdateRequest
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
    }
}
