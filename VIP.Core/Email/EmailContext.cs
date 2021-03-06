﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;
using System.Net.Mail;
using VIP.Core.Customer;

namespace VIP.Core.Email
{
    public class EmailContext : Easy.Net.Email.EmailContentBase
    {
        EmailHost _host;
        EmailMessage _emailMessage;
        public EmailContext(EmailMessage emailMessage, EmailHost host)
        {
            _emailMessage = emailMessage;
            _host = host;
        }
        public override string GetBody()
        {
            return _emailMessage.EmailContent;
        }

        public override System.Net.NetworkCredential GetCredential()
        {
            return new System.Net.NetworkCredential(_host.UserName, _host.PassWord);
        }

        public override IEnumerable<System.Net.Mail.MailAddress> GetReceivers()
        {
            List<System.Net.Mail.MailAddress> receivers = new List<MailAddress>();
            if (_emailMessage.Receiver.IsNotNullAndWhiteSpace())
            {
                _emailMessage.Status = 3;
                _emailMessage.Receiver.Split(';').Each(m =>
                {
                    if (m.IsNotNullAndWhiteSpace())
                    {
                        _emailMessage.Sended++;
                        receivers.Add(new MailAddress(m, m));
                    }
                });
            }
            else
            {
                ICustomerService customerService = Easy.Loader.CreateInstance<ICustomerService>();
                Random ran = new Random(DateTime.Now.Millisecond + DateTime.Now.Second * 1000);
                CustomerEntity entity = null;
                while (entity == null)
                {
                    entity = customerService.Get(ran.Next(0, (int)_emailMessage.Total));
                    if (entity.ID == _emailMessage.LastCustId)
                        entity = null;
                }
                _emailMessage.LastCustId = entity.ID;
                _emailMessage.Sended++;
                receivers.Add(new MailAddress(entity.Email, entity.FirstName));
                if (_emailMessage.Sended >= _emailMessage.Total)
                {
                    _emailMessage.Status = 3;
                }
                else
                {
                    _emailMessage.Status = 2;
                }

            }
            Easy.Loader.CreateInstance<IEmailService>().Update(_emailMessage);

            return receivers;
        }

        public override System.Net.Mail.MailAddress GetSender()
        {
            return new System.Net.Mail.MailAddress(_host.EmailAddress, _host.UserName);
        }

        public override string GetSmtpHost()
        {
            return _host.SmtpHost;
        }

        public override string GetSubject()
        {
            return _emailMessage.Title;
        }
        public override SmtpClient GetSmtpClient()
        {
            SmtpClient client = base.GetSmtpClient();
            client.Port = _host.Port;
            client.EnableSsl = _host.IsSSL;
            return client;
        }
    }
}
