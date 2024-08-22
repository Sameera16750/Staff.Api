using System.Net;
using Staff.Application.Helpers.ResourceHelper;
using Staff.Application.Models.Response.Common;
using Staff.Core.Constants;

namespace Staff.Application.Helpers.ResponseHelper;

public class ResponseHelper(IMessageResourceHelper messageResourceHelper) : IResponseHelper
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

    public ResponseWithCode<dynamic> InternalServerErrorResponse()
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Response = CreateMessageResponse(Constants.Messages.Error.InternalServerError)
        };
    }

    public ResponseWithCode<dynamic> NotFoundErrorResponse()
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.NotFound,
            Response = CreateMessageResponse(Constants.Messages.Error.DataNotFound)
        };
    }

    public ResponseWithCode<dynamic> SaveFailedResponse()
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Response = CreateMessageResponse(Constants.Messages.Error.SaveFailed)
        };
    }

    public ResponseWithCode<dynamic> UpdateFailedResponse()
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Response = CreateMessageResponse(Constants.Messages.Error.UpdateFailed)
        };
    }

    public ResponseWithCode<dynamic> DeleteFailedErrorResponse()
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Response = CreateMessageResponse(Constants.Messages.Error.DeleteFailed)
        };
    }

    public ResponseWithCode<dynamic> SaveSuccessResponse<T>(T id)
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.Created,
            Response = CreateIdResponse(id, Constants.Messages.Success.SaveSuccess)
        };
    }

    public ResponseWithCode<dynamic> DeleteSuccessResponse<T>(T id)
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.OK,
            Response = CreateIdResponse(id, Constants.Messages.Success.DeleteSuccess)
        };
    }

    public ResponseWithCode<dynamic> UpdateSuccessResponse<T>(T id)
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.Created,
            Response = CreateIdResponse(id, Constants.Messages.Success.UpdateSuccess)
        };
    }

    public ResponseWithCode<dynamic> BadRequest(string message)
    {
        return new ResponseWithCode<dynamic>
        {
            StatusCode = HttpStatusCode.BadRequest,
            Response = CreateMessageResponse(message)
        };
    }
}