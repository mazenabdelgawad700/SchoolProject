using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories;

public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
{
    private readonly DbSet<Student> _students;
    public StudentRepository(AppDbContext dbContext):base(dbContext)
    {
        _students = dbContext.Set<Student>();
    }

    #region Handle Functions
    public async Task<List<Student>> GetStudentsAsync()
    {
        try
        {
            return await _students.Include(x => x.Department).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erorr: {ex.Message}");
        }
    }
    #endregion
}
