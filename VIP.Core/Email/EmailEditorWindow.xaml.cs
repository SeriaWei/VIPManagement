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

namespace VIP.Core.Email
{
    /// <summary>
    /// EmailEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EmailEditorWindow : Window
    {
        EmailMessage _emailMessage;
        public EmailEditorWindow(EmailMessage emailMessage)
        {
            _emailMessage = emailMessage;
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            EmailEditor editor = new EmailEditor(_emailMessage);
            editor.SaveComplete += editor_SaveComplete;
            Main.Children.Add(editor);
        }

        void editor_SaveComplete(EmailMessage obj)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
