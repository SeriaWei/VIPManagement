using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace VIP.Core.Common
{
    public class TopMenu
    {
        public TopMenu()
        {
            TopMenuClickCommand = new RoutedUICommand();
        }
        public RoutedUICommand TopMenuClickCommand { get; set; }

        public void TopMenuClick(object sender, ExecutedRoutedEventArgs e)
        {

        }
        public void TopMenuCanClick(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
