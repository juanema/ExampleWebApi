using ExampleWebApi.Domain.Communications;
using ExampleWebApi.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleWebApi.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<User> GetAsync(int id);
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(User user);
        Task<UserResponse> DeleteAsync(User user);
                
    }
}
