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
            foreach (UIElement item in menuBar.Children)
            {
                if (item is Button)
                {
                    (item as Button).Click += TopMenu_Click;
                }
            }
          //  VIP.Core.Common.TopMenu menu = new VIP.Core.Common.TopMenu();
           // CommandBinding bind = new CommandBinding(menu.TopMenuClickCommand, new ExecutedRoutedEventHandler(menu.TopMenuClick), new CanExecuteRoutedEventHandler(menu.TopMenuCanClick));
           // Button_Customer.Command = menu.TopMenuClickCommand;
            //this.CommandBindings.Add(bind);
        }
        private void TopMenu_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement item in menuBar.Children)
            {
                if (item is Button)
                {
                    (item as Button).IsEnabled = true;
                }
            }
            (sender as Button).IsEnabled = false;
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

    }
}
