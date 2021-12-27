using System;
using Groot.API.Infrastructure;
using Groot.Model;
using Groot.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Groot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IUserService userService;

        public LoginController(IMemoryCache _memoryCache, IUserService _userService)
        {
            memoryCache = _memoryCache;
            userService = _userService;
        }

        [HttpPost]//Post

        public General<bool> Login([FromBody] Groot.Model.Login.LoginViewModel loginUser)
        {
            General<bool> response = new() { Entity = false };
            General<Groot.Model.User.UserViewModel> _response = userService.Login(loginUser);
            if (_response.IsSuccess)
            {
                if (!memoryCache.TryGetValue(CacheKeys.Login, out Model.User.UserViewModel _loginUser))
                {
                    var cacheOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(1),// after one hour, erased from cache. 
                        Priority = CacheItemPriority.Normal
                    };
                    memoryCache.Set(CacheKeys.Login, _response.Entity, cacheOptions);
                }
                response.Entity = true;
                response.IsSuccess = true;
            }
                
            return response;
            //firstname, lastname and password are getting from frontside.
            //service will be written. then Is login success?Yes. keep in that data on cache.
        }

       
    }
}