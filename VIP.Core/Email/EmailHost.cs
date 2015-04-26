using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Email
{
    [DataConfigure(typeof(EmailHostMetaData))]
    public class EmailHost
    {
        public int ID { get; set; }
        public string SmtpHost { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public long UseTimes { get; set; }
        public bool IsEnable { get; set; }
    }

    class EmailHostMetaData : DataViewMetaData<EmailHost>
    {
        protected override void DataConfigure()
        {
            DataTable("EmailHost");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.PassWord).AsHidden();
            ViewConfig(m => m.SmtpHost).AsTextBox().SearchAble(false);
            ViewConfig(m => m.Port).AsTextBox().SearchAble(false);
            ViewConfig(m => m.IsSSL).AsTextBox().SearchAble(false);
            ViewConfig(m => m.EmailAddress).AsTextBox().SearchAble(false);
            ViewConfig(m => m.UserName).AsTextBox().SearchAble(false);
            ViewConfig(m => m.UseTimes).AsTextBox().SearchAble(false);
            ViewConfig(m => m.IsEnable).AsCheckBox().SearchAble(false);
        }
    }

}
