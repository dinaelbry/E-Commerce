using E_Commerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int durationInSec;

        public RedisCacheAttribute(int durationInSec = 90)
        {
            this.durationInSec = durationInSec;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get the cache service from the DI container
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();
            
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetAsync(cacheKey);

            // if data exissts in cache get from cache and skip endpoint
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                context.Result = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // if not exists in cache execute the endpoint and cache the response

            var executedContext = await next.Invoke();
            if (executedContext.Result is OkObjectResult{Value: not null } ok)
                await cacheService.SetAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(durationInSec));

        }



        private static string CreateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path).Append('?');
            foreach (var (key, value) in request.Query.OrderBy(q => q.Key))
            {
                keyBuilder.Append(key).Append('=').Append(value).Append('&');
            }
            return keyBuilder.ToString();
        }

    }
}
