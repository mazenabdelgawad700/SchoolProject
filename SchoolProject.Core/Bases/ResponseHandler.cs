using Microsoft.Extensions.Localization;
using SchoolProject.Core.SharedResourcesHelper;
using System.Net;

namespace SchoolProject.Core.Bases;
public class ResponseHandler
{
    private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;

    #region Constructors
    public ResponseHandler(IStringLocalizer<SharedResourcesLocalization> localizer)
    {
        _localizer = localizer;
    }
    #endregion

    #region Handle Functions
    public Response<T> Success<T>(T entity)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            //Message = "Success",
            Message = _localizer[LocalizationSharedResourcesKeys.Success],
        };
    }
    public Response<T> Unauthorized<T>()
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Succeeded = true,
            //Message = "UnAuthorized"
            Message = _localizer[LocalizationSharedResourcesKeys.UnAuthorized]
        };
    }
    public Response<T> Failed<T>()
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.ExpectationFailed,
            Succeeded = false,
            Message = _localizer[LocalizationSharedResourcesKeys.ExpectationFailed]
        };
    }
    public Response<T> BadRequest<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = message ?? _localizer[LocalizationSharedResourcesKeys.BadRequest]
        };
    }
    public Response<T> UnprocessableEntity<T>(string message = null!)
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            //Message = "UnprocessableEntity"
            Message = message ?? _localizer[LocalizationSharedResourcesKeys.UnprocessableEntity]
        };
    }
    public Response<T> NotFound<T>()
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.NotFound,
            Succeeded = false,
            //Message = "Not Found",
            Message = _localizer[LocalizationSharedResourcesKeys.NotFound]
        };
    }
    public Response<T> Created<T>(T entity)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.Created,
            Succeeded = true,
            //Message = "Created",
            Message = _localizer[LocalizationSharedResourcesKeys.Created],
        };
    }
    public Response<T> Updated<T>()
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            //Message = "Updated",
            Message = _localizer[LocalizationSharedResourcesKeys.Updated],
        };
    }
    public Response<T> Deleted<T>()
    {
        return new Response<T>()
        {
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            //Message = "Deleted",
            Message = _localizer[LocalizationSharedResourcesKeys.Deleted],
        };
    }
    #endregion
}

