using MongoDB.Driver;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public BaseRepository(MongoDbContext context)
        {
            string collectionTarget = typeof(T).Name + "s";
            _collection = context.GetCollection<T>(collectionTarget);
        }

        public Task<bool> AddAsync(T toAdd)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(T toDelete)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
