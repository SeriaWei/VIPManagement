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

namespace VIP.Core.Email
{
    /// <summary>
    /// EmailEditor.xaml 的交互逻辑
    /// </summary>
    public partial class EmailEditor : UserControl
    {
        bool isUpdating = false;
        public EmailEditor()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            WebBrowser_Email.Navigate(AppDomain.CurrentDomain.BaseDirectory + "tinymce\\Index.html");
            WebBrowser_Email.LoadCompleted += WebBrowser_Email_Navigated;


            CommandBinding binding = new CommandBinding(ApplicationCommands.Save, SaveExcuted, IsCanSave);
            this.CommandBindings.Add(binding);


            CommandBinding openImage = new CommandBinding(ApplicationCommands.Open, UpdatedImageExcuted, IsCanUpdate);
            this.CommandBindings.Add(openImage);
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
                    catch(Exception ex)
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

        }
        private void IsCanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        void WebBrowser_Email_Navigated(object sender, NavigationEventArgs e)
        {
            SetEmailContent("<img alt='Headerimage' src='http://moxiecode.cachefly.net/tinymce/v9/images/subimage/docs.gif' style='left:200px; position:absolute; display: block'>");
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
