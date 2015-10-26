using System;
using Bdf.MongoDb;
using Bdf.MongoDb.Repositories;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;

namespace Sample.Repositories.MongoDB
{
    public class UserRepository : MongoDbRepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(IMongoDatabaseProvider databaseProvider) 
            : base(databaseProvider)
        {
        }

        public bool CheckPassword(string userName, string password)
        {
            var user = FirstOrDefault(u => u.UserName == userName && u.Password == password);
            return user != null;
        }
    }
}