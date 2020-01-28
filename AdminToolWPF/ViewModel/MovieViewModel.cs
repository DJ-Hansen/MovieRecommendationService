using AdminToolWPF.Helper_Classes;
using AdminToolWPF.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdminToolWPF.ViewModel
{

    public class MovieViewModel : ViewModelBase
    {
        private Movie _movie;
        private MoviesViewModel _parrent;
        private ObservableCollection<GenreViewModel> _genreViewModelList = new ObservableCollection<GenreViewModel>();

        private string _title = "";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }


        private int _releaseYear = 1800;
        public int ReleaseYear
        {
            get { return _releaseYear; }
            set
            {
                _releaseYear = value;
                RaisePropertyChanged("ReleaseYear");
            }
        }


        public ObservableCollection<GenreViewModel> GenreViewModelList
        {
            get { return _genreViewModelList; }
            set
            {
                _genreViewModelList = value;
                RaisePropertyChanged("GenreViewModelList");
            }
        }

        private bool _isNewMovie = false;
        public bool IsNewMovie
        {
            get { return _isNewMovie; }
            set
            {
                _isNewMovie = value;
                RaisePropertyChanged("IsNewMovie");
            }
        }



        public IRelayCommand UpdateMovieCommand => new RelayCommand(() =>
        {
            _movie.ReleaseYear = this.ReleaseYear;
            _movie.Title = this.Title;
            _movie.GenreList = this.GenreViewModelList.Where(x=>x.IsSelected).ToList().Select(y=>y.Genre).ToList();
            


            if (IsNewMovie)
                QuerryHandler.CreateMovie(_movie);
            else
                QuerryHandler.UpdateMovie(_movie);
            
        });

        public IRelayCommand CancelCommand => new RelayCommand(() =>
        {
            _parrentWindow.Close();
        });

        private Window _parrentWindow = null;

        public MovieViewModel(MoviesViewModel parrent = null, Movie model = null, Window parrentWindow = null)
        {
            _parrentWindow = parrentWindow;
            
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

            _parrent = parrent;

            GenreViewModelList.Clear();

            if(model != null)
                model = QuerryHandler.GetMovie(model.id);



            List<Genre> GenreList = new List<Genre>()
            {
                new Genre(){ GenreName = "Action" },
                new Genre(){ GenreName = "Adventure" },
                new Genre(){ GenreName = "Animation" },
                new Genre(){ GenreName = "Children" },
                new Genre(){ GenreName = "Comedy" },
                new Genre(){ GenreName = "Crime" },
                new Genre(){ GenreName = "Documentary" },
                new Genre(){ GenreName = "Drama" },
                new Genre(){ GenreName = "Fantasy" },
                new Genre(){ GenreName = "Film-Noir" },
                new Genre(){ GenreName = "Horror" },
                new Genre(){ GenreName = "Musical" },
                new Genre(){ GenreName = "Mystery" },
                new Genre(){ GenreName = "Romance" },
                new Genre(){ GenreName = "Sci-Fi" },
                new Genre(){ GenreName = "Thriller" },
                new Genre(){ GenreName = "War" },
                new Genre(){ GenreName = "Western" }
            };

            foreach (Genre gen in GenreList)
                GenreViewModelList.Add(new GenreViewModel(gen));


            if (model == null || model.id < 1)
            {
                IsNewMovie = true;
                _movie = new Movie();
            }
            else
            {
                IsNewMovie = false;
                _movie = model;
                ReleaseYear = _movie.ReleaseYear;
                Title = _movie.Title;

                if (_movie.GenreList != null)
                {
                    foreach (Genre genre in _movie.GenreList)
                    {
                        GenreViewModel found = GenreViewModelList.FirstOrDefault(x => x.GenreText == genre.GenreName);
                        if (found != null)
                            found.IsSelected = true;
                    }
                }

            }


            RaisePropertyChanged();
            RaisePropertyChanged("GenreViewModel");
            RaisePropertyChanged("GenreViewModelList");
            RaisePropertyChanged("Title");
        }


    }

}
