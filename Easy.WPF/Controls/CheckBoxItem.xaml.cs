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
    /// CheckBox.xaml 的交互逻辑
    /// </summary>
    public partial class CheckBoxItem : ModelItemControlBase
    {
        public CheckBoxItem()
        {
            InitializeComponent();
        }


        public override FrameworkElement GetElement()
        {
            return CheckBox_Value;
        }

        public override DependencyProperty GetPeoperty()
        {
            return CheckBox.IsCheckedProperty;
        }
    }
}
