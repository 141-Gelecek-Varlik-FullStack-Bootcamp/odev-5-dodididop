using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Groot.API.Infrastructure
{

    public class BaseController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;

        public BaseController(IMemoryCache _memoryCache)
        {
            memoryCache = _memoryCache;
        }

        public Model.User.UserViewModel CurrentUser
        {
            get
            {
                return GetCurrentUser();
            }
        }

        private Model.User.UserViewModel GetCurrentUser()
        {
            var response = new Model.User.UserViewModel();
            if(memoryCache.TryGetValue(CacheKeys.Login, out Model.User.UserViewModel loginUser))
            {
                response = loginUser;
            }
            return loginUser;
        }
    }
}