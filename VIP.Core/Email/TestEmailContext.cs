using Easy.Net.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace VIP.Core.Email
{
    public class TestEmailContext : EmailContentBase
    {
        EmailHost _emailHost;
        public TestEmailContext(EmailHost emailHost)
        {
            _emailHost = emailHost;
        }
        public override string GetBody()
        {
            return "<p>这是一封测试邮件，如果你收到了，说明你的邮件服务器没有问题，可以正常使用。</p>";
        }

        public override System.Net.NetworkCredential GetCredential()
        {
            return new System.Net.NetworkCredential(_emailHost.UserName, _emailHost.PassWord);
        }

        public override IEnumerable<System.Net.Mail.MailAddress> GetReceivers()
        {
            return new List<MailAddress> { new MailAddress(_emailHost.EmailAddress, _emailHost.UserName) };
        }

        public override System.Net.Mail.MailAddress GetSender()
        {
            return new MailAddress(_emailHost.EmailAddress, _emailHost.UserName);
        }

        public override string GetSmtpHost()
        {
            return _emailHost.SmtpHost;
        }

        public override SmtpClient GetSmtpClient()
        {
            SmtpClient client = base.GetSmtpClient();
            client.Port = _emailHost.Port;
            client.EnableSsl = _emailHost.IsSSL;
            return client;
        }

        public override string GetSubject()
        {
            return "这是一封测试邮件，如果你收到了，说明你的邮件服务器没有问题，可以正常使用";
        }
    }
}
