using log4net;
using log4net.Config;
using System.Reflection;
using System.Text;

namespace Clean.Api.Filters
{
    public class CleanLog
    {
        private readonly RequestDelegate _next;
        private readonly ILog _log;

        public CleanLog(RequestDelegate next)
        {
            var logRepo = LogManager.GetLoggerRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepo, new FileInfo("web.config"));
            _next = next;
            _log = LogManager.GetLogger(typeof(CleanLog));
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path.ToString().ToLower().Contains("swagger"))
                await _next.Invoke(context);
            else
            {
                var originalResponse = context.Response.Body;
                string requestUrl = $"{context.Request.Path}/{context.Request.QueryString}";
                context.Request.EnableBuffering();

                using (var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    string requestText = await streamReader.ReadToEndAsync();
                    context.Request.Body.Seek(0, SeekOrigin.Begin);

                    MemoryStream responseBodyStream = new();
                    await _next.Invoke(context);

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    string responseText = await new StreamReader(responseBodyStream, Encoding.UTF8).ReadToEndAsync();

                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    if (!string.IsNullOrWhiteSpace(context.Response.ContentType) && context.Response.ContentType.ToLower().Contains("application/json"))
                    {
                        if (context.Response.StatusCode == 200)
                            _log.Info($"{requestUrl} => {context.Response.StatusCode} => {requestText}");
                        else
                            _log.Error($"{requestUrl} => {context.Response.StatusCode} => {requestText}");
                    }

                    await context.Response.Body.CopyToAsync(originalResponse);
                }
            }
        }
    }
}
