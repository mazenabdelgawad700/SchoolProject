using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Domain.Helpers;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementaion;
using System.Collections.Concurrent;

namespace SchoolProject.Service;

public static class ModuleServiceDependancies
{
    public static IServiceCollection AddServiceDependancies(this IServiceCollection services)
    {
        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<IDepartmentService, DepartmentService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddSingleton(new ConcurrentDictionary<string, RefreshToken>());

        return services;
    }
}
