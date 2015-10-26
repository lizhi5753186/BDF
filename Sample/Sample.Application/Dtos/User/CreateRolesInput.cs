using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.User
{
    public class CreateRolesInput : IInputDto
    {
        public List<RoleDto> Roles { get; set; }
    }
}