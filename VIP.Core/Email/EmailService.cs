using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VIP.Core.Customer;

namespace VIP.Core.Email
{
    public class EmailService : ServiceBase<EmailMessage>, IEmailService
    {
        public void Save(EmailMessage message)
        {
            message.Total = Easy.Loader.CreateInstance<ICustomerService>().Count(new Easy.Data.DataFilter());
            if (message.Status == 1)
            {
                message.Sended = 0;
            }
            if (message.ID > 0)
            {
                Update(message);
            }
            else
            {
                Add(message);
            }
        }
    }
}
