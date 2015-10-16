using System;
using Bdf.Domain.Repositories;
using Bdf.Sample.Domain.Model;

namespace Bdf.Sample.Domain.Repositories
{
    public interface IUserRepository  : IRepository<User, Guid>
    {
        bool CheckPassword(string userName, string password);
    }
}