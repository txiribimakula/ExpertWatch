using Newtonsoft.Json;

namespace Txiribimakula.ExpertWatch.Loading
{
    public class BlueprintNode
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "members")]
        public BlueprintNode[] Members { get; set; }
    }
}
