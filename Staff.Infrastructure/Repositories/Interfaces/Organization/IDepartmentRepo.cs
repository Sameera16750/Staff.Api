using Staff.Core.Entities.Organization;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDepartmentRepo
{
    Task<long>SaveDepartment(Department department);
    Task<Department?>GetDepartment(long id,int status);
    Task<long>UpdateDepartment(Department department);
}