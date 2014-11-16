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
using System.Windows.Shapes;

namespace VIPManagement
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.MouseDown += (s, e) => { DragMove(); };
            this.Button_Close.Click += (s, e) => { this.Close(); };
            this.Button_Login.Click += (s, e) => {
                new MainWindow().Show();
                this.Close();
            };
        }
    }
}
