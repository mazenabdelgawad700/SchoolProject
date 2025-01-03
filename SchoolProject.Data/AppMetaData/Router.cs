namespace SchoolProject.Domain.AppMetaData
{
    public static class Router
    {
        public const string WithId = "{id}";
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";

        public static class StudentRouting
        {
            // Api/V1/Student
            private const string Prefix = Rule + "Student/";
            public const string List = Prefix + "List/";
            public const string GetById = Prefix + WithId;
            public const string Create = Prefix;
            public const string Update = Prefix;
            public const string Delete = Prefix + WithId;
            public const string Paginated = Prefix + "Paginated";
        }
        public static class DepartmentRouting
        {
            // Api/V1/Department
            private const string Prefix = Rule + "Department/";
            public const string List = Prefix + "List/";
            public const string GetById = Prefix + "Id/";
            public const string Create = Prefix;
            public const string Update = Prefix;
            public const string Delete = Prefix + WithId;
            public const string Paginated = Prefix + "Paginated/";
        }
        public static class UserRouting
        {
            // Api/V1/User
            private const string Prefix = Rule + "User/";
            public const string List = Prefix + "List/";
            public const string GetById = Prefix + "Id/";
            public const string Create = Prefix + "Add/";
            public const string Update = Prefix + "Update/";
            public const string Delete = Prefix + "Delete/" + WithId;
            public const string Paginated = Prefix + "Paginated/";
        }
        public static class AuthenticationRouting
        {
            // Api/V1/Authentication
            private const string Prefix = Rule + "Authentication/";
            public const string SignIn = Prefix + "SignIn/";
            public const string RefreshToken = Prefix + "Refresh-Token/";
            public const string ValidateToken = Prefix + "Validate-Token/";

        }
        public static class AuthorizationRouting
        {
            // Api/V1/Authentication
            private const string Prefix = Rule + "Authorization/";
            public const string GetRolesList = Prefix + "Role/List/";
            public const string GetRoleById = Prefix + "Role/{Id}/";
            public const string AddRole = Prefix + "Role/Add/";
            public const string UpdateRole = Prefix + "Role/Update/";
            public const string DeleteRole = Prefix + "Role/Delete/";
            public const string GetUserRolesList = Prefix + "Role/User-Roles-List/";
            public const string UpdateUserRolesList = Prefix + "Role/Update/User-Roles/";
            // Claims
            public const string ManageUserClaims = Prefix + "Claim/Manage-User-Claims/{userId}/";
            public const string UpdateUserClaimsList = Prefix + "Claim/Update/User-Claims/";
        }

    }
}
