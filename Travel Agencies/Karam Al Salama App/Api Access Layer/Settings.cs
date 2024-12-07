using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SalamaTravelDAL
{
    public static class Settings
    {
        public static readonly HttpClient HttpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7116/api/") // تغيير هذا للـ API الفعلي
        };
        public static async Task SendEmail(string Subject, string Body, string EmailToSend)
        {
            try
            {
                // إعدادات البريد الإلكتروني
                string fromAddress = "commercialprokerskaramalsalama@gmail.com"; // بريدك الإلكتروني
                string appPassword = "tabr lqzr mavn omvc"; // كلمة مرور التطبيق أو كلمة المرور العادية إذا كان "الوصول للتطبيقات الأقل أمانًا" مفعلاً
                string toAddress = EmailToSend; // البريد الإلكتروني للمستلم
                string subject = Subject;
                string fromName = "Karam Al-Salama Commercial Prokers";
                string body = Body;

                // تكوين SMTP
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress, appPassword)
                };

                MailAddress from = new MailAddress(fromAddress, fromName);
                MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    From = from,
                    Body = body
                };

                // إرسال الرسالة
                smtp.Send(message);
            }
            catch (Exception)
            {
            }
        }

    }
}
