using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IOrganizationDetailService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveOrganization(OrganizationRequestDto organization);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetOrganizationById(long id, int status);
    Task<ResponseWithCode<dynamic>> GetAllOrganizations(int pageNumber, int pageSize, string search, int status);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateOrganization(OrganizationRequestDto organization, long id);

    #endregion

    #region DELETE Methods

    Task<ResponseWithCode<dynamic>> DeleteOrganization(long id);

    #endregion
}