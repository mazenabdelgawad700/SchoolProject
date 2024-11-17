using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstracts;

public interface IStudentRepository: IGenericRepositoryAsync<Student>
{
    Task<List<Student>> GetStudentsAsync();
}