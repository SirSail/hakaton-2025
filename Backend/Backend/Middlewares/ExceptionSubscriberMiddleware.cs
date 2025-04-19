using API.Exceptions;
using Application.Authorization.Exceptions;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace API.Middlewares
{
    public class ExceptionSubscriberMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionSubscriberMiddleware> _logger;

        public ExceptionSubscriberMiddleware(
            RequestDelegate next,
            ILogger<ExceptionSubscriberMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ResourceNotFoundException e)
            {
                LogException(e);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                var responseObj = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = e.Message
                };
                await context.Response.WriteAsync(responseObj.ToString());
            }
            catch(BadCredentialsException e)
            {
                LogException(e);
                var errorResponse = new
                {
                    errorStatus = StatusCodes.Status400BadRequest,
                    message = e.Message
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(errorResponse, options: new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower });
            }
            catch(RequestValidationException e)
            {
                LogException(e);
                var errorResponse = new
                {
                    errorStatus = StatusCodes.Status400BadRequest,
                    message = e.Message
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(errorResponse, options: new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower });
            }

            catch(Exception e) 
            {
                LogException(e);
                throw;
            }
        }

        private void LogException(Exception e)
        {
            _logger.LogError($">>{DateTime.Now.ToString()} Wystąpił błąd {e.GetType()} --> {e.Message}");
        }


    }
}
