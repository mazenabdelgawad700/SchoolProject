﻿namespace SchoolProject.Core.Features.ApplicationUser.Queries.Responses
{
    public class GetApplicationUserByIdResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }
}
