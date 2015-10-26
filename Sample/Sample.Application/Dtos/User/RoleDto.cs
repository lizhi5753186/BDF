using System;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.User
{
    public class RoleDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; } 
    }
}