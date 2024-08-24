using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DB1.Core.Messages
{
    public abstract class Message
    {
        [JsonIgnore]
        public string MessageType { get; protected set; }
        [JsonIgnore]
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
