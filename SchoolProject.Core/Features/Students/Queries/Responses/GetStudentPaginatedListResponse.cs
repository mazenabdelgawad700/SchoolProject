namespace SchoolProject.Core.Features.Students.Queries.Responses
{
    public class GetStudentPaginatedListResponse
    {
        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? DepartmentName { get; set; }
        public GetStudentPaginatedListResponse(int studId, string name, string address, string departementName)
        {
            StudID = studId;
            Name = name;
            Address = address;
            DepartmentName = departementName;
        }
    }
}
