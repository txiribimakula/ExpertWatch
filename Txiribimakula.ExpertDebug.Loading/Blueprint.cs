using Newtonsoft.Json;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class Blueprint
    {
        [JsonProperty(PropertyName = "keys")]
        public string[] Keys { get; set; }

        [JsonProperty(PropertyName = "root")]
        public BlueprintNode Root { get; set; }
    }
}
