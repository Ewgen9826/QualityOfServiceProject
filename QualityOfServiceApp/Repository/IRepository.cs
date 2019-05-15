using System.Collections.Generic;

namespace QualityOfServiceApp.Repository
{
    interface IRepository<T>
    {
        T Add(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
        bool SaveAll();
    }
}
