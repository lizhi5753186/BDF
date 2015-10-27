using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bdf.Json;
using Microsoft.AspNet.Identity;
using Sample.Application.Dtos.Order;
using Sample.Application.Dtos.User;

namespace Bdf.Sample.Web.Mvc.SampleIdentity
{
    public class SampleUserStore : IUserStore<SampleUser, Guid>
    {
        public void Dispose()
        {
           
        }

        public Task CreateAsync(SampleUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (!string.IsNullOrEmpty(GetUserNameByEmail(user.Email)))
            {
                throw new Exception("DuplicateEmail");
            }

            var existuser = FindByIdAsync(user.Id);
            if (existuser != null) return Task.FromResult<object>(null);
            using (var client = new HttpClient())
            {
                var users = new CreateUsersInput
                {
                    Users = new List<UserDto>
                    {
                        new UserDto
                        {
                            UserName = user.UserName,
                            Password = user.Password,
                            Contact = user.Contact,
                            LastLogonDate = DateTime.MinValue,
                            RegisteredDate = DateTime.Now,
                            Email = user.Email,
                            IsDisabled = false,
                            PhoneNumber = user.PhoneNumber,
                            ContactAddress =
                                new AddressDto
                                {
                                    Country = user.ContactAddress.Country,
                                    State = user.ContactAddress.State,
                                    City = user.ContactAddress.City,
                                    Street = user.ContactAddress.Street,
                                    Zip = user.ContactAddress.Zip
                                },
                            DeliveryAddress =
                                new AddressDto
                                {
                                    Country = user.DeliveryAddress.Country,
                                    State = user.DeliveryAddress.State,
                                    City = user.DeliveryAddress.City,
                                    Street = user.DeliveryAddress.Street,
                                    Zip = user.DeliveryAddress.Zip
                                },
                        }
                    }
                };

                var jsonStr = JsonHelper.ConvertToJson(users);
                var result = client.PostAsync("http://localhost:3296/api/UserCreateUsers", new StringContent(jsonStr)).Result;

                return Task.FromResult<object>(result);
            }
        }

        public Task UpdateAsync(SampleUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(SampleUser user)
        {
            throw new NotImplementedException();
        }

        public Task<SampleUser> FindByIdAsync(Guid userId)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://localhost:3296/api/User/UserByKey?id=" + userId).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var userDto = JsonHelper.ConvertJsonToObject<SampleUser>(result);
                return userDto == null ? Task.FromResult<SampleUser>(null) : Task.FromResult(userDto);
            }
        }

        public Task<SampleUser> FindByNameAsync(string userName)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://localhost:3296/api/User/UserByName?userName=" + userName).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var userDto = JsonHelper.ConvertJsonToObject<SampleUser>(result);
                return userDto == null ? Task.FromResult<SampleUser>(null) : Task.FromResult(userDto);
            }
        }

        #region Helper Methods
        public string GetUserNameByEmail(string email)
        {
            using (var proxy = new HttpClient())
            {
                var response = proxy.GetAsync("http://localhost:3296/api/User/UserByEmail?email=" + email).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var userDto = JsonHelper.ConvertJsonToObject<SampleUser>(result);
                return userDto == null ? null : userDto.UserName;
            }
        }
        #endregion 
    }
}