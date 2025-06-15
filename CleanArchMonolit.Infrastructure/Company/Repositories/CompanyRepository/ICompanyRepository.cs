using CleanArchMonolit.Infrastructure.DataShared.Interface;
using CompanyEntity = CleanArchMonolit.Domain.Company.Entities.Company;

namespace CleanArchMonolit.Infrastructure.Company.Repositories.CompanyRepository
{
    public interface ICompanyRepository : IRepository<CompanyEntity>
    {
    }
}
