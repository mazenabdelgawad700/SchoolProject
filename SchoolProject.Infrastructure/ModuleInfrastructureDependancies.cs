using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Infrastructure;

public static class ModuleInfrastructureDependancies
{
    public static IServiceCollection AddInfrastructureDependancies
        (this IServiceCollection services)
    {
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IDepartmentRepository, DepartmentRepository>();
        services.AddTransient<ISubjectsRepository, SubjectsRepository>();
        services.AddTransient<IInstructorRepository, InstructorRepository>();
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

        return services;
    }
}
