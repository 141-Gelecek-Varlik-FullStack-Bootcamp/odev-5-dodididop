using System;
using System.Linq;
using Groot.DB.Entities.DatabaseContext;
using Groot.Model;
using AutoMapper;

namespace Groot.Service.User
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;

        public UserService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public bool Login(string firstName, string lastName, string email,  string password)
        {
            bool result = false;
            using (var srv = new GrootContext())
            {
                result = srv.User.Any(a => !a.IsDeleted && a.IsActive && a.LastName ==lastName && a.Email == email &&
                a.FirstName == firstName && a.Password == password);
            }
                return result;
        }

        public General<Groot.Model.User.UserViewModel> Insert(Groot.Model.User.UserViewModel newUser)
        {
            var result = new General<Groot.Model.User.UserViewModel>() { IsSuccess = false };
            var model = mapper.Map<Groot.DB.Entities.User>(newUser); 
            using (var srv = new GrootContext())
            { 
                
                model.IdateTime = DateTime.Now;
                srv.User.Add(model);
                srv.SaveChanges();
                result.Entity = mapper.Map<Groot.Model.User.UserViewModel>(model);
                result.IsSuccess = true;

            }
            return result;
        }

        
    }
}
