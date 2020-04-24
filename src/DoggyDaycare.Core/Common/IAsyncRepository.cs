using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoggyDaycare.Core.Common
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Adds single entity to the collection.
        /// </summary>
        /// <returns>returns the ID of the created entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Returns a single entity with matching id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T entity</returns>
        Task<T> FindAsync(string id);

        /// <summary>
        /// Updates (replaces) the entity with the matching entity.Id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Returns all T based on condition of predicate match.
        /// </summary>
        /// <param name="match"></param>
        /// <returns>A list of matching entities.</returns>
        Task<List<T>> FindAllAsync(Predicate<T> match);
    }
}
