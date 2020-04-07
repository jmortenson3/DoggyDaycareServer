using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Common
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds single entity to the collection.
        /// </summary>
        /// <returns>returns the ID of the created entity.</returns>
        public string Add(T entity);

        /// <summary>
        /// Returns a single entity with matching id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T entity</returns>
        public T Find(string id);

        /// <summary>
        /// Updates (replaces) the entity with the matching entity.Id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Update(T entity);

    }
}
