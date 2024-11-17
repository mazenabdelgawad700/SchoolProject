using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            Console.WriteLine($"Middleware: {error.Message}");
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>()
            { Succeeded = false, Message = error?.Message };
            //TODO:: cover all validation errors
            switch (error)
            {
                case UnauthorizedAccessException e:
                    // custom application error
                    responseModel.StatusCode = HttpStatusCode.Unauthorized;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ValidationException e:
                    // custom validation error
                    responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    responseModel.StatusCode = HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case DbUpdateException e:
                    // can't update error
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;

                    break;
                case Exception e:
                    //if (e.Message.Contains("already"))
                    //{
                    //    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    //    response.StatusCode = (int)HttpStatusCode.BadRequest;

                    //    break;
                    //}
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                //responseModel.Message = e.Message;
                //responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                //responseModel.StatusCode = HttpStatusCode.InternalServerError;
                //response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //break;
                default:
                    // unhandled error
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }
}