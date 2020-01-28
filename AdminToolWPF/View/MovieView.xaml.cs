using AdminToolWPF.Model;
using AdminToolWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminToolWPF.View
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : UserControl
    {
        MovieViewModel context;
        public MovieView(MoviesViewModel parrent = null, Movie model = null, Window parrentWindow = null)
        {
            InitializeComponent();
            context = new MovieViewModel(parrent, model, parrentWindow);
            DataContext = context;
        }

        public Window Window { get; internal set; }
    }
}
