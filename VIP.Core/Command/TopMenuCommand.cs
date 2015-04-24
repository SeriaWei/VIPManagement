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
                (e.Parameter as DockPanel).Children.Add(Activator.CreateInstance("VIP.Core", Type).Unwrap() as UIElement);
            }
        }

        public override void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
