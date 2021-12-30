namespace Groot.Service.Common
{
    public class AppSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; } 
        public string MailPassword { get; set; }
        public string MailSendFrom { get; set; }
        public string MailSendFromName { get; set; }
    }
}
