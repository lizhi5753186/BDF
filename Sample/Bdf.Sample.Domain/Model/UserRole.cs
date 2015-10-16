using System;
using Bdf.Domain.Entities;

namespace Bdf.Sample.Domain.Model
{
    public class UserRole : Entity<Guid>
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public static UserRole CreateUserRole(User user, Role role)
        {
            return new UserRole() { UserId = user.Id, RoleId = role.Id };
        }
    }
}