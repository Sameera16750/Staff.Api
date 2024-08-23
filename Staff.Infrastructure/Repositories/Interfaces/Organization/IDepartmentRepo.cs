using Staff.Core.Entities.Organization;
using Staff.Infrastructure.Models;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDepartmentRepo
{
    #region POST Methods
    
    Task<long> SaveDepartment(Department department);

    #endregion

    #region GET Methods

    Task<Department?> GetDepartment(long id, int status);

    Task<PaginatedListDto<Department>?> GetAllDepartments(string search, int pageNumber, int pageSize,
        int departmentStatus, long organization, int organizationStatus);

    #endregion

    #region PUT Methods

    Task<long> UpdateDepartment(Department department);

    #endregion

    #region DELETE Methods

    Task<long> DeleteDepartment(long id);

    #endregion
}