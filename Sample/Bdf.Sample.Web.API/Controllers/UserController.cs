using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using Sample.Application;
using Sample.Application.Dtos.Order;
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

        [Route("CreateUsers")]
        public GetUsersOutput CreateUsers(CreateUsersInput input)
        {
            return _userServiceImp.CreateUsers(input);
        }

        [Route("ValidateUser")]
        public bool ValidateUser(string userName, string password)
        {
            return _userServiceImp.ValidateUser(userName, password);
        }

        [Route("DisableUser")]
        public bool DisableUser(UserDto userDto)
        {
            return _userServiceImp.DisableUser(userDto);
        }

        [Route("EnableuUser")]
        public bool EnableuUser(UserDto userDto)
        {
            return _userServiceImp.EnableUser(userDto);
        }

        [Route("DeleteUsers")]
        public void DeleteUsers(DeleteUsersInput userDtos)
        {
            _userServiceImp.DeleteUsers(userDtos);
        }

        [Route("UpdateUsers")]
        public GetUsersOutput UpdateUsers(UpdateUsersInput userDataObjects)
        {
            return _userServiceImp.UpdateUsers(userDataObjects);
        }

        [Route("UserByKey")]
        public UserDto GetUserByKey(Guid id)
        {
            return _userServiceImp.GetUserByKey(id);
        }

        [Route("UserByEmail")]
        public UserDto GetUserByEmail(string email)
        {
            return _userServiceImp.GetUserByEmail(email);
        }

        [Route("UserByName")]
        public UserDto GetUserByName(string userName)
        {
            return _userServiceImp.GetUserByName(userName);
        }

        [Route("Users")]
        public GetUsersOutput GetUsers()
        {
            return _userServiceImp.GetUsers();
        }

        [Route("Roles")]
        public GetRolesOutput GetRoles()
        {
            return _userServiceImp.GetRoles();
        }

        [Route("RoleByKey")]
        public RoleDto GetRoleByKey(Guid id)
        {
            return _userServiceImp.GetRoleByKey(id);
        }

        [Route("CreateRoles")]
        public GetRolesOutput CreateRoles(CreateRolesInput roleDataObjects)
        {
            return _userServiceImp.CreateRoles(roleDataObjects);
        }

        [Route("UpdateRoles")]
        public GetRolesOutput UpdateRoles(UpdateRolesInput roleDataObjects)
        {
            return _userServiceImp.UpdateRoles(roleDataObjects);
        }

        [Route("DeleteRoles")]
        public void DeleteRoles(List<Guid> roleList)
        {
            _userServiceImp.DeleteRoles(roleList);
        }

        [Route("AssignRole")]
        public void AssignRole(Guid userId, Guid roleId)
        {
            _userServiceImp.AssignRole(userId, roleId);
        }

        [Route("UnassignRole")]
        public void UnassignRole(Guid userId)
        {
            _userServiceImp.UnassignRole(userId);
        }

        [Route("RoleByUserName")]
        public RoleDto GetRoleByUserName(string userName)
        {
            return _userServiceImp.GetRoleByUserName(userName);
        }

        [Route("OrdersForUser")]
        public GetOrdersOutput GetOrdersForUser(string userName)
        {
            return _userServiceImp.GetOrdersForUser(userName);
        }
    }
}
