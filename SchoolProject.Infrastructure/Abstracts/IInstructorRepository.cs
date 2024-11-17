using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstracts
{
    public interface IInstructorRepository : IGenericRepositoryAsync<Instructor>
    {
    }
}
