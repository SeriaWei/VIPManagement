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

namespace VIP.Core.Email
{
    /// <summary>
    /// EmailList.xaml 的交互逻辑
    /// </summary>
    public partial class EmailList : UserControl
    {
        IEmailService _emailService;
        public EmailList()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            _emailService = Easy.Loader.CreateInstance<IEmailService>();
            ListPanel_Email.Service(_emailService);
            ListPanel_Email.EditClick += (s, ee) =>
            {
                var wind = new EmailEditorWindow(s as EmailMessage);
                if (wind.ShowDialog() ?? false)
                {
                    ListPanel_Email.Reload();
                }
            };
        
        }
    }
}
