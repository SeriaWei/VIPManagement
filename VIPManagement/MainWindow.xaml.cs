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
using Easy.WPF.Command;
using VIP.Core.Common;

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

        }
        private void TopMenu_Click(object sender, RoutedEventArgs e)
        {

        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            
            var command = new TopMenuCommand();
            var binding = new UICommandBinding(command);
            foreach (UIElement item in menuBar.Children)
            {
                if (item is Button)
                {
                    (item as Button).Command = command;
                    (item as Button).CommandParameter = dockPanel_Main;
                }
            }
            menuBar.CommandBindings.Add(binding);

            VIP.Core.Email.Sender.Start();
        }

    }
}
