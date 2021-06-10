using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Middleware.Services.Interface
{
    public interface IService<T>
    {
        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="item">Item to be created</param>
        /// <returns></returns>
        void Create(T item);

        /// <summary>
        /// Get an item by its ID
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>The found item or 'null' if there is no item with the ID</returns>
        T Retrieve(uint id);

        /// <summary>
        /// Get a list with all items
        /// </summary>
        /// <returns>A list with all the saved items</returns>
        IEnumerable<T> Retrieve();

        /// <summary>
        /// Update an existing item
        /// </summary>
        /// <remarks>The existing item will be searched by the model ID</remarks>
        /// <param name="item">Item to be Updated</param>
        /// <returns>If an item was updated</returns>
        void Update(T item);

        /// <summary>
        /// Delete an item by its ID
        /// </summary>
        /// <param name="id">D of the item to be removed.</param>
        /// <returns>If an item was deleted</returns>
        void Delete(uint id);
        
    }
}
