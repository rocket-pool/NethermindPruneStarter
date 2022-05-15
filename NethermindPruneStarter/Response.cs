using Newtonsoft.Json;

namespace NethermindPruneStarter
{
    /// <summary>
    /// A JSON-RPC Response
    /// </summary>
    [JsonObject]
    internal class Response
    {
        [JsonProperty(PropertyName = "jsonrpc")]
        public string JsonRPC { get; set; } = "2.0";

        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string? Result { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public int? ErrorCode { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object? Data { get; set; }
    }
}
