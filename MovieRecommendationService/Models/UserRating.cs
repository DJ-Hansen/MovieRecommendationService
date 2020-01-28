using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationLibrary.Model
{
    public class UserRating
    {
        [JsonProperty(PropertyName = "userId")]
        public long UserId { get; set; }
        [JsonProperty(PropertyName = "movieId")]
        public long MovieId { get; set; }
        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }
    }
}
