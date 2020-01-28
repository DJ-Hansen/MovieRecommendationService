using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationLibrary.Model
{

    public class MovieRecommendation
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "movieId")]
        public int MovieId { get; set; }

        [JsonProperty(PropertyName = "averageRating")]
        public float? AverageRating { get; set; }

        [JsonProperty(PropertyName = "jaccard")]
        public float? Jaccard { get; set; }
    }
}
