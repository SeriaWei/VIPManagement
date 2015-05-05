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
            item.RegistDate = DateTime.Now;
            if (this.Count(new Easy.Data.DataFilter().Where("Email", Easy.Data.OperatorType.Equal, item.Email)) == 0)
            {
                base.Add(item);
            }
        }

        public override bool Update(CustomerEntity item, params object[] primaryKeys)
        {
            if (this.Count(new Easy.Data.DataFilter()
                .Where("Email", Easy.Data.OperatorType.Equal, item.Email)
                .Where("ID", Easy.Data.OperatorType.NotEqual, item.ID)) == 0)
            {
                base.Update(item);
            }
            return true;
        }
    }
}
