using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Helpers;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementaion;

public class StudentService : IStudentService
{
    #region Fields
    private readonly IStudentRepository _studentRepository;
    #endregion

    #region Constructors
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    #endregion

    #region Handle Functions
    public async Task<List<Student>> GetStudentsAsync()
    {
        try
        {
            return await _studentRepository.GetStudentsAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erorr: {ex.Message}");
        }
    }
    public async Task<Student> GetStudentByIdAsync(int id)
    {
        try
        {
            if (id > 0)
            {
                var student = _studentRepository
                    .GetTableNoTracking()
                    .Include(e => e.Department)
                    .Where(e => e.StudID == id)
                    .FirstOrDefault();

                if (student is not null)
                {
                    return student;
                }
            }
            return null!; // Temporary 
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}", ex);
        }
    }
    public async Task<string> AddStudentAsync(Student student)
    {
        try
        {
            await _studentRepository.AddAsync(student);
            return "Success";
        }
        catch (Exception ex)
        {
            return "Exist";
        }
    }
    public async Task<bool> IsStudentNameUsed(string name)
    {
        var studentResult = _studentRepository.GetTableNoTracking()
                .Where(s => (s.Name == name))
                .FirstOrDefault();
        if (studentResult is null)
            return false;
        else
            return true;
    }
    public async Task<string> UpdateStudentAsync(Student student)
    {
        try
        {
            await _studentRepository.UpdateAsync(student);
            return "Updated";
        }
        catch
        {
            return "Error";
        }
    }
    public async Task<bool> IsStudentNameUsedButNotTheSameStudent(string name, int id)
    {
        var studentResult = _studentRepository.GetTableNoTracking()
                .Where(s => (s.Name == name) && s.StudID != id)
                .FirstOrDefault();
        if (studentResult is null)
            return false;
        else
            return true;
    }
    public async Task<bool> DeleteStudentAsync(int id)
    {
        try
        {
            await _studentRepository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<Student> GetByIdAsync(int id)
    {
        try
        {
            if (id > 0)
            {
                var student = _studentRepository
                    .GetTableNoTracking()
                    .Where(e => e.StudID == id)
                    .FirstOrDefault();

                if (student is not null)
                {
                    return student;
                }
            }
            return null!;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}", ex);
        }
    }
    public IQueryable<Student> GetStudentsAsQuerable()
    {
        try
        {
            return _studentRepository.GetTableNoTracking()
                                     .Include(s => s.Department)
                                     .AsQueryable();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    public IQueryable<Student> GetStudentFilterPaginatedQuerable(StudentOrderingEnum orderBy, string search)
    {
        try
        {
            var students =
                _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();

            if (orderBy.ToString() is null && search is null)
                return students;

            else if (search is not null)
                return students.Where(s => s.Name!.Contains(search) || s.Address!.Contains(search));

            switch (orderBy)
            {
                case StudentOrderingEnum.StudID:
                    return students.OrderBy(s => s.StudID);
                case StudentOrderingEnum.Name:
                    return students.OrderBy(s => s.Name);
                case StudentOrderingEnum.DepartmentName:
                    return students.OrderBy(s => s.Department!.DName);
                case StudentOrderingEnum.Address:
                    return students.OrderBy(s => s.Address);
                default:
                    return students;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    public IQueryable<Student> GetStudentsByDepartmentIDAsQuerable(int DID)
    {
        try
        {
            return _studentRepository.GetTableNoTracking()
                                     .Where(x => x.DID.Equals(DID))
                                     .AsQueryable();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    #endregion
}
