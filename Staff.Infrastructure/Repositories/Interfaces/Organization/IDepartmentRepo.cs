using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDepartmentRepo
{
    #region POST Methods
    
    Task<long> SaveDepartmentAsync(Department department);

    #endregion

    #region GET Methods

    Task<Department?> GetDepartmentByNameAsync(string name, long organization, int status);
    
    Task<Department?> GetDepartmentAsync(long id, int status);

    Task<PaginatedListDto<Department>?> GetAllDepartmentsAsync(string search, int pageNumber, int pageSize,
        int departmentStatus, long organization, int organizationStatus);

    #endregion

    #region PUT Methods

    Task<long> UpdateDepartmentAsync(Department department);

    #endregion

    #region DELETE Methods

    Task<long> DeleteDepartmentAsync(long id);

    #endregion
}