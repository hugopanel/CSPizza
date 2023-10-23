using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private string _caption;

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowClientViewCommand { get; }
        public ICommand ShowWorkerViewCommand { get; }
        public ICommand ShowStatViewCommand { get; }
        public ICommand ShowRegisterViewCommand { get; }

        public MainViewModel()
        {
            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowClientViewCommand = new ViewModelCommand(ExecuteShowClientViewCommand);
            ShowWorkerViewCommand = new ViewModelCommand(ExecuteShowWorkerViewCommand);
            ShowStatViewCommand = new ViewModelCommand(ExecuteShowStatViewCommand);
            ShowRegisterViewCommand = new ViewModelCommand(ExecuteRegisterClientViewCommand);
            //Default view
            ExecuteShowHomeViewCommand(null);
        }

        private void ExecuteShowClientViewCommand(object obj)
        {
            CurrentChildView = new ClientViewModel();
            Caption = "CLIENTS";
        }
        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "COMMANDS";
        }

        private void ExecuteShowWorkerViewCommand(object obj)
        {
            CurrentChildView = new WorkerViewModel();
            Caption = "WORKERS";
        }

        private void ExecuteShowStatViewCommand(object obj)
        {
            CurrentChildView = new StatViewModel();
            Caption = "STATS";
        }

        private void ExecuteRegisterClientViewCommand(object obj)
        {
            CurrentChildView = new RegisterViewModel();
            Caption = "REGISTER";
        }
    }
}
