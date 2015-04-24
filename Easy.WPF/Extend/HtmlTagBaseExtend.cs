using Easy.HTML.Tags;
using Easy.WPF.Controls;
using Easy.WPF.ValueConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using Easy.Extend;

namespace Easy.WPF.Extend
{
    public static class HtmlTagBaseExtend
    {
        public static ModelItemControlBase ToModelItemControl(this HtmlTagBase tag, bool containsValidation = true)
        {
            ModelItemControlBase item = null;
            if (tag is CheckBoxHtmlTag)
            {
                item = new CheckBoxItem();
            }
            else if (tag is HiddenHtmlTag)
            {
                item = new Normal();
                item.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (tag is DropDownListHtmlTag)
            {
                item = new DropDownList();
                (item as DropDownList).SetOptionItem((tag as DropDownListHtmlTag).OptionItems);
            }
            else if (tag is MutiLineTextBoxHtmlTag)
            {
                item = new TextArea();
            }
            else
            {
                item = new Normal();
                if (tag.DataType.Name == "DateTime")
                {
                    item = new Normal_Date();
                }
                else
                {
                    item = new Normal();
                }
            }
            item.Name = tag.Name;
            if (tag.IsHidden)
            {
                item.Visibility = System.Windows.Visibility.Collapsed;
            }
            item.Label = tag.DisplayName;
            item.IsEnabled = !tag.IsReadOnly;
            if (containsValidation)
            {
                tag.Validator.ForEach(v =>
                {
                    item.AddValidationRule(new ValidationRuleProvide(v));
                });
            }
            return item;
        }

        public static DataGridBoundColumn ToDataGridBoundColumn(this HtmlTagBase tag)
        {
            DataGridBoundColumn column = null;
            if (tag is CheckBoxHtmlTag)
            {
                column = new DataGridCheckBoxColumn();
            }
            else if (tag is DropDownListHtmlTag)
            {
                column = new DataGridTextColumn();
                Binding binding = new Binding(tag.Name);
                binding.Converter = new DictionaryValueConverter();
                binding.ConverterParameter = (tag as DropDownListHtmlTag).OptionItems;
                column.Binding = binding;
            }
            else
            {
                column = new DataGridTextColumn();
            }

            if (!tag.Grid.Visiable || tag is HiddenHtmlTag)
            {
                column.Visibility = System.Windows.Visibility.Collapsed;
            }
            column.Header = tag.DisplayName;
            if (column.Binding == null)
            {
                Binding binding = new Binding(tag.Name);
                if (!tag.ValueFormat.IsNullOrEmpty())
                {
                    binding.StringFormat = tag.ValueFormat;
                }
                column.Binding = binding;
            }
            column.IsReadOnly = true;
            return column;
        }
    }
}
