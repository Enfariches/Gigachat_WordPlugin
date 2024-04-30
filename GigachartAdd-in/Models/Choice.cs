using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GigachartAdd_in
{
    public class Choice
    {
        [JsonPropertyName("message")]
        public MessageContent? message { get; set; }

        [JsonPropertyName("index")]
        public int index { get; set; }

        [JsonPropertyName("finish_reason")]
        public string? finish_reason { get; set; }
    }
}