using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationLibrary.Model
{
    public class User
    {
        [JsonProperty(PropertyName = "userId")]
        public long UserId { get; set; }
    }
}
