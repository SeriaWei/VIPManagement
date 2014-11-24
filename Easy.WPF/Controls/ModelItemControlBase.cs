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
        private FrameworkElement _customerValueElement;
        private DependencyProperty _customerValueProperty;
        public ModelItemControlBase()
        {
            this.Margin = new Thickness(15, 2, 15, 2);
        }
        public void AddValidationRule(ValidationRule rule)
        {
            DataBinding.ValidationRules.Add(rule);
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            _customerValueElement = GetElement();
            _customerValueProperty = GetPeoperty();
            this.DataContext = this;
            DataBinding = new Binding("Value")
            {
                Mode = BindingMode.TwoWay,
                Source = this.DataContext,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnValidationError = true,
                Converter = GetValueConverter()
            };
            _customerValueElement.SetBinding(_customerValueProperty, DataBinding);

        }
        public abstract FrameworkElement GetElement();
        public abstract DependencyProperty GetPeoperty();
        public virtual IValueConverter GetValueConverter()
        {
            return null;
        }
        public virtual ReadOnlyObservableCollection<ValidationError> GetValidateErrors()
        {
            if (_customerValueElement.GetBindingExpression(_customerValueProperty).ValidateWithoutUpdate())
            {
                _customerValueElement.GetBindingExpression(_customerValueProperty).UpdateSource();
            }
            return Validation.GetErrors(_customerValueElement);
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(ModelItemControlBase));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(ModelItemControlBase));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

    }
}
