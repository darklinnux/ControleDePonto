using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace backend.Exceptions
{
    public class ErrorHandlingFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    if (ex is ErrorServiceException errorEx)
                    {
                        context.Result = new BadRequestObjectResult(new { error = errorEx.Message });
                        return;
                    }
                }

                // Se não for uma ErrorServiceException, lança a exceção novamente
                throw ae;
            }
        }
    }
}
