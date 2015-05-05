using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Easy.WPF.Command;

namespace VIP.Core.Common
{
    public class TopMenuCommand : UICommand
    {
        Dictionary<string, UIElement> tabs = new Dictionary<string, UIElement>();
        public override void Executed(object target, ExecutedRoutedEventArgs e)
        {
            foreach (UIElement item in (target as VirtualizingStackPanel).Children)
            {
                if (item is Button)
                {
                    (item as Button).IsEnabled = true;
                }
            }
            (e.Source as Button).IsEnabled = false;

            (e.Parameter as DockPanel).Children.Clear();
            if ((e.Source as Button).Tag != null)
            {
                string Type = (e.Source as Button).Tag.ToString();
                UIElement element;
                if (tabs.ContainsKey(Type))
                {
                    element = tabs[Type];
                }
                else
                {
                    element = Activator.CreateInstance("VIP.Core", Type).Unwrap() as UIElement;
                    tabs.Add(Type, element);
                }
                (e.Parameter as DockPanel).Children.Add(element);
            }
        }

        public override void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
