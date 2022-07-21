namespace EmailSender.Models
{
    public class MailSenderDTO
    {

        public string EmailSender { get; set; }
        public string EmailReciever { get; set; }
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public string EmailCc { get; set; }
        public string EmailBcc { get; set; }
        public List<AttachmentValues> EmailAttachments { get; set; }
    }
    public class AttachmentValues
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string EmailAttachment { get; set; }
    }

}
