using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Easy.Extend;

namespace VIP.Core.Helpe
{
    /// <summary>
    /// ADItem.xaml 的交互逻辑
    /// </summary>
    public partial class ADItem : UserControl
    {
        List<Color> Colors = new List<Color>();
        static Random ran;
        AD ad;
        static ADItem()
        {
            ran = new Random(DateTime.Now.Millisecond);
        }
        public ADItem(AD aditem)
        {
            Colors.Add(Color.FromArgb(255, 51, 122, 183));
            Colors.Add(Color.FromArgb(255, 244, 117, 110));
            Colors.Add(Color.FromArgb(255, 75, 178, 221));
            Colors.Add(Color.FromArgb(255, 80, 212, 214));
            InitializeComponent();
            this.TextBlock_Title.Text = aditem.Title;
            this.TextBlock_Description.Text = aditem.Description;
            if (aditem.Image.IsNotNullAndWhiteSpace())
            {
                this.Image_Main.Source = new BitmapImage(new Uri(aditem.Image, UriKind.Absolute));
            }
            ad = aditem;
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Grid_Title.Background = new SolidColorBrush(Colors[ran.Next(0, Colors.Count)]);
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(ad.Url);
            }
            catch
            {
                System.Diagnostics.Process.Start("iexplore", ad.Url);
            }
        }
    }
}
