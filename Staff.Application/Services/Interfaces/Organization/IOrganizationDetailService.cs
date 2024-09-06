using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IOrganizationDetailService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveOrganizationAsync(OrganizationRequestDto organization);

    #endregion

    #region GET Methods

    Task<ResponseWithCode<dynamic>> GetOrganizationByIdAsync(long id, int status);
    Task<ResponseWithCode<dynamic>> GetAllOrganizationsAsync(int pageNumber, int pageSize, string search, int status);

    #endregion

    #region PUT Methods

    Task<ResponseWithCode<dynamic>> UpdateOrganizationAsync(OrganizationRequestDto organization, long id,bool updateApikey);

    #endregion

    #region DELETE Methods

    Task<ResponseWithCode<dynamic>> DeleteOrganizationAsync(long id);

    #endregion
}