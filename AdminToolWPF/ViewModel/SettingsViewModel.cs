using AdminToolWPF.Helper_Classes;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminToolWPF.ViewModel
{
    public class SettingsViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Settings";
            }
        }


        public IRelayCommand TestAdminConnection => new RelayCommand(() =>
        {
            bool result = RequestHandler.IsAddressAvailable(AdminServiceAddress + "/api/movies");

            
            if (result)
                MessageBox.Show("Connection was found", "Connection result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Connection was NOT found", "Connection result", MessageBoxButtons.OK, MessageBoxIcon.Error);
        });


        // Recommendation
        public string AdminServiceAddress
        {
            get { return ConnetionSettings.AdminServiceAddress; }
            set
            {
                ConnetionSettings.AdminServiceAddress = value;
                RaisePropertyChanged("AdminServiceAddress");
            }
        }

        //public string AdminServiceUserName
        //{
        //    get { return ConnetionSettings.AdminServiceUserName; }
        //    set
        //    {
        //        ConnetionSettings.AdminServiceUserName = value;
        //        RaisePropertyChanged("AdminServiceUserName");
        //    }
        //}

        //public string AdminServiceUserPassword
        //{
        //    get { return ConnetionSettings.AdminServiceUserPassword; }
        //    set
        //    {
        //        ConnetionSettings.AdminServiceUserPassword = value;
        //        RaisePropertyChanged("AdminServiceUserPassword");
        //    }
        //}



        //// Recommendation
        //public string Neo4jAddress
        //{
        //    get { return ConnetionSettings.Neo4jAddress; }
        //    set
        //    {
        //        ConnetionSettings.Neo4jAddress = value;
        //        RaisePropertyChanged("Neo4jAddress");
        //    }
        //}

        //public string Neo4jUserName
        //{
        //    get { return ConnetionSettings.Neo4jUserName; }
        //    set
        //    {
        //        ConnetionSettings.Neo4jUserName = value;
        //        RaisePropertyChanged("Neo4jUserName");
        //    }
        //}

        //public string Neo4jUserPassword
        //{
        //    get { return ConnetionSettings.Neo4jUserPassword; }
        //    set
        //    {
        //        ConnetionSettings.Neo4jUserName = value;
        //        RaisePropertyChanged("Neo4jUserPassword");
        //    }
        //}


        //// MovieSearch
        //public string PostgresMoviesAddress
        //{
        //    get { return ConnetionSettings.PostgresMoviesAddress; }
        //    set
        //    {
        //        ConnetionSettings.PostgresMoviesAddress = value;
        //        RaisePropertyChanged("PostgresMoviesAddress");
        //    }
        //}

        //public string PostgresMoviesUserName
        //{
        //    get { return ConnetionSettings.PostgresMoviesUserName; }
        //    set
        //    {
        //        ConnetionSettings.PostgresMoviesUserName = value;
        //        RaisePropertyChanged("PostgresMoviesUserName");
        //    }
        //}

        //public string PostgresMoviesUserPassword
        //{
        //    get { return ConnetionSettings.PostgresMoviesUserPassword; }
        //    set
        //    {
        //        ConnetionSettings.PostgresMoviesUserPassword = value;
        //        RaisePropertyChanged("PostgresMoviesUserPassword");
        //    }
        //}


        //// UserInfo
        //public string PostgresUserAddress
        //{
        //    get { return ConnetionSettings.PostgresUserAddress; }
        //    set
        //    {
        //        ConnetionSettings.PostgresUserAddress = value;
        //        RaisePropertyChanged("PostgresUserAddress");
        //    }
        //}

        //public string PostgresUserUserName
        //{
        //    get { return ConnetionSettings.PostgresUserUserName; }
        //    set
        //    {
        //        ConnetionSettings.PostgresUserUserName = value;
        //        RaisePropertyChanged("PostgresUserUserName");
        //    }
        //}

        //public string PostgresUserUserPassword
        //{
        //    get { return ConnetionSettings.PostgresUserUserPassword; }
        //    set
        //    {
        //        ConnetionSettings.PostgresUserUserPassword = value;
        //        RaisePropertyChanged("PostgresUserUserPassword");
        //    }
        //}

    }
}
