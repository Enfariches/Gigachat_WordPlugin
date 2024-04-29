using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GigachartAdd_in
{
    public class MessageContent
    {
        [JsonPropertyName("role")]
        public string role { get; set; }

        [JsonPropertyName("content")]
        public string content { get; set; }

        public MessageContent(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
    }
}