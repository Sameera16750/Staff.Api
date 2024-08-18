using System.Net;
using Staff.Application.Helpers.ResourceHelper;
using Staff.Application.Models.Response.Common;

namespace Staff.Application.Helpers.ResponseHelper;

public class ResponseHelper(IMessageResourceHelper messageResourceHelper):IResponseHelper
{
    public MessageResponse CreateMessageResponse(string message)
    {
        return new MessageResponse()
        {
            Message = messageResourceHelper.GetResource(message)
        };
    }

    public IdResponse<T> CreateIdResponse<T>(T id, string message)
    {
        return new IdResponse<T>
        {
            Id = id,
            Message = messageResourceHelper.GetResource(message)
        };
    }

    public ResponseWithCode<T> CreateResponseWithCode<T>(HttpStatusCode statusCode, T? data = default(T))
    {
        return new ResponseWithCode<T>
        {
            StatusCode = statusCode,
            Response = data
        };
    }
}