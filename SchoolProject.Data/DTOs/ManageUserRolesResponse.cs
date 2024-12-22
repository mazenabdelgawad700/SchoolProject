namespace SchoolProject.Domain.DTOs
{
    public class ManageUserRolesResponse
    {
        public int UserId { get; set; }
        public List<UserRoles> UserRoles { get; set; }
    }
    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
