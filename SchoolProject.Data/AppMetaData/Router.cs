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
            public const string List = Prefix + "List";
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
            public const string List = Prefix + "List";
            public const string GetById = Prefix + "Id";
            public const string Create = Prefix;
            public const string Update = Prefix;
            public const string Delete = Prefix + WithId;
            public const string Paginated = Prefix + "Paginated";
        }
        public static class UserRouting
        {
            // Api/V1/User
            private const string Prefix = Rule + "User/";
            public const string List = Prefix + "List";
            public const string GetById = Prefix + "Id";
            public const string Create = Prefix;
            public const string Update = Prefix;
            public const string Delete = Prefix + WithId;
            public const string Paginated = Prefix + "Paginated";
        }
    }
}
