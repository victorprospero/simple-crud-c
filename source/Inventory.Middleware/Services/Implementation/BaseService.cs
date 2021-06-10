using System;

namespace Inventory.Middleware.Services.Implementation
{
    public class BaseService<Repository>
    {
        private Repository _repository;

        protected Repository GetRepository()
        {
            if (_repository == null) _repository = (Repository)Activator.CreateInstance(typeof(Repository));
            return _repository;
        }
    }
}
