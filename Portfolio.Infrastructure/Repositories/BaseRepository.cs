using MongoDB.Driver;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Infrastructure.Data;
using SharpCompress.Common;
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
            string collectionTarget = typeof(T).Name.ToLower() + "s";
            _collection = context.GetCollection<T>(collectionTarget);
        }

        public async Task<IEnumerable<T>> GetAllAsync(string userId)
        {
            return await _collection.Find(p => p.Id == userId).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T toAdd)
        {
            await _collection.InsertOneAsync(toAdd);
        }

        public async Task DeleteAsync(T toDelete)
        {
            await _collection.DeleteOneAsync(e => e.Id == toDelete.Id);
        }

        public async Task UpdateAsync(T toUpdate)
        {
            await _collection.ReplaceOneAsync(e => e.Id == toUpdate.Id, toUpdate);
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task CheckIfExistsAsync(string id)
        {
            await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}
