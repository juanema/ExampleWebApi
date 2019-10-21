using ExampleWebApi.Core;
using ExampleWebApi.Core.Persistence.Repositories;
using ExampleWebApi.Domain.Persistence.Context;
using System;
using System.Threading.Tasks;

namespace ExampleWebApi.Domain.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            Guard.Against<ArgumentNullException>(context == null, $"Parameter {nameof(context)} is null");
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
