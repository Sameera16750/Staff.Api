using System.Net;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Helpers.ResponseHelper;

public interface IResponseHelper
{
    MessageResponse CreateMessageResponse(string message);
    IdResponse<T> CreateIdResponse<T>(T id, string message);
    ResponseWithCode<T> CreateResponseWithCode<T>(HttpStatusCode statusCode, T? data = default(T));
    ResponseWithCode<dynamic> InternalServerErrorResponse();
    ResponseWithCode<dynamic> NotFoundErrorResponse();
    ResponseWithCode<dynamic> SaveFailedResponse();
    ResponseWithCode<dynamic> UpdateFailedResponse();
    ResponseWithCode<dynamic> DeleteFailedErrorResponse();
    ResponseWithCode<dynamic> SaveSuccessResponse<T>(T id);
    ResponseWithCode<dynamic> DeleteSuccessResponse<T>(T id);
    ResponseWithCode<dynamic> UpdateSuccessResponse<T>(T id);
    ResponseWithCode<dynamic> BadRequest(string message);
}