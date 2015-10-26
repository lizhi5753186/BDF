using System;
using System.Linq;
using Bdf.MongoDb;
using Bdf.MongoDb.Repositories;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;
using MongoDB.Driver.Linq;

namespace Sample.Repositories.MongoDB
{
    public class UserRoleRepository : MongoDbRepositoryBase<UserRole, Guid>, IUserRoleRepository
    {
        public UserRoleRepository(IMongoDatabaseProvider databaseProvider) 
            : base(databaseProvider)
        {
        }

        public Role GetRoleForUser(User user)
        {
            var userRole = Collection.AsQueryable().FirstOrDefault(ur => ur.UserId == user.Id);
            if (userRole == null)
                return null;
            var roleCollection = Database.GetCollection<Role>(typeof(Role).Name);
            return roleCollection.AsQueryable().FirstOrDefault(r => r.Id == userRole.RoleId);
        }
    }
}