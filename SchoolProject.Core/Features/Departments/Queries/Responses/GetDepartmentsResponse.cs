namespace SchoolProject.Core.Features.Departments.Queries.Responses
{
    public class GetDepartmentsResponse
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string? DepartmentManager { get; set; }
    }
}
