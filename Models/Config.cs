namespace EmailSender.Models
{
    public class Config
    {
        public string MailSender { get; set; }
        public string MailSubject { get; set; }
        public string MailCC { get; set; }
        public string MailBCC { get; set; }
        public string FilePath { get; set; }
        public string MailURL { get; set; }
    }
}

