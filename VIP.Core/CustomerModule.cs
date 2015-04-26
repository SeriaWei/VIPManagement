using Easy.IOCAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VIP.Core.Customer;
using VIP.Core.Email;

namespace VIP.Core
{
    public class CustomerModule : IModule
    {
        public void Load()
        {
            Container.Register(typeof(ICustomerService), typeof(CustomerService));
            Container.Register(typeof(IEmailService), typeof(EmailService));
            Container.Register(typeof(IEmailHostService), typeof(EmailHostService));
        }
    }
}
