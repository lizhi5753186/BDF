using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bdf.Application.Services;
using Bdf.Domain.Services;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;
using Bdf.Sample.Domain.Services;
using Bdf.Uow;
using Sample.Application.Dtos.Order;
using Sample.Application.Dtos.User;

namespace Sample.Application.Imp
{
    public class UserServiceImp : ApplicationService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IDomainService _domainService;

        public UserServiceImp(
            IUserRepository userRepository,
            IShoppingCartRepository shoppingCartRepository,
            IDomainService domainService,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _domainService = domainService;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public GetUsersOutput CreateUsers(CreateUsersInput input)
        {
            if (input.Users == null)
                throw new ArgumentNullException("input");
            var userDtos = this.PerformCreateObjects<List<UserDto>, UserDto, User>(input.Users,
                _userRepository,
                dto =>
                {
                    if (dto.RegisteredDate == null)
                        dto.RegisteredDate = DateTime.Now;
                },
                ar =>
                {
                    var shoppingCart = ar.CreateShoppingCart();
                    _shoppingCartRepository.Insert(shoppingCart);
                });
            return new GetUsersOutput
            {
                Users = userDtos
            };
        }

        public bool ValidateUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            return _userRepository.CheckPassword(userName, password);
        }

        [UnitOfWork]
        public bool DisableUser(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException("userDto");
            User user;
            if (!(userDto.Id == Guid.Empty))
                user = _userRepository.Get(userDto.Id);
            else if (!string.IsNullOrEmpty(userDto.UserName))
                user = _userRepository.Single(u => u.UserName == userDto.UserName);
            else if (!string.IsNullOrEmpty(userDto.Email))
                user = _userRepository.Single(u => u.Email == userDto.Email);
            else
                throw new ArgumentNullException("userDto", "Either ID, UserName or Email should be specified.");
            user.Disable();
            _userRepository.Update(user);
            return user.IsDeleted;
        }

         [UnitOfWork]
        public bool EnableUser(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException("userDto");
            User user;
            if (!(userDto.Id == Guid.Empty))
                user = _userRepository.Get(userDto.Id);
            else if (!string.IsNullOrEmpty(userDto.UserName))
                user = _userRepository.Single(u => u.UserName == userDto.UserName);
            else if (!string.IsNullOrEmpty(userDto.Email))
                user = _userRepository.Single(u => u.Email == userDto.Email);
            else
                throw new ArgumentNullException("userDto", "Either ID, UserName or Email should be specified.");
            user.Enable();
            _userRepository.Update(user);
            return user.IsDeleted;
        }

        public void DeleteUsers(DeleteUsersInput input)
        {
            if (input.Users == null)
                throw new ArgumentNullException("input");
            foreach (var userDto in input.Users)
            {
                User user = null;
                if (!(userDto.Id == Guid.Empty))
                    user = _userRepository.Get(userDto.Id);
                else if (!string.IsNullOrEmpty(userDto.UserName))
                    user = _userRepository.Single(u => u.UserName == userDto.UserName);
                else if (!string.IsNullOrEmpty(userDto.Email))
                    user = _userRepository.Single(u => u.Email == userDto.Email);
                else
                    throw new ArgumentNullException("input", "Either ID, UserName or Email should be specified.");
                var userRole = _userRoleRepository.Single(ur => ur.UserId == user.Id);
                if (userRole != null)
                    _userRoleRepository.Delete(userRole);
                _userRepository.Delete(user);
            }
        }

        public GetUsersOutput UpdateUsers(UpdateUsersInput input)
        {
            var userDtos = this.PerformUpdateObjects<List<UserDto>, UserDto, User>(input.Users, _userRepository,
               userDto => userDto.Id.ToString(),
               (u, userDto) =>
               {
                   if (!string.IsNullOrEmpty(userDto.Contact))
                       u.Contact = userDto.Contact;
                   if (!string.IsNullOrEmpty(userDto.PhoneNumber))
                       u.PhoneNumber = userDto.PhoneNumber;
                   if (userDto.ContactAddress != null)
                   {
                       if (!string.IsNullOrEmpty(userDto.ContactAddress.City))
                           u.ContactAddress.City = userDto.ContactAddress.City;
                       if (!string.IsNullOrEmpty(userDto.ContactAddress.Country))
                           u.ContactAddress.Country = userDto.ContactAddress.Country;
                       if (!string.IsNullOrEmpty(userDto.ContactAddress.State))
                           u.ContactAddress.State = userDto.ContactAddress.State;
                       if (!string.IsNullOrEmpty(userDto.ContactAddress.Street))
                           u.ContactAddress.Street = userDto.ContactAddress.Street;
                       if (!string.IsNullOrEmpty(userDto.ContactAddress.Zip))
                           u.ContactAddress.Zip = userDto.ContactAddress.Zip;
                   }
                   if (userDto.DeliveryAddress != null)
                   {
                       if (!string.IsNullOrEmpty(userDto.DeliveryAddress.City))
                           u.DeliveryAddress.City = userDto.DeliveryAddress.City;
                       if (!string.IsNullOrEmpty(userDto.DeliveryAddress.Country))
                           u.DeliveryAddress.Country = userDto.DeliveryAddress.Country;
                       if (!string.IsNullOrEmpty(userDto.DeliveryAddress.State))
                           u.DeliveryAddress.State = userDto.DeliveryAddress.State;
                       if (!string.IsNullOrEmpty(userDto.DeliveryAddress.Street))
                           u.DeliveryAddress.Street = userDto.DeliveryAddress.Street;
                       if (!string.IsNullOrEmpty(userDto.DeliveryAddress.Zip))
                           u.DeliveryAddress.Zip = userDto.DeliveryAddress.Zip;
                   }
                   if (userDto.LastLogonDate != null)
                       u.LastLogonDate = userDto.LastLogonDate;
                   if (userDto.RegisteredDate != null)
                       u.CreationTime = userDto.RegisteredDate.Value;
                   if (!string.IsNullOrEmpty(userDto.Email))
                       u.Email = userDto.Email;

                   if (userDto.IsDisabled != null)
                   {
                       if (userDto.IsDisabled.Value)
                           u.Disable();
                       else
                           u.Enable();
                   }

                   if (!string.IsNullOrEmpty(userDto.Password))
                       u.Password = userDto.Password;
               });

            return new GetUsersOutput
            {
                Users = userDtos
            };
        }

        public UserDto GetUserByKey(Guid id)
        {
            var user = _userRepository.Get(id);
            var userDto = Mapper.Map<User, UserDto>(user);
            return userDto;
        }

        public UserDto GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("email");
            var user = _userRepository.Single(u => u.Email == email);
            var userDto = Mapper.Map<User, UserDto>(user);
            return userDto;
        }

        public UserDto GetUserByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("userName");
            var user = _userRepository.Single(u => u.UserName == userName);
            var userDto = Mapper.Map<User, UserDto>(user);
            return userDto;
        }

        public GetUsersOutput GetUsers()
        {
            var users = _userRepository.GetAll();
            if (users == null)
                return null;
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = Mapper.Map<User, UserDto>(user);
                var role = _userRoleRepository.GetRoleForUser(user);
                if (role != null)
                {
                    userDto.Role = Mapper.Map<Role, RoleDto>(role);
                }

                result.Add(userDto);
            }

            return new GetUsersOutput
            {
                Users = result
            };
        }

        public GetRolesOutput GetRoles()
        {
            var roles = _roleRepository.GetAll();
            if (roles == null)
                return null;
            var result = roles.Select(role => Mapper.Map<Role, RoleDto>(role)).ToList();
            return new GetRolesOutput
            {
                Roles = result
            };
        }

        public RoleDto GetRoleByKey(Guid id)
        {
            return Mapper.Map<Role, RoleDto>(_roleRepository.Get(id));
        }

        public GetRolesOutput CreateRoles(CreateRolesInput input)
        {
            var roleDtos = this.PerformCreateObjects<List<RoleDto>, RoleDto, Role>(input.Roles, _roleRepository);
            return new GetRolesOutput
            {
                Roles = roleDtos
            };
        }

        public GetRolesOutput UpdateRoles(UpdateRolesInput input)
        {
            var roleDtos = this.PerformUpdateObjects<List<RoleDto>, RoleDto, Role>(input.Roles,
                _roleRepository,
                roleDto => roleDto.Id.ToString(),
                (r, roleDto) =>
                {
                    if (!string.IsNullOrEmpty(roleDto.Name))
                        r.Name = roleDto.Name;
                    if (!string.IsNullOrEmpty(roleDto.Description))
                        r.Description = roleDto.Description;
                });
            return new GetRolesOutput
            {
                Roles = roleDtos
            };
        }

        public void DeleteRoles(List<Guid> roleList)
        {
            this.PerformDeleteObjects<Role>(roleList,
                _roleRepository,
                id =>
                {
                    var userRole = _userRoleRepository.Single(ur => ur.RoleId == id);
                    if (userRole != null)
                        _userRoleRepository.Delete(userRole);
                });
        }

        public void AssignRole(Guid userId, Guid roleId)
        {
            var user = _userRepository.Get(userId);
            var role = _roleRepository.Get(roleId);
            _domainService.AssignRole(user, role);
        }

        public void UnassignRole(Guid userId)
        {
            var user = _userRepository.Get(userId);
            _domainService.UnassignRole(user);
        }

        public RoleDto GetRoleByUserName(string userName)
        {
            var user = _userRepository.Single(u => u.UserName == userName);
            var role = _userRoleRepository.GetRoleForUser(user);
            return Mapper.Map<Role, RoleDto>(role);
        }

        public GetOrdersOutput GetOrdersForUser(string userName)
        {
            var user = _userRepository.Single(u => u.UserName == userName);
            var orders = user.Orders;
            var result = new List<OrderDto>();
            if (orders == null) return  new GetOrdersOutput { Orders = result };

            result = orders.Select(Mapper.Map<Order, OrderDto>).ToList();
            return new GetOrdersOutput { Orders = result };
        }
    }
}