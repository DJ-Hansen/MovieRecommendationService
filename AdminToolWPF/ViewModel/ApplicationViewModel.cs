using AdminToolWPF.Helper_Classes;
using AdminToolWPF.ViewModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AdminToolWPF
{


    public class ApplicationViewModel : ViewModelBase
    {
        #region Fields

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion

        public bool _working = false;
        public bool WorkInProgress
        {
            get { return _working; }
            set
            {
                _working = value;
                RaisePropertyChanged("WorkInProgress");
            }
        }

        //private static
        //public static bool SetWorkInProgress
        //{
        //    get { return WorkInProgress; }
        //    set
        //    {
        //        _working = value;
        //    }
        //}


        private ApplicationViewModel()
        {
            // Add available pages
            PageViewModels.Add(new MoviesViewModel());
            PageViewModels.Add(new UsersViewModel());
            PageViewModels.Add(new TestMethodsViewModel());
            PageViewModels.Add(new SettingsViewModel());

            // Set starting page
            CurrentPageViewModel = PageViewModels[3];
        }

        private static ApplicationViewModel singleton = new ApplicationViewModel();
        public static ApplicationViewModel GetInstance()
        {
            return singleton;
        }

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new Helper_Classes.RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    RaisePropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion
    }
}
