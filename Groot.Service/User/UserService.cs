using System;
using System.Linq;
using AutoMapper;
using Groot.DB.Entities.DatabaseContext;
using Groot.Model;
using Hangfire;
using Groot.Service.Common;
using Groot.Model.User;

namespace Groot.Service.User
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IWelcomeMailJob welcomeMailJob;
        

        public UserService(IMapper _mapper, IWelcomeMailJob _welcomeMailJob)
        {
            mapper = _mapper;
            welcomeMailJob = _welcomeMailJob;

        }
        public General<Groot.Model.User.UserViewModel> Login(Groot.Model.Login.LoginViewModel loginUser)
        {
            General<Groot.Model.User.UserViewModel> result = new();
            try
            {
                using (var srv = new GrootContext())
                {
                    var _data = srv.User.FirstOrDefault(a => a.LastName == loginUser.LastName &&
                    a.FirstName == loginUser.FirstName && a.Password == loginUser.Password);
                    if (_data is not null)
                    {
                        result.IsSuccess = true;
                        result.Entity = mapper.Map<Groot.Model.User.UserViewModel>(_data);
                    }
                }
            }
            catch (Exception ex)
            {
                result.ExceptionMessage = "Beklenmeyen bir hata oluştu.";
            }
                return result;
        }

        public General<Groot.Model.User.UserViewModel> Insert(Groot.Model.User.UserViewModel newUser)
        {
            var result = new General<Groot.Model.User.UserViewModel>() { IsSuccess = false };
            try
            {
                var model = mapper.Map<Groot.DB.Entities.User>(newUser);
                using (var srv = new GrootContext())
                {
                    model.IdateTime = DateTime.Now;
                    srv.User.Add(model);
                    srv.SaveChanges();
                    result.Entity = mapper.Map<Groot.Model.User.UserViewModel>(model);
                    result.IsSuccess = true;
                }
                var welcomeUser = new WelcomeMailViewModel() { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email};
                BackgroundJob.Schedule(() => welcomeMailJob.SendMail(welcomeUser), TimeSpan.FromDays(1));
            }
            catch (Exception ex)
            {
                result.ExceptionMessage ="Beklenmeyen bir hata oluştu.";
            }
            
            return result;
        }        
    }
}
