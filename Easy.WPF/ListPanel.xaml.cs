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

namespace Easy.WPF
{
    /// <summary>
    /// ListPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ListPanel : UserControl
    {
        public ListPanel()
        {
            InitializeComponent();
        }
        Type _modelType;
        public Type ModelType
        {
            get { return _modelType; }
            set
            {
                _modelType = value;
                initPanel();
            }
        }
        IEnumerable _dataSource;
        public IEnumerable DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                dataGrid.ItemsSource = _dataSource;
            }
        }

        void initPanel()
        {

            var attribute = Easy.MetaData.DataConfigureAttribute.GetAttribute(this.ModelType);
            if (attribute != null)
            {
                var tags = attribute.GetHtmlTags(true).OrderBy(m => m.OrderIndex);
                tags.Each(m =>
                {
                    DataGridBoundColumn column = null;
                    if (m is Easy.HTML.Tags.TextBoxHtmlTag)
                    {
                        column = new DataGridTextColumn();
                    }
                    else if (m is Easy.HTML.Tags.CheckBoxHtmlTag)
                    {
                        column = new DataGridCheckBoxColumn();
                    }
                    if (column != null)
                    {
                        if (!m.Grid.Visiable)
                        {
                            column.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        column.Header = m.DisplayName;
                        Binding binding = new Binding(m.Name);
                        if (!m.ValueFormat.IsNullOrEmpty())
                        {
                            binding.StringFormat = m.ValueFormat;
                        }
                        column.Binding = binding;
                        column.IsReadOnly = true;
                        dataGrid.Columns.Add(column);
                    }
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
                    object proValue = null;
                    dataGrid.Columns.Add(column);
                });
            }
        }
    }
}
