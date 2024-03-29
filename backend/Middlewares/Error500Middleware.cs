namespace backend.Middlewares
{
    public class Error500Middleware
    {
        private readonly RequestDelegate _next;

        public Error500Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(context, ex);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            
            context.Response.StatusCode = 500;

            await context.Response.WriteAsync("Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
        }
    }
}
