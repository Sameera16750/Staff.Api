using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDepartmentRepo
{
    Task<long> SaveDepartment(Department department);
    Task<Department?> GetDepartment(long id, int status);

    Task<PaginatedListDto<Department>?> GetAllDepartments(string search, int pageNumber, int pageSize,
        int departmentStatus, long organization, int organizationStatus);

    Task<long> UpdateDepartment(Department department);
}