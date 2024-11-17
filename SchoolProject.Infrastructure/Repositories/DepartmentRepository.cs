using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>,
        IDepartmentRepository
    {
        private readonly DbSet<Department> _departments;
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            try
            {
                return await _departments.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
