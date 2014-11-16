using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Easy.Extend;
using System.Collections.ObjectModel;

namespace Easy.WPF.Controls
{
    public abstract class ModelItemControlBase : UserControl
    {
        protected Binding DataBinding;
        public void AddValidationRule(ValidationRule rule)
        {
            DataBinding.ValidationRules.Add(rule);
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.DataContext = this;
            DataBinding = new Binding("Value")
            {
                Mode = BindingMode.TwoWay,
                Source = this.DataContext,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnValidationError = true,
            };
        }

        public abstract ReadOnlyObservableCollection<ValidationError> GetValidateErrors();
        
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(ModelItemControlBase));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(ModelItemControlBase));

    }
}
