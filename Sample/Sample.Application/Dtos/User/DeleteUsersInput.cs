using System;
using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.User
{
    public class DeleteUsersInput: IInputDto
    {
        public List<UserDto> Users { get; set; }
    }
}