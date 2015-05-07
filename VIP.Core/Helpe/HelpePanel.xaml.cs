using System;
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

namespace VIP.Core.Helpe
{
    /// <summary>
    /// HelpePanel.xaml 的交互逻辑
    /// </summary>
    public partial class HelpePanel : UserControl
    {
        public HelpePanel()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Task.Factory.StartNew(() =>
            {
                ADHelper ads = ADHelper.GetAD();
                if (ads != null && ads.ADs != null)
                {
                    ads.ADs.ForEach(m =>
                    {
                        this.Dispatcher.BeginInvoke(new Action<AD>(item =>
                        {
                            WrapPanel_Items.Children.Add(new ADItem(item));
                        }), m);
                    });
                }
            });
        }
    }
}
