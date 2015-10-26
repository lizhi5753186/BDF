using System;
using System.Collections.Generic;
using Sample.Application.Dtos.Order;
using Sample.Application.Dtos.User;

namespace Sample.Application
{
    public interface IUserService
    {
        GetUsersOutput CreateUsers(CreateUsersInput input);

        bool ValidateUser(string userName, string password);

        bool DisableUser(UserDto userDto);

        bool EnableUser(UserDto userDto);

        void DeleteUsers(DeleteUsersInput input);

        GetUsersOutput UpdateUsers(UpdateUsersInput input);

        UserDto GetUserByKey(Guid id);
       
        UserDto GetUserByEmail(string email);
       
        UserDto GetUserByName(string userName);

        GetUsersOutput GetUsers();
      
        GetRolesOutput GetRoles();

        RoleDto GetRoleByKey(Guid id);

        GetRolesOutput CreateRoles(CreateRolesInput input);

        GetRolesOutput UpdateRoles(UpdateRolesInput input);

        void DeleteRoles(List<Guid> roleList);
      
        void AssignRole(Guid userId, Guid roleId);
      
        void UnassignRole(Guid userId);

        RoleDto GetRoleByUserName(string userName);

        GetOrdersOutput GetOrdersForUser(string userName);
    }
}