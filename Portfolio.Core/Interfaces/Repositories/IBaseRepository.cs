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
        Task<IEnumerable<T>> GetAllAsync(string userId);
        Task<bool> AddAsync(T toAdd);
        Task<bool> UpdateAsync(T toUpdate);
        Task<bool> DeleteAsync(T toDelete);
        Task<bool> CheckIfExistsAsync(string id);
    }
}
