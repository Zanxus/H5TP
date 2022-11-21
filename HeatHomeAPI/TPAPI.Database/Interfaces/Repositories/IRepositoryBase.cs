using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAPI.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {


        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(Guid id);

        Task AddAsync(T entity);

        IReadOnlyList<T> GetAll();
        T? Get(Guid id);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
