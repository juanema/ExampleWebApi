using ExampleWebApi.Core;
using ExampleWebApi.Domain.Persistence.Context;
using System;

namespace ExampleWebApi.Domain.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            Guard.Against<ArgumentNullException>(context == null, $"Parameter {nameof(context)} is null");
            _context = context;
        }
    }
}
