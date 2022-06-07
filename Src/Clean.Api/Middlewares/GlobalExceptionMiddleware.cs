using Clean.Api.Controllers.v1.Commons.Models;
using Clean.Domain.Resources.Exceptions;
using System.Text.Json;

namespace Clean.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                if (context.Response.StatusCode == 401)
                    throw new CleanException(Domain.Resources.Exceptions.Commons.ExceptionLevelEnum.Application, Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.Unauthorize, "برای استفاده از این سرویس نیاز به احراز هویت می باشد.");
            }
            catch (CleanException ex)
            {
                int status = 200;

                switch (ex.Type)
                {
                    case Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.NotFound:
                        status = 404;
                        break;
                    case Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.Unauthorize:
                        status = 401;
                        break;
                    case Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.Douplicate:
                    case Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.BadArgument:
                    case Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.Validation:
                    case Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.ApiFailed:
                        status = 400;
                        break;
                    default:
                        status = 500;
                        break;
                }

                var errorResult = JsonSerializer.Serialize(new BaseResponse<object>(false, (int)ex.Type, null, new BaseError(ex.Message)));
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = status;
                await context.Response.WriteAsync(errorResult);

                //log error
            }
            catch (Exception ex)
            {
                var errorResult = JsonSerializer.Serialize(new BaseResponse<object>(false, 500 , null, new BaseError(ex.Message)));
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(errorResult);

                //log
            }
        }
    }
}
