using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectsRepository : GenericRepositoryAsync<Subjects>, ISubjectsRepository
    {
        public SubjectsRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
