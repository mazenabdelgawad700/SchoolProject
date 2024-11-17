using SchoolProject.Domain.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentById(int id);
    }
}
