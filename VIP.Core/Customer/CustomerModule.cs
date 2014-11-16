using Easy.IOCAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Customer
{
    public class CustomerModule : IModule
    {
        public void Load()
        {
            Container.Register(typeof(ICustomerService), typeof(CustomerService));
        }
    }
}
