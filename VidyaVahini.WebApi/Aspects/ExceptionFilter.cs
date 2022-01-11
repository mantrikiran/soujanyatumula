using System;
using Microsoft.AspNetCore.Mvc.Filters;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Entities.Response;
using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Infrastructure.Exception;
using Newtonsoft.Json;

namespace VidyaVahini.WebApi.Aspects
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        private readonly Response<ErrorDetails> _response;

        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
            _response = new Response<ErrorDetails>();
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.Message, context.Exception);

            if(context.Exception is VidyaVahiniException)
            {
                var exceptionDetails = JsonConvert.DeserializeObject<ErrorDetails>(context.Exception.Message);
                _response.Error.Message = exceptionDetails.Message;
                _response.Error.Code = exceptionDetails.Code;

            }
            else if (context.Exception is VidyaVahiniBadRequestException)
            {
                _response.Error.Message = context.Exception.Message;
                _response.Error.Code = "400";
            }
            else
            {
                _response.Error.Message = context.Exception.Message;
                _response.Error.Code = context.Exception is UnauthorizedAccessException ? "401" : "500";
            }
            
            context.HttpContext.Response.StatusCode = context.Exception is UnauthorizedAccessException ? 401 : 500;

            context.Result = new JsonResult(_response);
        }
    }
}
