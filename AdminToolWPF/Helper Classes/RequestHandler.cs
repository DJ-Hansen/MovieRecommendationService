using AdminToolWPF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminToolWPF.Helper_Classes
{
    public class RequestHandler
    {

        public static bool IsAddressAvailable(string address)
        {
            try
            {
                System.Net.WebClient client = new WebClient();
                client.DownloadData(address);
                return true;
            }
            catch
            {
                return false;
            }
        }




        /// 
        /// User Methods
        /// 
        /// 


        /// 
        /// Movie Methods
        /// 

        //private void GetAllMovies()
        //{
        //    string address = ConnetionSettings.PostgresMoviesAddress + ;
        //}



        //public static string GetAdmin(string uri, string user = "", string password = "")
        //{
        //    try
        //    {
        //        var request = WebRequest.Create("http://myserver.com/service");
        //        string authInfo = user + ":" + password;
        //        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

        //        //like this:
        //        request.Headers["Authorization"] = "Basic " + authInfo;

        //        var response = request.GetResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }
        //    return null;
        //}



        //public static string Get(string uri)
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        var content = client.GetAsync(uri).Result;
        //        if (content.IsSuccessStatusCode)
        //        {
        //            var user = content.Content.ReadAsAsync<List<User>>().Result;
        //            string retunval = JsonConvert.SerializeObject(user);
        //            return retunval;
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }
        //}

        public static string Get(string uri)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        //public async Task<string> GetAsync(string uri)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        //    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        //    using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        //    using (Stream stream = response.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        return await reader.ReadToEndAsync();
        //    }
        //}


        public static string Post(string uri, string data, string contentType, string method = "POST")
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.ContentLength = dataBytes.Length;
                request.ContentType = contentType;
                request.Method = method;

                using (Stream requestBody = request.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        

        //public async Task<string> PostAsync(string uri, string data, string contentType, string method = "POST")
        //{
        //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        //    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        //    request.ContentLength = dataBytes.Length;
        //    request.ContentType = contentType;
        //    request.Method = method;

        //    using (Stream requestBody = request.GetRequestStream())
        //    {
        //        await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
        //    }

        //    using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        //    using (Stream stream = response.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        return await reader.ReadToEndAsync();
        //    }
        //}



    }
}
