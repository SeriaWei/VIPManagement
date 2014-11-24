using Easy.WPF.ValueConverter;
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

namespace Easy.WPF.Controls
{
    /// <summary>
    /// DropDownList.xaml 的交互逻辑
    /// </summary>
    public partial class DropDownList : ModelItemControlBase
    {
        public DropDownList()
        {
            InitializeComponent();
        }

        public override FrameworkElement GetElement()
        {
            return ComboBox_Value;
        }

        public override DependencyProperty GetPeoperty()
        {
            return ComboBox.SelectedValueProperty;
        }
        //public override IValueConverter GetValueConverter()
        //{
        //    return new DictionaryValueConverter();
        //}
        public void SetOptionItem(Dictionary<string, string> options)
        {
            ComboBox_Value.ItemsSource = options;
        }
    }
}
