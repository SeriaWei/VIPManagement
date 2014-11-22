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
        public string ID { get; set; }
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
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.ActionType).Ignore();
            DataConfig(m => m.Age).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.FirstName).AsTextBox().Required().SearchAble();
            ViewConfig(m => m.LastName).AsTextBox().Required();
            ViewConfig(m => m.LastActivedDate).AsTextBox().FormatAsDate().ReadOnly();
            ViewConfig(m => m.Age).AsTextBox().ReadOnly().HideInGrid().SearchAble(false);
        }
    }

}
