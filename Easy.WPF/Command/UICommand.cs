using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Easy.WPF.Command
{
    public abstract class UICommand : RoutedUICommand
    {
        public abstract void Executed(object target, ExecutedRoutedEventArgs e);
        public abstract void CanExecute(object sender, CanExecuteRoutedEventArgs e);
    }
}
