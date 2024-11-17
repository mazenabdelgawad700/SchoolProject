﻿using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Departments.Queries.Responses
{
    public class GetDepartmentByIdResponse
    {
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public string? ManagerName { get; set; }
        public PaginatedResult<StudentResponse>? StudentList { get; set; }
        public List<InstructorResponse>? InstructorList { get; set; }
        public List<SubjectResponse>? SubjectList { get; set; }
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public StudentResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class InstructorResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
