using Newtonsoft.Json;

namespace LittleBits.CloudBitApiClient
{
    public class Device
    {
        [JsonProperty("label")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("is_connected")]
        public bool IsConnected { get; set; }

        [JsonProperty("subscribers")]
        public DeviceSubscriber[] Subscribers { get; set; }
    }

    public class DeviceSubscriber
    {
        [JsonProperty("pid")]
        public string PublisherId { get; set; }

        [JsonProperty("sid")]
        public string SubscriberId { get; set; }
    }

    public class DeviceOutputRequest
    {
        public DeviceOutputRequest()
        {
            this.DurationInMilliseconds = -1; // the default API value
        }

        [JsonProperty("percent")]
        public decimal Percent { get; set; }
        [JsonProperty("duration_ms")]
        public int DurationInMilliseconds { get; set; }
        [JsonIgnore]
        public string DeviceId { get; set; }
    }

    public class PublisherSubscriberRequest
    {
        [JsonProperty("publisher_id")]
        public string PublisherId { get; set; }

        [JsonProperty("subscriber_id")]
        public string SubscriberId { get; set; }
    }

    public class CreateSubscriberRequest
    {
        [JsonProperty("publisher_id")]
        public string PublisherId { get; set; }

        [JsonProperty("subscriber_id")]
        public string SubscriberId { get; set; }

        [JsonProperty("publisher_events")]
        public string[] PublisherEvents { get; set; }
    }
}
