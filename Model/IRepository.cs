using System.Linq;

namespace MantiScanServices.Model
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(string emailId = "");

        T Find(long key);

        T Find(string key);

        void Remove(long key);

        void Remove(string key);

        void Add(T item);

        void Update(T item);

        T GetNewItem();
    }
}
