using Groot.Model.User;

namespace Groot.Service.Common
{
    public class WelcomeMailJob :IWelcomeMailJob
    {
        private readonly IMailSender mailSender;
        public WelcomeMailJob(IMailSender _mailSender)
        {
            mailSender = _mailSender;
        }
        public void SendMail(WelcomeMailViewModel newUser)
        {
            mailSender.Send("Hoşgeldiniz", $"Hoşgeldin yoldaş "+newUser.FirstName, new System.Collections.Generic.List<string> { newUser.Email });
        }
    }
}
