using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Enums;

namespace SchoolProject.Service.Abstracts;

public interface IStudentService
{
    Task<List<Student>> GetStudentsAsync();
    Task<Student> GetStudentByIdAsync(int id);
    Task<Student> GetByIdAsync(int id);
    Task<string> AddStudentAsync(Student student);
    Task<bool> IsStudentNameUsed(string name);
    Task<string> UpdateStudentAsync(Student student);
    Task<bool> IsStudentNameUsedButNotTheSameStudent(string name, int id);
    Task<bool> DeleteStudentAsync(int id);
    IQueryable<Student> GetStudentsAsQuerable();
    IQueryable<Student> GetStudentsByDepartmentIDAsQuerable(int DID);
    IQueryable<Student> GetStudentFilterPaginatedQuerable(StudentOrderingEnum orderBy, string search);
}
