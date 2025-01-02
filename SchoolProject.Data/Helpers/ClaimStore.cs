using System.Security.Claims;

namespace SchoolProject.Domain.Helpers
{
    public static class ClaimStore
    {
        public static List<Claim> Claims = new()
        {
            new Claim("Create Student", "false"),
            new Claim("Update Student", "false"),
            new Claim("Delete Student", "false"),
        };

    }
}
