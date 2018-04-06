using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetStash
{
    [Newtonsoft.Json.JsonObject]
    public class NetStashEvent
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "exception-details")]
        public string ExceptionDetails { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "level")]
        public string Level { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "machine-name")]
        public string Machine { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "mac-address")]
        public string MacAddress { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "method")]
        public string Method { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "app-version")]
        public string AppVersion { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "fields")]
        public Dictionary<string, string> Fields { get; set; }

        //[Newtonsoft.Json.JsonProperty(PropertyName = "oldvalue")]
        //public string OldValue { get; set; }
        //[Newtonsoft.Json.JsonProperty(PropertyName = "newvalue")]
        //public string NewValue { get; set; }

        public NetStashEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
