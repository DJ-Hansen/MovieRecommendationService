using AdminToolWPF.Helper_Classes;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminToolWPF;
using System.Windows;
using AdminToolWPF.Model;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;
using AdminToolWPF.View;

namespace AdminToolWPF.ViewModel
{
    public class UsersViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Users";
            }
        }





        public IRelayCommand NewUserCommand => new RelayCommand(() =>
        {
            Window window = new Window { Title = "New User" };

            window.Content = new UserView(null, window);
            window.Height = 220;
            window.Width = 260;
            window.ShowDialog();
        });


        public IRelayCommand EditUserCommand => new RelayCommand(() =>
        {
            Window window = new Window {Title = "New User" };
            window.Content = new UserView(SelectedUser, window);
            window.Height = 220;
            window.Width = 260;
            window.ShowDialog();
        }, () => SelectedUser != null);


        public IRelayCommand DeleteUserCommand => new RelayCommand(() =>
        {
            string sMessageBoxText = $"Do you want to DELETE '{SelectedUser.UserName}' ?";
            string sCaption = "Delete User";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                DoWorkDelete();
            }

        }, () => SelectedUser != null);



        private async void DoWorkDelete()
        {
            bool result = await QuerryHandler.DeleteUser(_selectedUser.UserId);
            
            string sMessageBoxText = result ? "User is deleted": "User is NOT deleted";
            string sCaption = "Delete User";

            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = result ? MessageBoxImage.None : MessageBoxImage.Error;
            MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            DoWork();
        }


        public User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                RaisePropertyChanged("SelectedUser");
            }
        }


        private CollectionViewSource usersCollection = new CollectionViewSource();

        public ICollectionView SourceCollection
        {
            get
            {
                return this.usersCollection.View;
            }
        }


        private string filterText;
        public string FilterText
        {
            get
            {
                return filterText;
            }
            set
            {
                filterText = value;
                this.usersCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
            }
        }


        public UsersViewModel(User model = null)
        {
            DoWork();
        }


        private Task<List<User>> DoWorkAsync()
        {
            TaskCompletionSource<List<User>> tcs = new TaskCompletionSource<List<User>>();
            Task.Run(() =>
            {
                try
                {
                    var test = QuerryHandler.GetUsers();
                    tcs.SetResult(test.OrderBy(x => x.UserName).ToList());
                }
                catch (Exception)
                {
                    tcs.SetResult(null);
                }
            });
            //return the Task
            return tcs.Task;
        }

        public async void DoWork()
        {
            //ApplicationViewModel.WorkInProgress = true;

            List<User> uList = await DoWorkAsync();
            ObservableCollection<User> users = new ObservableCollection<User>(uList);

            if (users is null || users.Count is 0)
            {
                MessageBoxResult rsltMessageBox = MessageBox.Show("There was no registered users, please check the connection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            usersCollection = new CollectionViewSource
            {
                Source = users
            };
            usersCollection.Filter += (o, e) =>
            {
                if (string.IsNullOrEmpty(FilterText))
                {
                    e.Accepted = true;
                    return;
                }

                User usr = e.Item as User;
                if (usr.UserName.ToUpper().Contains(FilterText.ToUpper()))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            };

            RaisePropertyChanged("SourceCollection");
            //ApplicationViewModel.GetInstance().WorkInProgress = false;
        }
        

    }
}
