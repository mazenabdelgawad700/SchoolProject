﻿using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Domain.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResponse>>
    {
        public int UserId { get; set; }
    }
}
