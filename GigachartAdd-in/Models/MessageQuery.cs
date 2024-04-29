using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GigachartAdd_in
{
    public class MessageQuery
    {
        [JsonPropertyName("model")]
        public string model { get; set; }

        [JsonPropertyName("messages")]
        public List<MessageContent> messages { get; set; }

        [JsonPropertyName("temperature")]
        public float temperature { get; set; }
        [JsonPropertyName("top_p")]
        public float top_p { get; set; }
        [JsonPropertyName("n")]
        public long n { get; set; }
        [JsonPropertyName("stream")]
        public bool stream { get; set; }
        [JsonPropertyName("max_tokens")]
        public long max_tokens { get; set; }
        public MessageQuery(List<MessageContent>? messages = null, string model = "GigaChat:latest", float temperature = 0.87f, float top_p = 0.47f, long n = 1, bool stream = false, long max_tokens = 512)
        {
            List<MessageContent> Contents = new List<MessageContent>();
            this.model = model;
            this.messages = messages ?? Contents;
            this.temperature = temperature;
            this.top_p = top_p;
            this.n = n;
            this.stream = stream;
            this.max_tokens = max_tokens;
        }
    }
}