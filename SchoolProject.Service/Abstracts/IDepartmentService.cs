﻿using SchoolProject.Domain.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentById(int id);
        Task<string> AddDepartmentAsync(Department department);
        Task<bool> IsDepartmentNameExsit(string name);
        Task<List<Department>> GetDepartmentsAsync();
        Task<string> UpdateDepartmentAsync(Department department);
        Task<bool> IsDepartmentNameUsedButNotTheSameDepartmentAsync(string name, int id);
        Task<string> DeleteDepartmentAsync(int id);
        Task<bool> IsDepartmentIdExist(int id);
    }
}
