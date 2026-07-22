using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiHandson3.Filters;

// Implements IExceptionFilter to catch all unhandled exceptions
public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Capture the exception details
        var exception = context.Exception;
        var errorMessage = $"[{DateTime.Now}] Exception: {exception.Message}\nStackTrace: {exception.StackTrace}\n";

        // Write exception to a log file
        var logPath = Path.Combine(Directory.GetCurrentDirectory(), "exception_log.txt");
        File.AppendAllText(logPath, errorMessage);

        // Set result to 500 Internal Server Error
        context.Result = new ObjectResult(new
        {
            StatusCode = 500,
            Message    = "An internal server error occurred.",
            Detail     = exception.Message
        })
        {
            StatusCode = 500
        };

        // Mark exception as handled
        context.ExceptionHandled = true;
    }
}
