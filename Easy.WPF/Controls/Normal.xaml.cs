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

namespace Easy.WPF.Controls
{
    /// <summary>
    /// Normal.xaml 的交互逻辑
    /// </summary>
    public partial class Normal : ModelItemControlBase
    {
        public Normal()
        {
            InitializeComponent();

            TextBox_Value.SetBinding(TextBox.TextProperty, DataBinding);
        }



        public override System.Collections.ObjectModel.ReadOnlyObservableCollection<ValidationError> GetValidateErrors()
        {
            DataBinding.ValidationRules.Each(m => m.Validate());
             Validation.GetErrors(TextBox_Value);
        }
    }
}
