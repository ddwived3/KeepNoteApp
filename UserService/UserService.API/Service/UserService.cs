using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.API.Exceptions;
using UserService.API.Models;
using UserService.API.Repository;

namespace UserService.API.Service
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool DeleteUser(string userId)
        {
            var result = _userRepository.DeleteUser(userId);
            if (!result)
            {
                throw new UserNotFoundException("This user id does not exist");
            }

            return result;
        }

        public User GetUserById(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new UserNotFoundException("This user id does not exist");
            }

            return user;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User RegisterUser(User user)
        {
            var result = _userRepository.RegisterUser(user);
            if (result == null)
            {
                throw new UserNotCreatedException("This user id already exists");
            }

            return result;
        }

        public bool UpdateUser(string userId, User user)
        {
            var result = _userRepository.UpdateUser(userId, user);
            if (!result)
            {
                throw new UserNotFoundException("This user id does not exist");
            }

            return result;
        }
    }
}
