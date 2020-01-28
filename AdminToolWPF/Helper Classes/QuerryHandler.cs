using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdminToolWPF.Model;
using Newtonsoft.Json;

namespace AdminToolWPF.Helper_Classes
{
    public class QuerryHandler
    {

        /// <summary>
        /// 
        /// Movie
        /// 
        /// </summary>
        public static List<Movie> GetMovies()
        {
            string CurrentSentQuerry = $"{ConnetionSettings.MoviesService}";
            
            string json = RequestHandler.Get(CurrentSentQuerry);
            
            List<Movie> result = JsonConvert.DeserializeObject<List<Movie>>(json);
            
            return result;
        }


        public static Movie GetMovie(int id)
        {
            string CurrentSentQuerry = $"{ConnetionSettings.MoviesService}/{id}";

            string json = RequestHandler.Get(CurrentSentQuerry);

            Movie result = JsonConvert.DeserializeObject<Movie>(json);

            return result;
        }



        public static bool CreateMovie(Movie movie)
        {

            string CurrentSentQuerry = $"https://localhost:44319/api/movies";
            
            string serialized = JsonConvert.SerializeObject(movie);
            

            var result = RequestHandler.Post(CurrentSentQuerry, serialized, "Movie", "POST");


            return true;
        }

        

        public async static Task<bool> UpdateMovie(Movie movie)
        {
            string CurrentSentQuerry = $"{ConnetionSettings.AdminServiceAddress}/api/movies/{movie.id}";
            var client = new HttpClient();
            var content = await client.PutAsJsonAsync<Movie>(CurrentSentQuerry, movie);

            return content.IsSuccessStatusCode;
        }




        /// <summary>
        /// 
        /// Users
        /// 
        /// </summary>
        public static List<User> GetUsers()
        {
            string CurrentSentQuerry = $"{ConnetionSettings.UserService}";

            string json = RequestHandler.Get(CurrentSentQuerry);

            List<User> result = JsonConvert.DeserializeObject<List<User>>(json);

            return result;
        }

        public async static Task<bool> DeleteUser(int? userId)
        {
            string CurrentSentQuerry = string.Format("http://dlsadminapi.azurewebsites.net/api/users/{0}", userId);
            var client = new HttpClient();
            var content = await client.DeleteAsync(CurrentSentQuerry);

            return content.IsSuccessStatusCode;
        }

        
        public async static Task<bool> CreateUser(User user)
        {
            string CurrentSentQuerry = "http://dlsadminapi.azurewebsites.net/api/users";
            //string CurrentSentQuerry = string.Format("https://localhost:44374/api/users/{0}", user.UserId);
            var client = new HttpClient();
            var content = await client.PostAsJsonAsync<User>(CurrentSentQuerry, user);

            return content.IsSuccessStatusCode;
        }


        public async static Task<bool> UpdateUser(User user)
        {
            string CurrentSentQuerry = string.Format("http://dlsadminapi.azurewebsites.net/api/users/{0}", user.UserId);
            //string CurrentSentQuerry = string.Format("https://localhost:44374/api/users/{0}", user.UserId);
            var client = new HttpClient();
            var content = await client.PutAsJsonAsync<User>(CurrentSentQuerry, user);

            return content.IsSuccessStatusCode;
        }


        //public async static Task<bool> UpdateUser(User user)
        //{
        //    //string CurrentSentQuerry = $"{ConnetionSettings.AdminServiceAddress}/api/users/{user.UserId}";
        //    // string CurrentSentQuerry = $"{"http://dlsadminapi.azurewebsites.net"}/api/users/{user.UserId}";
        //    string CurrentSentQuerry = string.Format("http://dlsadminapi.azurewebsites.net/api/users/{0}", user.UserId);

        //    var client = new HttpClient();
        //    var content = await client.PutAsJsonAsync<User>(CurrentSentQuerry, user);
        //    string serialized = JsonConvert.SerializeObject(user);

        //    var result = RequestHandler.Post(CurrentSentQuerry, serialized, "User", "PUT");

        //    return true;
        //}

    }
}
