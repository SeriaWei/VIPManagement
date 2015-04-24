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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VIP.Core.Email
{
    /// <summary>
    /// EmailCollect.xaml 的交互逻辑
    /// </summary>
    public partial class EmailCollect : UserControl
    {
        string[] hostEnd = new string[] { "com", "cn", "xyz", "wang", "net", "top", "ren", "club", "org", "biz", "me", "name", "tv", "info", "cc", "asia", "mobi", "co", "so", "tel" };

        Regex oRegex = new Regex(Easy.Constant.RegularExpression.Chinese);
        Regex emailRegex = new Regex(Easy.Constant.RegularExpression.Email);
        Regex pageRegex = new Regex(@"\[\d+\-\d+\]*");
        Customer.CustomerService customerService = new Customer.CustomerService();
        int total = 0;
        int added = 0;
        int start = 0;
        int end = 0;
        bool isLoading = false;
        public EmailCollect()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            CommandBinding binding = new CommandBinding(ApplicationCommands.Find, Find, IsCanFind);
            this.CommandBindings.Add(binding);
        }

        private void Find(object sender, ExecutedRoutedEventArgs e)
        {
            total = 0;
            added = 0;
            string urlFormat = TextBox_UrlFormat.Text;
            isLoading = true;
            ListBox_Result.Items.Clear();
            Task.Factory.StartNew(() =>
            {
                pageRegex.Replace(urlFormat, new MatchEvaluator(PageSizeMatch));
                for (int i = start; i <= end; i++)
                {
                    this.Dispatcher.BeginInvoke(new Action<string>(msg =>
                    {
                        Label_Info.Content = msg;
                    }), string.Format("正在加载:{0}/{1}", i, end));
                    string url = pageRegex.Replace(urlFormat, i.ToString());
                    Easy.Net.WebPage page = new Easy.Net.WebPage(url);
                    string html = page.GetHtml();
                    emailRegex.Replace(html, new MatchEvaluator(Finded));
                }
            }).ContinueWith(new Action<Task>(t =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Label_Info.Content = string.Format("获取到邮箱:{0},新增:{1}", total, added);
                }));
                isLoading = false;
            }));
        }

        private string Finded(Match math)
        {
            string result = oRegex.Replace(math.Value, "").ToLower().Trim().Trim('.');
            if (!hostEnd.Any(m => result.EndsWith(m)))
            {
                foreach (string item in hostEnd)
                {
                    string[] infoStruct = result.Split('.');
                    string end = infoStruct[infoStruct.Length - 1];
                    if (end.Equals("con")) end = "com";
                    if (end.StartsWith(item))
                    {
                        result = result.Replace("." + end, "." + item);
                        break;
                    }
                }
            }
            this.Dispatcher.BeginInvoke(new Action<string>(val =>
            {
                ListBox_Result.Items.Insert(0, val);
            }), result);
            if (customerService.Count(new Easy.Data.DataFilter().Where("Email", Easy.Data.OperatorType.Equal, result)) == 0)
            {
                added++;
                customerService.Add(new Customer.CustomerEntity { Email = result, FirstName = result.Split('@')[0] });
            }
            total++;
            return result;
        }

        private string PageSizeMatch(Match match)
        {
            string[] info = match.Value.Replace("[", "").Replace("]", "").Split('-');
            int.TryParse(info[0], out start);
            int.TryParse(info[1], out end);
            return match.Value;
        }

        private void IsCanFind(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isLoading && pageRegex.IsMatch(TextBox_UrlFormat.Text);
        }
    }
}
