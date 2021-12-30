using System.Collections.Generic;

namespace Groot.Service.Common
{
    public interface IMailSender
    {
        bool Send(string subject, string body, List<string> addresses);    
    }
}
