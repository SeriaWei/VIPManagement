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

namespace VIPManagement
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Button_Customer.Click += (s, e) =>
            {
                this.dd.GetModel();
            };
            this.dd.GetModelCompleted += (s, e) =>
            {
            };
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.dd.Model = new VIP.Core.Customer.CustomerEntity() { };
        }
    }
}
