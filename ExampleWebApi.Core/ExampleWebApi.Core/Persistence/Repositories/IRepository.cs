using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleWebApi.Core.Domain.Models;

namespace ExampleWebApi.Core.Persistence.Repositories
{
    /// <summary>
    /// Repository pattern interface
    /// </summary>
    public interface IRepository<T, TId> where T : class, IEntityWithTypedId<TId>
    {
        /// <summary>
        /// Reference to the unit of work the repository
        /// is running in
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Retrieve all elements
        /// </summary>        
        Task<IEnumerable<T>> ListAsync();

        /// <summary>
        /// Add element
        /// </summary>
        /// <param name="element">Element</param>        
        Task AddAsync(T element);

        /// <summary>
        /// Search by primary key
        /// </summary>
        /// <param name="id">Primary key</param>        
        Task<T> FindByIdAsync(TId id);

        /// <summary>
        /// Update element
        /// </summary>
        /// <param name="element">Element</param>
        void Update(T element);

        /// <summary>
        /// Delete element
        /// </summary>
        /// <param name="element">Element</param>
        void Remove(T element);
    }

    /// <summary>
    /// Most of the entities have a surrogate key, so
    /// most of the repositories will be have a long 
    /// identifier
    /// </summary>
    public interface IRepository<T> : IRepository<T, int> where T : class, IEntityWithTypedId<int> { }
}
