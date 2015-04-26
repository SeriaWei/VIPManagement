using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Email
{
    public interface IEmailService : IServiceBase<EmailMessage>
    {
        void Save(EmailMessage message);
    }
}
