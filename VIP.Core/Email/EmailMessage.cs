using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Email
{
    public class EmailMessage
    {
        public string Subject { get; set; }

        public string Receiver { get; set; }
        public int LastCustId { get; set; }
        public string EmailContent { get; set; }
    }
}
