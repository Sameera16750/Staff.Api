using Staff.Core.Entities.Organization;

namespace Staff.Infrastructure.Repositories.Interfaces.Organization;

public interface IDepartmentRepo
{
    Task<long>SaveDepartment(Department department);
}