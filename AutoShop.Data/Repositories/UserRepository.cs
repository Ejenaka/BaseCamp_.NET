using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutoShop.Core;
using AutoShop.Data;
using Microsoft.EntityFrameworkCore;
using AutoShop.Core.Interfaces;
using AutoShop.Core.Models;

namespace AutoShop.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AutoShopContext context)
            : base(context)
        {
        }

        //public override async Task<User> Get(int id)
        //{
        //    var foundUsers = await FindByCondition(user => user.ID == id);

        //    return foundUsers.FirstOrDefault();
        //}
    }
}
