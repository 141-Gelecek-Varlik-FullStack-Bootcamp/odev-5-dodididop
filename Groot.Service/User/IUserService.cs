using Groot.Model;
namespace Groot.Service.User
{
    public interface IUserService
    {
        public General<Groot.Model.User.UserViewModel> Login(Groot.Model.Login.LoginViewModel loginUser);
        public General<Groot.Model.User.UserViewModel> Insert(Groot.Model.User.UserViewModel newUser);

    }
}