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
            CheckBox_Value.SetBinding(CheckBox.IsCheckedProperty, DataBinding);
        }

        public override System.Collections.ObjectModel.ReadOnlyObservableCollection<ValidationError> GetValidateErrors()
        {
            return Validation.GetErrors(CheckBox_Value);
        }
    }
}
