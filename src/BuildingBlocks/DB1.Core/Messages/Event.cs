using MediatR;
using System.Text.Json.Serialization;

namespace DB1.Core.Messages
{
    public class Event : Message, INotification
    {
        [JsonIgnore]
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
