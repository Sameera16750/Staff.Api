using Staff.Application.Models.Request.Organization;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Services.Interfaces.Organization;

public interface IDesignationService
{
    #region POST Methods

    Task<ResponseWithCode<dynamic>> SaveDesignationAsync(DesignationRequestDto request);

    #endregion
}