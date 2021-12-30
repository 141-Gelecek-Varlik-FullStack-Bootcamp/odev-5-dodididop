using System;
using Groot.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Groot.API.Infrastructure
{
    public class LoginFilter : IActionFilter
    {
        private readonly IMemoryCache memoryCache;
        private readonly IUserService userService;

        public LoginFilter(IMemoryCache _memoryCache, IUserService _userService)
        {
            memoryCache = _memoryCache;
            userService = _userService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
            if (!memoryCache.TryGetValue(CacheKeys.Login, out Model.User.UserViewModel response))
            {
                context.Result = new BadRequestObjectResult("User is null");
            }
            return;

        }
    }
}
