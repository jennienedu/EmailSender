using EmailSender.Interfaces;
using EmailSender.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Configuration;

namespace EmailSender.Services
{
    public class MailService : IMailService
    {
        private readonly Config _config;

        public MailService(IOptions<Config> appSettings)
        {
            _config = appSettings.Value;
        }


        /// <summary>
        /// This method gets the Mobile App and Website customers details 
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public async Task<MailResponse> SendMailToCustomers(CustomersDTO customer)
        {
            var emailRes = new MailResponse();

            var bodyTemplate = string.Empty;
            var body = string.Empty;
            string path = _config.FilePath;

            if (!string.IsNullOrEmpty(path))
                try
                {
                    if (customer.Username != string.Empty)
                    {
                        bodyTemplate = $"{System.Configuration.ConfigurationManager.AppSettings.Get("FilePath")}WebsiteTemplate.cshtml";
                        path = path + bodyTemplate;
                        body = File.ReadAllText(path);
                        body = body.Replace("FullName", customer.FullName);
                    }
                    else
                    {
                        bodyTemplate = $"{System.Configuration.ConfigurationManager.AppSettings.Get("FilePath")}MobileAppTemplate.html";
                        path = path + bodyTemplate;
                        body = File.ReadAllText(path);
                        body = body.Replace("FullName", customer.FullName);
                    }

                    var mailSender = new MailSenderDTO
                    {
                        EmailSender = _config.MailSender,
                        EmailReciever = customer.Email,
                        EmailBody = body,
                        EmailSubject = _config.MailSubject,
                        EmailCc = _config.MailCC,
                        EmailBcc = _config.MailBCC,
                    };
                    emailRes = await SendEmail(mailSender);
                }
                catch (Exception e)
                {
                    throw e;
                }
            else return new MailResponse { ActionMessage = "File path cannot be empty!", ActionStatus = false };

            return emailRes;

        }


        /// <summary>
        /// This method consumes the mail endpoint and sends the mails to customers
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public async Task<MailResponse> SendEmail(MailSenderDTO request)
        {
            var resp = new MailResponse();
            string value = string.Empty;
            var emailAPI = _config.MailURL;
            try
            {
                HttpClient client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
                client.Timeout = TimeSpan.FromMinutes(5);
                var response = await client.PostAsync(emailAPI, content);

                if (response.IsSuccessStatusCode)
                {
                    resp.ActionMessage = "Called Successfully";
                    value = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<MailResponse>(value);
                }
                else
                {
                    resp.ActionMessage = "Not 200";
                    value = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<MailResponse>(value);
                }
            }
            catch (Exception ex)
            {
                resp.ActionMessage = ex.StackTrace + ex.Message.ToString();
                ex.Message.ToString();
            }
            return resp;
        }
    }
}
