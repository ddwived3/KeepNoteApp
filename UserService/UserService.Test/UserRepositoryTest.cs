using System;
using System.Collections.Generic;
using System.Text;
using UserService.API.Models;
using UserService.API.Repository;
using Xunit;

namespace UserService.Test
{
    public class UserRepositoryTest:IClassFixture<DatabaseFixture>
    {
        private readonly IUserRepository repository;
        private readonly DatabaseFixture fixture;

        public UserRepositoryTest(DatabaseFixture _fixture)
        {
            fixture = _fixture;
            repository = new UserRepository(fixture.context);
        }

        [Fact]
        public void RegisterUserShouldReturnUser()
        {
            User user = new User {UserId="Nishant", Name="Nishant", Contact="9892134560", AddedDate=new DateTime() };
            Moq.Mock<IUserContext> context = new Moq.Mock<IUserContext>();
            var actual = repository.RegisterUser(user);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<User>(actual);
        }

        [Fact]
        public void DeleteUserShouldReturnTrue()
        {
            string userId = "Sachin";

            var actual = repository.DeleteUser(userId);

            Assert.True(actual);
        }

        [Fact]

        public void UpdateUserShouldReturnTrue()
        {
            User user = new User { UserId = "Mukesh", Name = "Mukesh", Contact = "9822445566", AddedDate = new DateTime() };

            var actual = repository.UpdateUser("Mukesh", user);

            Assert.True(actual);
        }

        [Fact]
        public void GetUserByIdShouldReturnUser()
        {
            string userId = "Mukesh";

            var actual = repository.GetUserById(userId);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<User>(actual);
        }
    }
}
