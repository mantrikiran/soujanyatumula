using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using VidyaVahini.Infrastructure.Contracts;

namespace VidyaVahini.WebApi.Aspects
{
    public class ControllerActionProfilingFilter : IActionFilter
    {
        private readonly ILogger _logger;

        private Stopwatch stopwatch = new Stopwatch();

        public ControllerActionProfilingFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();
            _logger.LogInformation(
                $"Action {context.ActionDescriptor.DisplayName} " +
                $"executed in {stopwatch.Elapsed.ToString()} " +
                $"RequestId: {context.HttpContext.TraceIdentifier}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation(
                $"Profiling {(context.Controller as ControllerBase).Request.Path} " +
                $"RequestId: {context.HttpContext.TraceIdentifier}");
                stopwatch = Stopwatch.StartNew();
        }
    }
}
