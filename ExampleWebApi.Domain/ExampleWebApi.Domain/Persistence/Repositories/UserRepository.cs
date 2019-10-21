using ExampleWebApi.Core;
using ExampleWebApi.Core.Domain.Models;
using ExampleWebApi.Core.Persistence.Repositories;
using ExampleWebApi.Domain.Models;
using ExampleWebApi.Domain.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleWebApi.Domain.Persistence.Repositories
{
    /// <summary>
    /// Repository implementation
    /// </summary>
    public class UserRepository : BaseRepository, IRepository<User, int>
    {

        private readonly IUnitOfWork _unitOfWork;


        public UserRepository(IUnitOfWork unitOfWork, AppDbContext context) : base(context)
        {
            Guard.Against<ArgumentNullException>(unitOfWork == null, "\"unitOfWork\" may not be null");

            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

        public async Task AddAsync(User element)
        {
            await _context.Users.AddAsync(element);           
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Remove(User element)
        {
            _context.Users.Remove(element);
        }

        public void Update(User element)
        {
            _context.Users.Update(element);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
