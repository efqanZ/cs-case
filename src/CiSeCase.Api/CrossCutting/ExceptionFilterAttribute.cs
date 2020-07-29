using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CiSeCase.Api.CrossCutting
{
    public class ExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public ExceptionFilterAttribute()
        {

        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message });
            context.HttpContext.Response.StatusCode = (int)MapStatusCode(context.Exception); ;
            context.Exception = null;
        }

        private HttpStatusCode MapStatusCode(Exception ex)
        {
            // Status Codes
            if (ex is ArgumentNullException)
            {
                return HttpStatusCode.NotFound;
            }
            else if (ex is ValidationException)
            {
                return HttpStatusCode.BadRequest;
            }
            else if (ex is UnauthorizedAccessException)
            {
                return HttpStatusCode.Unauthorized;
            }
            else if (ex is DuplicateNameException)
            {
                return HttpStatusCode.Conflict;
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}