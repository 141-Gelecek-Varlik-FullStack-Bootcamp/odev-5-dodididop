using AutoMapper;
using Groot.API.Infrastructure;
using Groot.Model;
using Groot.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Groot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        
        public UserController(IUserService _userService, IMapper _mapper, IMemoryCache _memoryCache) : base(_memoryCache)
        {
            userService = _userService;
            mapper = _mapper; 
        }

        [HttpPost]
        [ServiceFilter(typeof(LoginFilter))]

        public General<Groot.Model.User.UserViewModel> Insert([FromBody] Groot.Model.User.UserViewModel newUser)
        {

            return userService.Insert(newUser);
            

        }
    }

    
}