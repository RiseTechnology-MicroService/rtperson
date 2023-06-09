using Microsoft.Extensions.Options;
using MongoDB.Driver;
using rtperson.DatabaseModels;
using rtperson.DatabaseModels.Settings;
using System.Linq.Expressions;

namespace rtperson.Repositories
{
    public class PersonRepository : IGenericRepository<Person>
    {
        private readonly IMongoCollection<Person> _collection;

        public PersonRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<Person>(databaseSettings.Value.PersonCollectionName);
            _collection.ApplyOptions();
        }

        public async Task<List<Person>> GetListAsync() => await _collection.Find(_ => true).ToListAsync();
        public async Task<List<Person>> GetListAsync(Expression<Func<Person, bool>> predicate) => await _collection.Find(predicate).ToListAsync();
        public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<Person, bool>> predicate, Expression<Func<Person, TResult>> selector) =>
            await _collection.Find(predicate).Project(selector).ToListAsync();

        public async Task<Person> GetAsync(Guid id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<Person> GetAsync(Expression<Func<Person, bool>> predicate) => await _collection.Find(predicate).FirstOrDefaultAsync();
        public async Task<TResult> GetAsync<TResult>(Expression<Func<Person, bool>> predicate, Expression<Func<Person, TResult>> selector) =>
            await _collection.Find(predicate).Project(selector).FirstOrDefaultAsync();

        public async Task CreateAsync(Person newPerson) => await _collection.InsertOneAsync(newPerson);
        public async Task UpdateAsync(Person updatedPerson) => await _collection.ReplaceOneAsync(x => x.Id == updatedPerson.Id, updatedPerson);
        public async Task DeleteAsync(Guid id) => await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
