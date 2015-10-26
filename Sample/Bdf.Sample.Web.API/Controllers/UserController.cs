using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using Sample.Application;
using Sample.Application.Dtos.User;

namespace Bdf.Sample.Web.API.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserService _userServiceImp;

        public UserController(IUserService userService)
        {
            _userServiceImp = userService;
        }

        public GetUsersOutput CreateUsers(CreateUsersInput input)
        {
            return _userServiceImp.CreateUsers(input);
        }

        public bool ValidateUser(string userName, string password)
        {
            return _userServiceImp.ValidateUser(userName, password);
        }

        public bool DisableUser(UserDto userDto)
        {
            return _userServiceImp.DisableUser(userDto);
        }

        public bool EnableuUser(UserDto userDto)
        {
            return _userServiceImp.EnableUser(userDto);
        }

        public void DeleteUsers(DeleteUsersInput userDtos)
        {
            _userServiceImp.DeleteUsers(userDtos);
        }

        public GetUsersOutput UpdateUsers(UpdateUsersInput userDataObjects)
        {
            return _userServiceImp.UpdateUsers(userDataObjects);
        }

        public UserDto GetUserByKey(Guid id)
        {
            return _userServiceImp.GetUserByKey(id);
        }

        public UserDto GetUserByEmail(string email)
        {
            return _userServiceImp.GetUserByEmail(email);
        }

        public UserDto GetUserByName(string userName)
        {
            return _userServiceImp.GetUserByName(userName);
        }

        public GetUsersOutput GetUsers()
        {
            return _userServiceImp.GetUsers();
        }

        public GetRolesOutput GetRoles()
        {
            return _userServiceImp.GetRoles();
        }

        public RoleDto GetRoleByKey(Guid id)
        {
            return _userServiceImp.GetRoleByKey(id);
        }

        public GetRolesOutput CreateRoles(CreateRolesInput roleDataObjects)
        {
            return _userServiceImp.CreateRoles(roleDataObjects);
        }

        public GetRolesOutput UpdateRoles(UpdateRolesInput roleDataObjects)
        {
            return _userServiceImp.UpdateRoles(roleDataObjects);
        }

        public void DeleteRoles(List<Guid> roleList)
        {
            _userServiceImp.DeleteRoles(roleList);
        }

        public void AssignRole(Guid userId, Guid roleId)
        {
            _userServiceImp.AssignRole(userId, roleId);
        }

        public void UnassignRole(Guid userId)
        {
            _userServiceImp.UnassignRole(userId);
        }
    }
}
