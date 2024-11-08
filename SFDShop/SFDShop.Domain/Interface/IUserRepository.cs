﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Domain.Interface
{
    public interface IUserRepository
    {
        public IQueryable<IdentityUser> GetUsers();
        public IdentityUser GetUserByEmail(string email);
        public IdentityUser GetUserById(string Id);
        public List<IdentityUser> GetAdmins();
        public void restrictPermissions(string id);
        public void addPermissions(string Email);
        public void deleteUser(string id);


    }
}
