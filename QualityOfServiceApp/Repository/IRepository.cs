using System.Collections.Generic;

namespace QualityOfServiceApp.Repository
{
    interface IRepository<T>
    {
        T Add(T item);
        T Delete(T item);
        IEnumerable<T> GetAll();
    }
}
