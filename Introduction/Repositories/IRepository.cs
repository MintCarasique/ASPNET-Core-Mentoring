using System.Collections.Generic;

namespace Northwind.Repositories
{
    interface IRepository<T>
    {
        List<T> GetAll();

        T Get(int id);

        void Update(T updatingObject);

        void Create(T newObject);
    }
}
