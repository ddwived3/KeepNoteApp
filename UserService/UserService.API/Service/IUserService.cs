﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.API.Models;

namespace UserService.API.Service
{
    public interface IUserService
    {
        User RegisterUser(User user);
        bool UpdateUser(string userId, User user);
        bool DeleteUser(string userId);
        User GetUserById(string userId);
        List<User> GetUsers();
    }
}
