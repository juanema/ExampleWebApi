using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleWebApi.Domain.Communications;
using ExampleWebApi.Domain.Models;
using ExampleWebApi.Domain.Services;
using System.Linq;

namespace ExampleWebApi.WebApiTest
{
    internal class UserServiceFake : IUserService
    {
        private readonly List<User> _database;

        public UserServiceFake()
        {
            _database = new List<User>()
            {
                new ExampleWebApi.Domain.Models.User() { Id = 1, Name = "SpongeBob Square Pants", Birthdate = new System.DateTime(1990, 5, 8) },
                new ExampleWebApi.Domain.Models.User() { Id = 2, Name = "Patrick Star", Birthdate = new System.DateTime(1990, 7, 10) },
                new ExampleWebApi.Domain.Models.User() { Id = 3, Name = "Squidward Q. Tentacles", Birthdate = new System.DateTime(1976, 3, 15) }
            };
        }
        public Task<UserResponse> DeleteAsync(User user)
        {
            UserResponse response;
            if (_database.Remove(user))
            {
                response = new UserResponse(user);
            }
            else
            {
                response = new UserResponse(new List<string>() { "Something was wrong" });
            }
            return Task.FromResult<UserResponse>(response);
        }

        public Task<User> GetAsync(int id)
        {
            User user = _database.Where(q => q.Id == id).FirstOrDefault();

            return Task.FromResult<User>(user);
        }

        public Task<IEnumerable<User>> ListAsync()
        {
            return Task.FromResult<IEnumerable<User>>(_database.ToList());
        }

        public Task<UserResponse> SaveAsync(User user)
        {
            UserResponse response;
            if (_database.Where(q => q.Id == user.Id).FirstOrDefault() == null)
            {
                _database.Add(user);
                response = new UserResponse(user);
            }
            else
            {
                response = new UserResponse(new List<string>() { "Something was wrong" });
            }
            return Task.FromResult<UserResponse>(response);
        }

        public Task<UserResponse> UpdateAsync(User user)
        {
            UserResponse response;
            User userDataBase = _database.Where(q => q.Id == user.Id).FirstOrDefault();
            if (user != null)
            {
                _database[_database.IndexOf(userDataBase)] = user;
                response = new UserResponse(user);
            }
            else
            {
                response = new UserResponse(new List<string>() { "Something was wrong" });
            }
            return Task.FromResult<UserResponse>(response);
        }
    }
}