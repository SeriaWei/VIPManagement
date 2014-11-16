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

namespace VIP.Core
{
    /// <summary>
    /// Paginal.xaml 的交互逻辑
    /// </summary>
    public partial class Paginal : UserControl
    {
        public delegate void PageChangeHandler(int pageIndex);
        public event PageChangeHandler PrevPageClick;
        public event PageChangeHandler NextPageClick;

        public static readonly DependencyProperty PageIndex = DependencyProperty.Register("PageIndex", typeof(int), typeof(Paginal));
        public static readonly DependencyProperty AllPage = DependencyProperty.Register("AllPage", typeof(int), typeof(Paginal));
        public Paginal()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.DataContext = this;
            Button_Prev.Click += (s, es) =>
            {
                int pageIndex = (int)this.GetValue(PageIndex);
                if (pageIndex > 1 && PrevPageClick != null)
                {
                    PrevPageClick(pageIndex - 2);
                }
            };
            Button_Next.Click += (s, es) =>
            {
                int pageIndex = (int)this.GetValue(PageIndex);
                int allPage = (int)this.GetValue(AllPage);
                if (pageIndex != allPage && NextPageClick != null)
                {
                    NextPageClick(pageIndex);
                }
            };
        }
        public void SetPage(Easy.Data.Pagination page)
        {
            this.SetValue(PageIndex, page.PageIndexReal);
            this.SetValue(AllPage, page.AllPage);
        }
    }
}
