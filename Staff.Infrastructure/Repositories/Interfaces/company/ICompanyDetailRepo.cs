using Staff.Core.Entities.Company;

namespace Staff.Infrastructure.Repositories.Interfaces.company;

public interface ICompanyDetailRepo
{
    Task<long> SaveCompany(CompanyDetails companyDetails);
}