using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminToolWPF.Model
{
    public class Genre
    {
        [JsonProperty(PropertyName = "name")]
        public string GenreName { get; set; }
    }
}
