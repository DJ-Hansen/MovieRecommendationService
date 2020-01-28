using AdminToolWPF.Helper_Classes;
using AdminToolWPF.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using AdminToolWPF.View;
using System.Threading.Tasks;

namespace AdminToolWPF.ViewModel
{
    public class MoviesViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Movies";
            }
        }
        




        public IRelayCommand NewMovieCommand => new RelayCommand(() =>
        {
            Window window = new Window { Title = "New Movie" };
            window.Content = new MovieView(this, null, window);
            window.Height = 300;
            window.Width = 500;
            window.ShowDialog();
        });


        public IRelayCommand EditMovieCommand => new RelayCommand(() =>
        {
            Window window = new Window { Title = "New Movie" };
            window.Content = new MovieView(this, SelectedMovie, window);
            window.Height = 300;
            window.Width = 500;
            window.ShowDialog();
        }, () => SelectedMovie != null);


        public IRelayCommand DeleteMovieCommand => new RelayCommand(() =>
        {
            string sMessageBoxText = $"Do you want to DELETE '{SelectedMovie.Title}' ?";
            string sCaption = "Delete movie";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            if(rsltMessageBox == MessageBoxResult.Yes)
            {
                // run code
                //moviesCollection.remo
                
            }

        }, () => SelectedMovie != null);





        public Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                RaisePropertyChanged("SelectedMovie");
            }
        }
        

        private CollectionViewSource moviesCollection = new CollectionViewSource();

        public ICollectionView SourceCollection
        {
            get
            {
                return this.moviesCollection.View;
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
                this.moviesCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
            }
        }

        
        public MoviesViewModel(Movie model = null)
        {
            if (IsInDesignMode || ViewModelBase.IsInDesignModeStatic)
            {
                model = new Movie()
                {
                    Title = "Test Movie",
                    ReleaseYear = 1999,
                    id = 9000,
                    GenreList = new List<Genre>() {
                        new Genre(){ GenreName = "Action" }
                    }
                };

            }


            //GetMovies();
            DoWork();
        }


        private Task<List<Movie>> DoWorkAsync()
        {
            TaskCompletionSource<List<Movie>> tcs = new TaskCompletionSource<List<Movie>>();
            Task.Run(() =>
            {
                try
                {
                    var test = QuerryHandler.GetMovies();
                    tcs.SetResult(test.OrderBy(x=>x.Title).ToList());
                }
                catch (Exception)
                {
                    tcs.SetResult(null);
                }
                
            });
            //return the Task
            return tcs.Task;
        }

        private async void DoWork()
        {
            try
            {
                //ApplicationViewModel.WorkInProgress = true;

                List<Movie> mList = await DoWorkAsync();

                if (mList is null || mList.Count is 0)
                {
                    MessageBoxResult rsltMessageBox = MessageBox.Show("There was no registered movie, please check the connection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                ObservableCollection<Movie> movies = new ObservableCollection<Movie>(mList);

                moviesCollection = new CollectionViewSource
                {
                    Source = movies
                };
                moviesCollection.Filter += (o, e) =>
                {
                    if (string.IsNullOrEmpty(FilterText))
                    {
                        e.Accepted = true;
                        return;
                    }

                    Movie usr = e.Item as Movie;
                    if (usr.Title.ToUpper().Contains(FilterText.ToUpper()))
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex.Message);
            }
        }





        //private void GetMovies(bool loadMovies = true)
        //{

        //    ObservableCollection<Movie> movies = null;

        //    if (loadMovies)
        //    {
        //        //Connect and load users

        //        var test = QuerryHandler.GetMovies();

        //        movies = new ObservableCollection<Movie>(test);
                
        //    }
        //    else
        //    {
        //        movies = new ObservableCollection<Movie>();
        //        for (int i = 1; i < 1000; i++)
        //        {
        //            movies.Add(new Movie() {
        //                MovieId = i,
        //                Title = "Title"+i,
        //                ReleaseYear = 1999
        //            });
        //        }
        //    }
            
        //    moviesCollection = new CollectionViewSource
        //    {
        //        Source = movies
        //    };
        //    moviesCollection.Filter += (o, e) =>
        //    {
        //        if (string.IsNullOrEmpty(FilterText))
        //        {
        //            e.Accepted = true;
        //            return;
        //        }

        //        Movie usr = e.Item as Movie;
        //        if (usr.Title.ToUpper().Contains(FilterText.ToUpper()))
        //        {
        //            e.Accepted = true;
        //        }
        //        else
        //        {
        //            e.Accepted = false;
        //        }
        //    };
        //}
    }

}
