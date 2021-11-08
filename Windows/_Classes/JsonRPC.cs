using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMessager
{
    /// <summary>
    //  var json = "{\"jsonrpc\":\"2.0\",\"method\":\"GUI.ShowNotification\",\"params\":{\"title\":\"This is the title of the message\",\"message\":\"This is the body of the message\"},\"id\":1}";
    /// </summary>
    class JsonRPCRequest
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("params")]
        public dynamic Params { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRPCRequest"/> class.
        /// </summary>
        public JsonRPCRequest(string method)
        {
            Version = "2.0";
            Method = method;
            Id = "1";
            Params = new ExpandoObject();
        }
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
            //  var json = "{\"jsonrpc\":\"2.0\",\"method\":\"GUI.ShowNotification\",\"params\":{\"title\":\"This is the title of the message\",\"message\":\"This is the body of the message\"},\"id\":1}";
            var json = "{";
            json += "\"jsonrpc\" : \"" + Version + "\", ";
            json += "\"method\" : \"" + Method + "\", ";
            json += "\"params\" : " + JsonConvert.SerializeObject(Params) + ",";
            json += "\"id\" : " + Id;
            json += "}";

            return json;
        }
    }
    /// <summary>
    /// JsonRPC Respone
    /// </summary>
    class JsonRPCRespone
    {
        // { "jsonrpc": "2.0", "result": "Hallo JSON-RPC", "id": 1}
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Result { get; set; }
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        public JsonRPCRespone()
        {
            Version = "2.0";
            Result = new ExpandoObject();
            Id = "1";
            Error = null;
        }
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
