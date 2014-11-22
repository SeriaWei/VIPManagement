using System;
using System.Collections;
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
using Easy.WPF.ValueConverter;
using Easy.HTML.Tags;
using Easy.RepositoryPattern;
using Easy.Data;
using Easy.WPF.Extend;

namespace Easy.WPF
{
    /// <summary>
    /// ListPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ListPanel : UserControl
    {
        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register("PageIndex", typeof(int), typeof(ListPanel));
        public static readonly DependencyProperty AllPageProperty = DependencyProperty.Register("AllPage", typeof(int), typeof(ListPanel));
        public ListPanel()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }
        Type _modelType;
        public Type ModelType
        {
            get { return _modelType; }
            private set
            {
                _modelType = value;
                initPanel();
            }
        }
        IEnumerable _dataSource;
        public IEnumerable DataSource
        {
            get { return _dataSource; }
            private set
            {
                _dataSource = value;
                dataGrid.ItemsSource = _dataSource;
            }
        }
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }
        public int AllPage
        {
            get { return (int)GetValue(AllPageProperty); }
            set { SetValue(AllPageProperty, value); }
        }
        public void Service<T>(IServiceBase<T> service) where T : class
        {
            ModelType = typeof(T);
            Pagination page = new Pagination { PageIndex = 0 };
            this.DataSource = service.Get(new DataFilter(), page);
            PageIndex = page.PageIndexReal;
            AllPage = page.AllPage;
            Button_Prev.Click += (s, e) =>
            {
                if (PageIndex > 1)
                {
                    Pagination pagin = new Pagination { PageIndex = PageIndex - 2 };
                    this.DataSource = service.Get(new DataFilter(), pagin);
                    PageIndex = pagin.PageIndexReal;
                    AllPage = page.AllPage;
                }

            };
            Button_Next.Click += (s, e) =>
            {
                if (PageIndex < AllPage)
                {
                    Pagination pagin = new Pagination { PageIndex = PageIndex };
                    this.DataSource = service.Get(new DataFilter(), pagin);
                    PageIndex = pagin.PageIndexReal;
                    AllPage = page.AllPage;
                }
            };
            this.DataContext = this;
        }

        void initPanel()
        {

            var attribute = Easy.MetaData.DataConfigureAttribute.GetAttribute(this.ModelType);
            if (attribute != null)
            {
                var tags = attribute.GetHtmlTags(true).OrderBy(m => m.OrderIndex);
                tags.Each(m =>
                {
                    if (m.Grid.Searchable)
                    {
                        var control = m.ToModelItemControl();
                        control.Width = 200;
                        control.Margin = new Thickness(2);
                        stackPanel_Search.Children.Add(control);
                    }
                    dataGrid.Columns.Add(m.ToDataGridBoundColumn());
                });
            }
            else
            {
                var properties = this.ModelType.GetProperties();
                properties.Each(item =>
                {
                    DataGridColumn column = null;
                    TypeCode code;
                    if (item.PropertyType.Name == "Nullable`1")
                    {
                        code = Type.GetTypeCode(item.PropertyType.GetGenericArguments()[0]);
                    }
                    else
                    {
                        code = Type.GetTypeCode(item.PropertyType);
                    }
                    switch (code)
                    {
                        case TypeCode.Boolean: break;
                        case TypeCode.DateTime: break;
                        case TypeCode.Char:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                        case TypeCode.SByte:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Object:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                        case TypeCode.String:
                        case TypeCode.DBNull:
                        case TypeCode.Byte:
                        case TypeCode.Empty:
                            break;
                        default:
                            break;
                    }
                    dataGrid.Columns.Add(column);
                });
            }
        }
    }


}
