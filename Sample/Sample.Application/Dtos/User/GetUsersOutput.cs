using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.User
{
    public class GetUsersOutput : IOutputDto
    {
        public IList<UserDto> Users { get; set; }
    }
}