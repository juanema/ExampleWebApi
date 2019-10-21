using ExampleWebApi.Core;
using ExampleWebApi.Core.Persistence.Repositories;
using ExampleWebApi.Domain.Communications;
using ExampleWebApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleWebApi.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, int> _userRepository;

        public UserService(IRepository<User, int> userRepository)
        {
            Guard.Against<ArgumentNullException>(userRepository == null, $"Parameter {nameof(userRepository)} is null");
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _userRepository.FindByIdAsync(id);
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _userRepository.UnitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse(new List<string>() { $"An error occurred when saving the user: {ex.Message}" });
            }
        }

        public async Task<UserResponse> UpdateAsync(User user)
        {
            try
            {
                _userRepository.Update(user);
                await _userRepository.UnitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse(new List<string>() { $"An error occurred when update the user: {ex.Message}" });
            }
        }

        public async Task<UserResponse> DeleteAsync(User user)
        {
            try
            {
                _userRepository.Remove(user);
                await _userRepository.UnitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse(new List<string>() { $"An error occurred when delete the user: {ex.Message}" });
            }
        }


    }
}
