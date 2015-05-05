using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;

namespace VIP.Core.Customer
{
    [DataConfigure(typeof(CustomerEntityMetaData))]
    public class CustomerEntity : HumanBase
    {
        public int ID { get; set; }
        /// <summary>
        /// 消费总额
        /// </summary>
        public decimal Expendamount { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Surplus { get; set; }
        /// <summary>
        /// 总积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 剩余积分
        /// </summary>
        public int SurplusIntegral { get; set; }
        /// <summary>
        /// 已兑积分
        /// </summary>
        public int ExchangedIntegral { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string CardID { get; set; }
        public DateTime? LastActivedDate { get; set; }
        public DateTime? RegistDate { get; set; }
    }
    class CustomerEntityMetaData : ViewMetaData<CustomerEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Customer");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.Age).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.LastName).AsHidden();
            ViewConfig(m => m.EnglishName).AsHidden();
            ViewConfig(m => m.Email).AsTextBox().Email().Order(0).SetDisplayName("邮箱");
            ViewConfig(m => m.FirstName).AsTextBox().Required().SearchAble().Order(1).SetDisplayName("姓名");
            ViewConfig(m => m.CardID).AsHidden();
            ViewConfig(m => m.MobilePhone).AsTextBox().SearchAble().Order(3).SetDisplayName("手机");
            ViewConfig(m => m.RegistDate).AsTextBox().SearchAble(false).Order(4).ReadOnly().SetDisplayName("注册日期").Format("yyyy/MM/dd HH:mm");
            ViewConfig(m => m.LastActivedDate).AsHidden();
            ViewConfig(m => m.Expendamount).AsHidden();
            ViewConfig(m => m.Surplus).AsHidden();
            ViewConfig(m => m.Integral).AsHidden();
            ViewConfig(m => m.SurplusIntegral).AsHidden();
            ViewConfig(m => m.ExchangedIntegral).AsHidden();
            ViewConfig(m => m.Age).AsHidden();
            ViewConfig(m => m.NickName).AsHidden();
            ViewConfig(m => m.Birthday).AsHidden();
            ViewConfig(m => m.Address).AsHidden();
            ViewConfig(m => m.School).AsHidden();
            ViewConfig(m => m.ZipCode).AsHidden();
            ViewConfig(m => m.Telephone).AsHidden();
            ViewConfig(m => m.Profession).AsHidden();
            ViewConfig(m => m.MaritalStatus).AsHidden();
            ViewConfig(m => m.Hobby).AsHidden();
            ViewConfig(m => m.Birthplace).AsHidden();
            ViewConfig(m => m.Description).AsMutiLineTextBox().SetDisplayName("描述").SearchAble(false);
            ViewConfig(m => m.Status).AsHidden();
            ViewConfig(m => m.Sex).AsHidden();
        }
    }

}
