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
    /// SmtpSetting.xaml 的交互逻辑
    /// </summary>
    public partial class SmtpSetting : UserControl
    {
        public static DependencyProperty SmtpHostProperty = DependencyProperty.Register("SmtpHost", typeof(string), typeof(SmtpSetting));
        public static DependencyProperty PortProperty = DependencyProperty.Register("Port", typeof(int), typeof(SmtpSetting));
        public static DependencyProperty IsSSLProperty = DependencyProperty.Register("IsSSL", typeof(bool), typeof(SmtpSetting));
        public static DependencyProperty EmailAddressProperty = DependencyProperty.Register("EmailAddress", typeof(string), typeof(SmtpSetting));
        public static DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(SmtpSetting));
        public static DependencyProperty PassWordProperty = DependencyProperty.Register("PassWord", typeof(string), typeof(SmtpSetting));
        public static DependencyProperty IsEnableProperty = DependencyProperty.Register("IsEnable", typeof(bool), typeof(SmtpSetting));
        public static DependencyProperty IsServiceEnableProperty = DependencyProperty.Register("IsServiceEnable", typeof(bool), typeof(SmtpSetting));
        EmailHost _emailHost;
        IEmailHostService _emailHostService;
        public string SmtpHost
        {
            get
            {
                return (string)GetValue(SmtpHostProperty);
            }
            set
            {
                SetValue(SmtpHostProperty, value);
            }
        }
        public int Port
        {
            get
            {
                return (int)GetValue(PortProperty);
            }
            set
            {
                SetValue(PortProperty, value);
            }
        }
        public bool IsSSL
        {
            get
            {
                return (bool)GetValue(IsSSLProperty);
            }
            set
            {
                SetValue(IsSSLProperty, value);
            }
        }
        public string EmailAddress
        {
            get
            {
                return (string)GetValue(EmailAddressProperty);
            }
            set
            {
                SetValue(EmailAddressProperty, value);
            }
        }
        public string UserName
        {
            get
            {
                return (string)GetValue(UserNameProperty);
            }
            set
            {
                SetValue(UserNameProperty, value);
            }
        }
        public string PassWord
        {
            get
            {
                return (string)GetValue(PassWordProperty);
            }
            set
            {
                SetValue(PassWordProperty, value);
            }
        }
        public bool IsEnable
        {
            get
            {
                return (bool)GetValue(IsEnableProperty);
            }
            set
            {
                SetValue(IsEnableProperty, value);
            }
        }
        public bool IsServiceEnable
        {
            get
            {
                return (bool)GetValue(IsServiceEnableProperty);
            }
            set
            {
                SetValue(IsServiceEnableProperty, value);
            }
        }
        public SmtpSetting()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            CommandBinding bind = new CommandBinding(ApplicationCommands.Save, Save, IsCanSave);
            CommandBinding test = new CommandBinding(ApplicationCommands.Find, Test, IsCanSave);
            this.CommandBindings.Add(bind);
            this.CommandBindings.Add(test);
            _emailHostService = Easy.Loader.CreateInstance<IEmailHostService>();
            ListPanel_Host.Service(_emailHostService);
            ListPanel_Host.EditClick += ListPanel_Host_EditClick;
            ListPanel_Host.DeleteClick += ListPanel_Host_DeleteClick;
            this.DataContext = this;
            Clear();

            IsServiceEnable = Sender.GetSetting().IsEnable;
        }

        void ListPanel_Host_DeleteClick(object sender, RoutedEventArgs e)
        {
            _emailHostService.Delete((sender as EmailHost).ID);
        }

        void Save(object sender, ExecutedRoutedEventArgs e)
        {
            if (InitEmailHost())
            {
                _emailHostService.Save(_emailHost);
                _emailHost = null;
                Clear();
                ListPanel_Host.Reload();
            }
        }

        private bool InitEmailHost()
        {
            if (_emailHost == null)
            {
                _emailHost = new EmailHost();
            }
            _emailHost.SmtpHost = SmtpHost;
            _emailHost.Port = Port;
            _emailHost.IsSSL = IsSSL;
            if (!System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, Easy.Constant.RegularExpression.Email))
            {
                MessageBox.Show("邮箱格式错误！", "错误");
                return false;
            }
            _emailHost.EmailAddress = EmailAddress;
            _emailHost.PassWord = PassWord;
            _emailHost.UserName = UserName;
            _emailHost.IsEnable = IsEnable;
            return true;
        }
        void Test(object sender, ExecutedRoutedEventArgs e)
        {
            if (InitEmailHost())
            {
                try
                {
                    new Easy.Net.Email.EmailSender().Send(new TestEmailContext(_emailHost));
                    MessageBox.Show("测试成功，可以正常使用！", "提示", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("测试失败！" + ex.Message, "提示", MessageBoxButton.OK);
                }
            }
        }
        void Clear()
        {
            SmtpHost = string.Empty;
            Port = 25;
            IsSSL = false;
            EmailAddress = string.Empty;
            PassWord = string.Empty;
            UserName = string.Empty;
            IsEnable = false;
        }
        void IsCanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void ListPanel_Host_EditClick(object sender, RoutedEventArgs e)
        {
            _emailHost = sender as EmailHost;
            if (_emailHost != null)
            {
                SmtpHost = _emailHost.SmtpHost;
                Port = _emailHost.Port;
                IsSSL = _emailHost.IsSSL;
                EmailAddress = _emailHost.EmailAddress;
                PassWord = _emailHost.PassWord;
                UserName = _emailHost.UserName;
                IsEnable = _emailHost.IsEnable;
            }
        }

        private void IsServiceEnable_Checked(object sender, RoutedEventArgs e)
        {
            Sender.SaveSetting(new ServiceSetting { IsEnable = IsServiceEnable, Seconds = 30 });
        }
    }
}
