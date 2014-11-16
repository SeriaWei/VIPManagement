using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Customer
{
    public class CustomerService : ServiceBase<CustomerEntity>, ICustomerService
    {
    }
}
