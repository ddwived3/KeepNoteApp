using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Repository;

namespace AuthenticationService.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository repository;
        
        public AuthService(IAuthRepository _repository)
        {
            this.repository = _repository;
        }
        
        public User LoginUser(string userId, string password)
        {
            var result = repository.LoginUser(userId, password);
            if(result == null)
            {
                throw new UserNotFoundException($"User with this id {userId} and password {password} does not exist");
            }

            return result;
        }

        public bool RegisterUser(User user)
        {
            var result = repository.RegisterUser(user);
            if (!result)
            {
                throw new UserNotCreatedException($"User with this id {user.UserId} already exists");
            }
            return result;
        }
    }
}
