using Newtonsoft.Json;

namespace NethermindPruneStarter
{
    /// <summary>
    ///  A JSON-RPC Request
    /// </summary>
    [JsonObject]
    internal class Request
    {
        [JsonProperty(PropertyName = "jsonrpc")]
        public string JsonRPC { get; set; } = "2.0";

        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "params")]
        public object[] Params { get; set; }

        public Request(int ID, string Method, object[] Params)
        {
            this.ID = ID;
            this.Method = Method;
            this.Params = Params;
        }
    }
}
