using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Customer
{
    public class CustomerService : ServiceBase<CustomerEntity>, ICustomerService
    {
        public override void Add(CustomerEntity item)
        {
            item.ID = Guid.NewGuid().ToString();
            item.RegistDate = DateTime.Now;
            base.Add(item);
        }
    }
}
