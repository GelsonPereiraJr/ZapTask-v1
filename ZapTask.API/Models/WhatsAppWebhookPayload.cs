
using System.Text.Json.Serialization;

namespace ZapTask.API.Models
{
    public class WhatsAppWebhookPayload
    {
        [JsonPropertyName("entry")]
        public List<Entry>? Entry { get; set; }
    }

    public class Entry
    {
        [JsonPropertyName("changes")]
        public List<Change>? Changes { get; set; }
    }

    public class Change
    {
        [JsonPropertyName("value")]
        public Value? Value { get; set; }
    }

    public class Value
    {
        [JsonPropertyName("messages")]
        public List<Message>? Messages { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("from")]
        public string? From { get; set; }

        [JsonPropertyName("text")]
        public Text? Text { get; set; }
    }

    public class Text
    {
        [JsonPropertyName("body")]
        public string? Body { get; set; }
    }
}
