using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminToolWPF.ViewModel
{
    public interface IRelayCommand : ICommand
    {
        void OnCanExecuteChanged();

        void Execute();

        bool CanExecute();
    }

    public interface IRelayCommand<TParam> : ICommand
    {
        [Obsolete("Use the strongly typed version of Execute instead", true)]
        new void Execute(object parameter);

        [Obsolete("Use the strongly typed version of CanExecute instead", true)]
        new bool CanExecute(object parameter);

        void Execute(TParam parameter);

        bool CanExecute(TParam parameter);
    }


    public abstract class RelayCommandBase
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }
        private event EventHandler CanExecuteChangedInternal;

        private SynchronizationContext _synchronizationContext;
        public RelayCommandBase()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        public void OnCanExecuteChanged()
        {
            if (_synchronizationContext != null)
            {
                _synchronizationContext.Post(new SendOrPostCallback((o) => {
                    CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);
                }), null);
            }
            else
            {
                CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public class RelayCommand : RelayCommandBase, IRelayCommand
    {
        private readonly Action methodToExecute;
        private readonly Func<bool> canExecuteEvaluator;

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action methodToExecute) : this(methodToExecute, null)
        { }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                methodToExecute();
            }
        }

        public void Execute() => Execute(null);

        public bool CanExecute(object parmater) => canExecuteEvaluator == null || canExecuteEvaluator();
        public bool CanExecute() => CanExecute(null);
    }

    public class RelayCommand<TParam> : RelayCommandBase, IRelayCommand, IRelayCommand<TParam>
    {
        private Action<TParam> methodToExecute;

        private Func<TParam, bool> canExecuteEvaluator;

        public RelayCommand(Action<TParam> methodToExecute, Func<TParam, bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action<TParam> methodToExecute) : this(methodToExecute, null)
        { }

        public bool CanExecute(object parameter) => canExecuteEvaluator == null || canExecuteEvaluator((TParam)parameter);

        public bool CanExecute() => CanExecute(null);

        public bool CanExecute(TParam parameter) => canExecuteEvaluator == null || canExecuteEvaluator(parameter);

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            methodToExecute((TParam)parameter);
        }

        public void Execute() => Execute(null);

        public void Execute(TParam parameter)
        {
            if (!CanExecute(parameter)) return;
            methodToExecute(parameter);
        }
    }




    ///// EXAMPLE CODE

    //public IRelayCommand<RoundViewModel> EditRoundCommand => new RelayCommand<RoundViewModel>((RoundViewModel roundViewModel) =>
    //{
    //    string errorMessage = string.Empty;
    //    if (roundViewModel.HasParticipants)
    //        errorMessage = Properties.Resources.CanNotEditRoundWithParticipants;

    //    if (!string.IsNullOrWhiteSpace(errorMessage))
    //    {
    //        _messageBoxService.ShowMyMessageBox(Windows.MyMessageBoxStyle.ERROR, Properties.Resources.CanNotEditRound, errorMessage);
    //        return;
    //    }

    //    RaiseEditRoundClickedEvent(roundViewModel.Round);
    //}, (round) => round != null);





    //public IRelayCommand CancelCommand => new RelayCommand(() =>
    //{
    //    Close(false);
    //});
}
