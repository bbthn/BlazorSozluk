using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BlazorSozluk.Api.WebApi.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtension 
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app
            ,bool includeExceptionDetails = false
            ,bool useDefaultHandlingResponse = true
            ,Func<HttpContext, Exception, Task> handleException = null)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(context =>
                {
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                    if (!useDefaultHandlingResponse && handleException is null)
                        throw new ArgumentNullException(nameof(handleException),
                            $"{handleException} cannot be null when {useDefaultHandlingResponse} is false");

                    if (!useDefaultHandlingResponse && handleException is not null)
                        return handleException(context, exceptionObject.Error);

                    return DefaultHandleException(context, exceptionObject.Error, includeExceptionDetails);

                });

            });

            return app;
        }


        private static async Task DefaultHandleException(HttpContext context, Exception exception, bool includeExceptionDetails)
        {
            HttpStatusCode statuscode = HttpStatusCode.InternalServerError;
            string message = "Internal Server error occured!";

            if(exception is UnauthorizedAccessException)
            {
                statuscode = HttpStatusCode.Unauthorized;
            }
            if(exception is DatabaseValidationException)
            {
                statuscode = HttpStatusCode.BadRequest;
                var validationResponse = new ValidationResponseModel(exception.Message);
                await WriteResponse(context, statuscode, validationResponse);
                return;
            }

            var res = new
            {
                HttpStatusCode = (int)statuscode,
                Detail = includeExceptionDetails ? exception.ToString() : message,

            };

            await WriteResponse(context, statuscode, res);


        }

        private static async Task WriteResponse(HttpContext context, HttpStatusCode httpStatusCode, object responseObj)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.WriteAsJsonAsync(responseObj);
        }
    }
}
