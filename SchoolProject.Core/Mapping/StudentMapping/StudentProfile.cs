﻿using AutoMapper;
namespace SchoolProject.Core.Mapping.StudentMapping;

public partial class StudentProfile : Profile
{
    public StudentProfile()
    {
        GetStudentsMapping();
        GetStudentByIdMapping();
        AddStudentMapping();
        EditStudentMapping();
        DeleteStudentMapping();
    }
}