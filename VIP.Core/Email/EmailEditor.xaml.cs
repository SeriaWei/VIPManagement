using Easy.Modules.DataDictionary;
using Microsoft.Win32;
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
using Easy.Extend;

namespace VIP.Core.Email
{
    /// <summary>
    /// EmailEditor.xaml 的交互逻辑
    /// </summary>
    public partial class EmailEditor : UserControl
    {
        public static DependencyProperty SubjectProperty = DependencyProperty.Register("Subject", typeof(string), typeof(EmailEditor));
        public static DependencyProperty ReceiverProperty = DependencyProperty.Register("Receiver", typeof(string), typeof(EmailEditor));
        public static DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(int), typeof(EmailEditor));
        public event Action<EmailMessage> SaveComplete;
        bool isUpdating = false;
        IEmailService _emailService;
        EmailMessage _message;
        public EmailEditor()
        {
            InitializeComponent();
        }
        public EmailEditor(EmailMessage emailMessage)
        {
            _message = emailMessage;
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            _emailService = Easy.Loader.CreateInstance<IEmailService>();
            WebBrowser_Email.Navigate(AppDomain.CurrentDomain.BaseDirectory + "tinymce\\Index.html");
            WebBrowser_Email.LoadCompleted += WebBrowser_Email_Navigated;


            CommandBinding binding = new CommandBinding(ApplicationCommands.Save, SaveExcuted, IsCanSave);
            this.CommandBindings.Add(binding);


            CommandBinding openImage = new CommandBinding(ApplicationCommands.Open, UpdatedImageExcuted, IsCanUpdate);
            this.CommandBindings.Add(openImage);

            IDataDictionaryService dicService = Easy.Loader.CreateInstance<IDataDictionaryService>();
            ComboBox_Status.ItemsSource = dicService.GetDictionaryByType("EmailMessage_Status");
            ComboBox_Status.DataContext = this;
            this.DataContext = this;
        }

        public string Subject
        {
            get { return (string)GetValue(SubjectProperty); }
            set
            {
                SetValue(SubjectProperty, value);
            }
        }

        public string Receiver
        {
            get { return (string)GetValue(ReceiverProperty); }
            set
            {
                SetValue(ReceiverProperty, value);
            }
        }

        public int Status
        {
            get { return (int)GetValue(StatusProperty); }
            set
            {
                SetValue(StatusProperty, value);
            }
        }

        private void UpdatedImageExcuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "图片|*.jpg;*.png;*.gif";
            if (dialog.ShowDialog() ?? false)
            {
                isUpdating = true;
                Task.Factory.StartNew(new Action(() =>
                {
                    try
                    {
                        string imageUrl = ImageControl.UploadImage(dialog.FileName);
                        this.Dispatcher.BeginInvoke(new Action<string>(src =>
                        {
                            InsertImage(imageUrl);
                        }), imageUrl);
                    }
                    catch (Exception ex)
                    {
                        this.Dispatcher.BeginInvoke(new Action<string>(msg =>
                        {
                            MessageBox.Show(msg);
                        }), ex.Message);
                    }
                    finally
                    {
                        isUpdating = false;
                    }

                }));

            }
        }
        private void IsCanUpdate(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isUpdating;
        }

        private void SaveExcuted(object sender, ExecutedRoutedEventArgs e)
        {
            bool create = false;
            if (_message == null)
            {
                _message = new EmailMessage();
                create = true;
            }
            _message.Title = Subject;
            _message.Receiver = Receiver;
            _message.Status = Status;
            _message.EmailContent = GetEmailContent();
            _emailService.Save(_message);
            MessageBox.Show("保存成功", "提示", MessageBoxButton.OK);
            if (SaveComplete != null)
            {
                SaveComplete(_message);
            }
            if (create)
            {
                _message = null;
                Subject = string.Empty;
                Receiver = string.Empty;
                SetEmailContent("");
            }
        }
        private void IsCanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        void WebBrowser_Email_Navigated(object sender, NavigationEventArgs e)
        {
            if (_message != null)
            {
                this.Subject = _message.Title;
                this.Receiver = _message.Receiver;
                this.Status = _message.Status;
                this.SetEmailContent(_message.EmailContent);
            }
        }

        void InsertImage(string url)
        {
            WebBrowser_Email.InvokeScript("insertImage", url);
        }

        public void SetEmailContent(string value)
        {
            WebBrowser_Email.InvokeScript("setValue", value);
        }
        public string GetEmailContent()
        {
            return WebBrowser_Email.InvokeScript("getValue").ToString();
        }


    }
}
