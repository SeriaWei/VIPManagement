using Easy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VIP.Core.Customer
{
    /// <summary>
    /// CustomerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerPanel : UserControl
    {
        Easy.Data.Pagination page;
        public CustomerPanel()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            page = new Easy.Data.Pagination { PageIndex = 0, PageSize = 20 };
            ICustomerService service = Loader.CreateInstance<ICustomerService>();
            listPanel.ModelType = typeof(CustomerEntity);
            listPanel.DataSource = service.Get(new Easy.Data.DataFilter(), page);
            paginal.SetPage(page);
            paginal.PrevPageClick += (p) =>
            {
                page.PageIndex = p;
                listPanel.DataSource = service.Get(new Easy.Data.DataFilter(), page);
                paginal.SetPage(page);
            };
            paginal.NextPageClick += (p) =>
            {
                page.PageIndex = p;
                listPanel.DataSource = service.Get(new Easy.Data.DataFilter(), page);
                paginal.SetPage(page);
            };
        }
    }
}
