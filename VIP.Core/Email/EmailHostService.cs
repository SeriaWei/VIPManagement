using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Email
{
    public class EmailHostService : ServiceBase<EmailHost>, IEmailHostService
    {
        public void Save(EmailHost emailHost)
        {
            if (emailHost.ID == 0)
            {
                this.Add(emailHost);
            }
            else
            {
                this.Update(emailHost);
            }
        }
    }
}
