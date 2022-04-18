using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        IAuthenticationContext context;
        public AuthRepository(IAuthenticationContext context)
        {
            this.context = context;
        }
        public User FindUserById(string userId)
        {
            return context.Users.FirstOrDefault(x => x.UserId == userId);
        }

        public User LoginUser(string userId, string password)
        {
            return context.Users.FirstOrDefault(x => x.UserId == userId && x.Password == password );
        }

        public bool RegisterUser(User user)
        {
            context.Users.Add(user);
            int i = context.SaveChanges();
            return i > 0;
        }
    }
}
