using System.Net;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Helpers.ResponseHelper;

public interface IResponseHelper
{
    MessageResponse CreateMessageResponse(string message);
    IdResponse<T> CreateIdResponse<T>(T id, string message);
    ResponseWithCode<T> CreateResponseWithCode<T>(HttpStatusCode statusCode, T? data = default(T));
    
    ResponseWithCode<dynamic> InternalServerErrorResponse();
}