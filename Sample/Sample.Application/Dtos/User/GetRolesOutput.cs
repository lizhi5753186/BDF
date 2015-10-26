using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.User
{
    public class GetRolesOutput : IOutputDto
    {
        public IList<RoleDto> Roles { get; set; }
    }
}