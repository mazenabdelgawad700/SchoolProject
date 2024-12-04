using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementaion;

namespace SchoolProject.Service;

public static class ModuleServiceDependancies
{
    public static IServiceCollection AddServiceDependancies(this IServiceCollection services)
    {
        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<IDepartmentService, DepartmentService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
