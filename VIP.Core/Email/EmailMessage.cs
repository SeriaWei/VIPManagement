using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Email
{
    [Easy.MetaData.DataConfigure(typeof(EmailMessageMetaData))]
    public class EmailMessage : EditorEntity
    {
        public int ID { get; set; }
        public string Receiver { get; set; }
        public string EmailContent { get; set; }
        public int LastCustId { get; set; }
        public int Sended { get; set; }
        public long Total { get; set; }
    }
    class EmailMessageMetaData : ViewMetaData<EmailMessage>
    {
        protected override void DataConfigure()
        {
            DataTable("EmailMessage");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.Description).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.Title).AsTextBox();
            ViewConfig(m => m.Sended).AsTextBox();
            ViewConfig(m => m.CreateDate).AsTextBox().SearchAble(false);
            ViewConfig(m => m.LastUpdateDate).AsTextBox().SearchAble(false);
            ViewConfig(m => m.Status).AsDropDownList().DataSource(Easy.Constant.SourceType.Dictionary);
            ViewConfig(m => m.Title).AsTextBox();
            ViewConfig(m => m.Description).AsHidden();
        }
    }

}
