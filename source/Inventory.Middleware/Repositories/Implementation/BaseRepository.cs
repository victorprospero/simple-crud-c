using System.Collections.Generic;

namespace Inventory.Middleware.Repositories.Implementation
{
    public class BaseRepository<T>
    {
        private static List<T> _persistedList;

        protected List<T> GetPersistedList()
        {
            if (_persistedList == null) _persistedList = new List<T>();
            return _persistedList;
        }
    }
}
