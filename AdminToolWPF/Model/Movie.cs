using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminToolWPF.Model
{
    public class Movie
    {
        public int id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public List<Genre> GenreList { get; set; } = new List<Genre>() {  };


        //[JsonProperty(PropertyName = "movieId")]
        //public int MovieId { get; set; }

        //[JsonProperty(PropertyName = "movieId")]
        //public string Title { get; set; }

        //[JsonProperty(PropertyName = "ReleaseYear")]
        //public int ReleaseYear { get; set; }

        //public List<Genre> GenreList { get; set; } = new List<Genre>() { };
    }
}
