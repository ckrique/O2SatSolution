using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.OrionClient.IoTAgent
{
    public class ExhaustorRootDataCatcher : RootDataCatcher
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("timeInstant")]
        public TimeInstant timeInstant { get; set; }

        [JsonProperty("off_info")]
        public OffInfo off_info { get; set; }

        [JsonProperty("off_status")]
        public OffStatus off_status { get; set; }

        [JsonProperty("on_info")]
        public OnInfo on_info { get; set; }

        [JsonProperty("on_status")]
        public OnStatus on_status { get; set; }

        [JsonProperty("state")]
        public State state { get; set; }

        [JsonProperty("on")]
        public On on { get; set; }

        [JsonProperty("off")]
        public Off off { get; set; }

        public class Metadata
        {
            public TimeInstant TimeInstant { get; set; }
        }

        public class Off
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class OffInfo
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class OffStatus
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class On
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class OnInfo
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class OnStatus
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }
        public class State
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class TimeInstant
        {
            public string type { get; set; }
            public DateTime value { get; set; }
            public Metadata metadata { get; set; }
        }

    }
}
