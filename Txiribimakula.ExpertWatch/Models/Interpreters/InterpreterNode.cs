using Newtonsoft.Json;

namespace Txiribimakula.ExpertWatch.Models.Interpreters
{
    public class InterpreterNode
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "members")]
        public InterpreterNode[] Members { get; set; }
    }
}
