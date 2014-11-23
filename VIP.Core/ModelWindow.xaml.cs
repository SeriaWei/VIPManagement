using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VIP.Core
{
    /// <summary>
    /// ModelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ModelWindow : Window
    {
        ReadOnlyObservableCollection<ValidationError> _errors;
        Action Save;
        bool _create = true;
        public ModelWindow()
        {
            InitializeComponent();
        }
        public object Model
        {
            get { return modelPanel.GetModel(); }
            set
            {
                _create = false;
                modelPanel.Model = value;
            }
        }
        public Type ModelType
        {
            get { return modelPanel.ModelType; }
            set { modelPanel.ModelType = value; }
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            modelPanel.GetModelCompleted += modelPanel_GetModelCompleted;
            // modelPanel.
            CommandBinding saveBinding = new CommandBinding(ApplicationCommands.Save, SaveExcuted, IsCanSave);
            this.CommandBindings.Add(saveBinding);

            CommandBinding closeBinding = new CommandBinding(ApplicationCommands.Close, CloseExcuted);
            this.CommandBindings.Add(closeBinding);
        }
        public void Service<T>(IServiceBase<T> service) where T : class
        {
            Save = new Action(() =>
            {
                if (!_create)
                {
                    service.Update(Model as T);
                }
                else
                {
                    service.Add(Model as T);
                }
            });
        }
        void modelPanel_GetModelCompleted(object model, ReadOnlyObservableCollection<ValidationError> errors)
        {
            _errors = errors;
        }
        void SaveExcuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (_errors == null)
            {
                modelPanel.GetModel();
            }
            if (!_errors.Any())
            {
                Save.Invoke();
                DialogResult = true;
                this.Close();
            }
        }
        void IsCanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_errors != null)
            {
                modelPanel.GetModel();
            }
            e.CanExecute = _errors == null || !_errors.Any();
        }

        void CloseExcuted(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
