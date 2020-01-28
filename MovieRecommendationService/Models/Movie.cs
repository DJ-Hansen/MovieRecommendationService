using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationLibrary.Model
{
    public class Movie
    {
        [JsonProperty(PropertyName = "movieId")]
        public long MovieId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "releaseYear")]
        public int ReleaseYear { get; set; }

        [JsonProperty(PropertyName = "genreList")]
        public List<Genre> GenreList { get; set; }  // = new List<Genre>() { new Genre() { GenreId = 1, GenreText = "Action" } };
    }

    

}
