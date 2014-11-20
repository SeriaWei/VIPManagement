using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Easy.WPF.Command
{
    public class UICommandBinding : CommandBinding
    {
        public UICommandBinding(UICommand command)
            : base(command, command.Executed, command.CanExecute)
        {
        }
    }
}
