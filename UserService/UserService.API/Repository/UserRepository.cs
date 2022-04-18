using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using UserService.API.Models;

namespace UserService.API.Repository
{
    public class UserRepository : IUserRepository
    {
        IUserContext _context;
        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public bool DeleteUser(string userId)
        {
            return _context.Users.DeleteOne(x=>x.UserId ==  userId).IsAcknowledged;
        }

        public User GetUserById(string userId)
        {
            var users = _context.Users.Find(x => x.UserId == userId);
            return users.FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return _context.Users.Find(_ => true).ToList();
        }

        public User RegisterUser(User user)
        {
            _context.Users.InsertOne(user);
            return user;
        }

        public bool UpdateUser(string userId, User user)
        {
            var model = Builders<User>.Update
                .Set(x => x.Name, user.Name)
                .Set(x => x.Contact, user.Contact)                
                .Set(x => x.AddedDate, user.AddedDate);
            var updateOptons = new UpdateOptions { IsUpsert = true };

            var result = _context.Users.UpdateOne(x => x.UserId == user.UserId, model, updateOptons);
            return result.IsAcknowledged;
        }
    }
}
