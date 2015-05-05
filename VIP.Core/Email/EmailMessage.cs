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
            ViewConfig(m => m.Title).AsTextBox().SetDisplayName("主题");
            ViewConfig(m => m.Sended).AsTextBox().SetDisplayName("发送量");
            ViewConfig(m => m.CreateDate).AsTextBox().SearchAble(false).SetDisplayName("创建日期");
            ViewConfig(m => m.LastUpdateDate).AsTextBox().SearchAble(false).SetDisplayName("最后发送日期");
            ViewConfig(m => m.Status).AsDropDownList().DataSource(Easy.Constant.SourceType.Dictionary).SetDisplayName("发送状态");
            ViewConfig(m => m.Description).AsHidden();
        }
    }

}
