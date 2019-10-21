using System.ComponentModel.DataAnnotations;

namespace ExampleWebApi.Core.Domain.Models
{
    /// <summary>
	/// Interface contract for entities
	/// </summary>
	/// <typeparam name="TId">Id type</typeparam>
	public interface IEntityWithTypedId<TId>
    {
        /// <summary>
        /// Unique identifier in the data layer
        /// </summary>
        /// <remarks>
        /// Write access is intended for domain assigned
        /// ids, do not use when the id is assignewd in 
        /// the data layer
        /// </remarks>
        TId Id { get; set; }
    }

    /// <summary>
    /// Base class for all entities
    /// </summary>
    /// <typeparam name="TId">
    /// Id may be of type string, int, custom type, etc.
    /// Setter is protected to allow unit tests to set this property via reflection and to allow 
    /// domain objects more flexibility in setting this for those objects with assigned Ids.
    /// </typeparam>
    public abstract class Entity<TId> : IEntityWithTypedId<TId>
    {
        [Key]
        [Required]
        public TId Id { get; set; }
    }
}
