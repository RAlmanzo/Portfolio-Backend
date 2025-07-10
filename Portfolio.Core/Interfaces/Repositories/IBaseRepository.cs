using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> GetByIdAsync(string id);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T toAdd);
        Task UpdateAsync(T toUpdate);
        Task DeleteAsync(T toDelete);
        Task CheckIfExistsAsync(string id);
    }
}
