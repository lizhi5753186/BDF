using System;
using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.User
{
    public class DeleteRolesInput : IInputDto
    {
        public List<Guid> RoleList { get; set; }
    }
}