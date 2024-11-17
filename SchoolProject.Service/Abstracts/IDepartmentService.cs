using SchoolProject.Domain.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentById(int id);
        Task<string> AddDepartmentAsync(Department department);
        Task<bool> IsDepartmentNameExsit(string name);
        Task<List<Department>> GetDepartmentsAsync();
    }
}
