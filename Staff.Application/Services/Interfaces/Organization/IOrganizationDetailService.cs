using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IOrganizationDetailService
{
    Task<ResponseWithCode<dynamic>> SaveCompany(OrganizationRequestDto organization);
    Task<ResponseWithCode<dynamic>> GetOrganizationById(long id);

    Task<ResponseWithCode<dynamic>> GetAllOrganizations(int pageNumber, int pageSize, string search);
}