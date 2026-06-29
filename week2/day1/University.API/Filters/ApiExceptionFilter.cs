using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using University.Core.Exceptions;

namespace University.API.Filters
    {
    public class ApiExceptionFilter : IExceptionFilter
        {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter ( ILogger<ApiExceptionFilter> logger )
            {
            _logger = logger;
            }

        public void OnException ( ExceptionContext context )
            {
            if ( context.Exception is NotFoundException )
                {
                context.Result = Response (context.Exception.Message, "item not found", StatusCodes.Status404NotFound);
                return;
                }

            if ( context.Exception is BusinessException businessException )
                {
                if ( businessException.Errors.Any () )
                    context.Result = Response (businessException.Errors, "One or more business validation errors occurred.", StatusCodes.Status400BadRequest);
                else
                    context.Result = Response (businessException.Message, "One or more business validation errors occurred.", StatusCodes.Status400BadRequest);
                return;
                }

            if ( context.Exception is ArgumentNullException )
                {
                context.Result = Response (context.Exception.Message, "missing data", StatusCodes.Status400BadRequest);
                return;
                }

            if ( context.Exception is UnauthorizedAccessException )
                {
                context.Result = Response (context.Exception.Message, "Unauthorized", StatusCodes.Status403Forbidden);
                return;
                }

            _logger.LogError (context.Exception, "Unhandled exception occurred");
            context.Result = Response (context.Exception.Message, "Internal Server Error", StatusCodes.Status500InternalServerError);
            }

        private ObjectResult Response ( string message, string title, int status )
            {
            return new ObjectResult (new
                {
                statusCode = status,
                message = title,
                result = message,
                isError = true
                })
                {
                StatusCode = status
                };
            }

        private ObjectResult Response ( Dictionary<string, List<string>> errors, string title, int status )
            {
            return new ObjectResult (new
                {
                statusCode = status,
                message = title,
                result = errors,
                isError = true
                })
                {
                StatusCode = status
                };
            }
        }
    }