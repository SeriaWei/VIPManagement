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
using Easy.WPF.Controls;
using Easy.Extend;
using System.Collections.ObjectModel;

namespace Easy.WPF
{
    /// <summary>
    /// ModelPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ModelPanel : UserControl
    {
        public delegate void GetModelCompletedEventHandler(object model, ReadOnlyObservableCollection<ValidationError> errors);
        public event GetModelCompletedEventHandler GetModelCompleted;
        public ModelPanel()
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
        object _model;
        public object Model
        {
            private get
            {
                ObservableCollection<ValidationError> errors = new ObservableCollection<ValidationError>();
                foreach (UIElement item in ModelContent.Children)
                {
                    if (item is ModelItemControlBase)
                    {
                        var itemM = item as ModelItemControlBase;
                        itemM.GetValidateErrors().Each(e => errors.Add(e));
                        var val = item.GetValue(ModelItemControlBase.ValueProperty);
                        Easy.Reflection.ClassAction.SetObjPropertyValue(_model, itemM.Name, val);
                    }
                }
                if (this.GetModelCompleted != null)
                {
                    this.GetModelCompleted(_model, new ReadOnlyObservableCollection<ValidationError>(errors));
                }
                return _model;
            }
            set
            {
                _model = value;
                this.ModelType = _model.GetType();
            }
        }

        public object GetModel()
        {
            return this.Model;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }
        void initPanel()
        {

            var attribute = Easy.MetaData.DataConfigureAttribute.GetAttribute(this.ModelType);
            if (attribute != null)
            {
                var tags = attribute.GetHtmlTags(true);
                tags.Each(m =>
                {
                    ModelItemControlBase item = null;
                    object proValue = null;
                    if (_model != null)
                    {
                        proValue = Easy.Reflection.ClassAction.GetObjPropertyValue(_model, m.Name);
                    }
                    if (m is Easy.HTML.Tags.TextBoxHtmlTag)
                    {
                        if (m.DataType.Name == "DateTime")
                        {
                            item = new Normal_Date();
                        }
                        else
                        {
                            item = new Normal();
                        }
                    }
                    else if (m is Easy.HTML.Tags.CheckBoxHtmlTag)
                    {
                        item = new CheckBoxItem();
                    }
                    else if (m is Easy.HTML.Tags.HiddenHtmlTag)
                    {
                        item = new Normal();
                        item.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    if (item != null)
                    {
                        item.Name = m.Name;
                        if (m.IsHidden)
                        {
                            item.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        item.SetValue(ModelItemControlBase.LabelProperty, m.DisplayName);
                        item.SetValue(ModelItemControlBase.IsEnabledProperty, !m.IsReadOnly);
                        if (proValue != null)
                        {
                            item.SetValue(ModelItemControlBase.ValueProperty, proValue);
                        }
                        m.Validator.ForEach(v =>
                        {
                            item.AddValidationRule(new ValidationRuleProvide(v));
                        });
                        ModelContent.Children.Add(item);
                    }
                });
            }
            else
            {
                var properties = this.ModelType.GetProperties();
                properties.Each(item =>
                {
                    ModelItemControlBase contrl = null;
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
                        case TypeCode.Boolean: contrl = new CheckBoxItem(); break;
                        case TypeCode.DateTime: contrl = new Normal_Date(); break;
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
                            contrl = new Normal(); break;
                        default: contrl = new Normal();
                            break;
                    }
                    object proValue = null;
                    if (_model != null)
                    {
                        proValue = Easy.Reflection.ClassAction.GetObjPropertyValue(_model, item.Name);
                    }
                    if (proValue != null)
                    {
                        contrl.SetValue(ModelItemControlBase.ValueProperty, proValue);
                    }
                    contrl.SetValue(ModelItemControlBase.LabelProperty, item.Name);
                    ModelContent.Children.Add(contrl);
                });
            }
        }
    }
}
