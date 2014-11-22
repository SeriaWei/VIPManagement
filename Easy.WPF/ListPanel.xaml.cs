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
using Easy.WPF.Controls;

namespace Easy.WPF
{
    /// <summary>
    /// ListPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ListPanel : UserControl
    {
        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register("PageIndex", typeof(int), typeof(ListPanel));
        public static readonly DependencyProperty AllPageProperty = DependencyProperty.Register("AllPage", typeof(int), typeof(ListPanel));

        private readonly Button _searchButton;
        public ListPanel()
        {
            InitializeComponent();
            _searchButton = new Button();
            _searchButton.Template = FindResource("ButtonIcon") as ControlTemplate;
            _searchButton.DataContext = new { Source = new Uri("/Easy.WPF;component/Images/search.png", UriKind.Relative) };
            _searchButton.ToolTip = "搜索";
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
            var search = new Func<DataFilter, Pagination, IEnumerable<T>>((filter, p) =>
            {
                var result = service.Get(filter, p);
                PageIndex = p.PageIndexReal;
                AllPage = p.AllPage;
                return result;
            });
            Button_Prev.Click += (s, e) =>
            {
                if (PageIndex > 1)
                {
                    this.DataSource = search.Invoke(GetFilter(), new Pagination { PageIndex = PageIndex - 2 });
                }

            };
            Button_Next.Click += (s, e) =>
            {
                if (PageIndex < AllPage)
                {
                    this.DataSource = search.Invoke(GetFilter(), new Pagination { PageIndex = PageIndex });
                }
            };
            _searchButton.Click += (s, e) =>
            {
                this.DataSource = search.Invoke(GetFilter(), new Pagination { PageIndex = 0 });
            };

            this.DataSource = search.Invoke(new DataFilter(), new Pagination { });
            this.DataContext = this;
        }
        private DataFilter GetFilter()
        {
            DataFilter filter = new DataFilter();
            var attribute = Easy.MetaData.DataConfigureAttribute.GetAttribute(this.ModelType);

            foreach (UIElement item in stackPanel_Search.Children)
            {
                if (item is ModelItemControlBase)
                {
                    var control = item as ModelItemControlBase;
                    if (control.Value != null && control.Value.ToString() != string.Empty)
                    {
                        filter.Where(attribute.MetaData.PropertyDataConfig[control.Name].ColumnName, OperatorType.Equal, control.Value);
                    }
                }
            }
            return filter;
        }
        void initPanel()
        {
            var attribute = Easy.MetaData.DataConfigureAttribute.GetAttribute(this.ModelType);
            if (attribute != null)
            {
                var tags = attribute.GetHtmlTags(true).OrderBy(m => m.OrderIndex);
                tags.Each(m =>
                {
                    if (!m.IsHidden && m.Grid.Searchable && m.Grid.Visiable)
                    {
                        var control = m.ToModelItemControl(false);
                        control.Width = 200;
                        control.Margin = new Thickness(2);
                        control.IsEnabled = true;                        
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
            stackPanel_Search.Children.Add(_searchButton);
        }
    }


}
