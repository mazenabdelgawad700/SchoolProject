using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementaion
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<string> AddDepartmentAsync(Department department)
        {
            try
            {
                await _departmentRepository.AddAsync(department);
                return "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Exist";
            }
        }
        public async Task<Department> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentRepository.GetTableNoTracking()
                                                      .Where(x => x.DID.Equals(id))
                                                      .Include(d => d.DepartmentSubjects)
                                                      .ThenInclude(ds => ds.Subject)
                                                      .Include(d => d.Instructors)
                                                      //.Include(d => d.Students)
                                                      .Include(d => d.Instructor)
                                                      .FirstOrDefaultAsync();
                return department!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
        public async Task<bool> IsDepartmentNameExsit(string name)
        {
            try
            {
                var department = await _departmentRepository.GetTableNoTracking()
                    .Where(d => d.DName.Equals(name)).FirstOrDefaultAsync();

                if (department is not null)
                    return true;

                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
