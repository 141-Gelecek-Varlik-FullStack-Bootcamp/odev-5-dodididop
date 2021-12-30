using Groot.Model.User;

namespace Groot.Service.Common
{
    public interface IWelcomeMailJob
    {
        public void SendMail(WelcomeMailViewModel newUser);
    }
}
