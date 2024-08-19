using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IOrganizationDetailService
{
    Task<ResponseWithCode<dynamic>> SaveOrganization(OrganizationRequestDto organization);
    Task<ResponseWithCode<dynamic>> GetOrganizationById(long id,int status);
    Task<ResponseWithCode<dynamic>> GetAllOrganizations(int pageNumber, int pageSize, string search,int status);
    Task<ResponseWithCode<dynamic>> UpdateOrganization(OrganizationRequestDto organization, long id);
    Task<ResponseWithCode<dynamic>> DeleteOrganization(long id);
}