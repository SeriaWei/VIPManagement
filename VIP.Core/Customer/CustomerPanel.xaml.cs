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
            listPanel.Service(Loader.CreateInstance<ICustomerService>());
        }
    }
}
