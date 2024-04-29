using System.Net;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GigachartAdd_in
{
    public class GigaChatClass(string secretKey)
    {
        private string URL = "https://gigachat.devices.sberbank.ru/api/v1/";
        public Token Token { get; private set; }
        public long ExpiresAt { get; private set; }
        private string SecretKey { get; set; } = secretKey;

        public async Task<Token> CreateTokenAsync()
        {
            try
            {
                Console.WriteLine("Creating Token");
                HttpClientHandler clientHandler = new HttpClientHandler();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => { return true; };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using var client = new HttpClient(clientHandler);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://ngw.devices.sberbank.ru:9443/api/v2/oauth");
                var httpClientHandler = new HttpClientHandler();

                request.Headers.Add("Authorization", "Bearer " + SecretKey);
                request.Headers.Add("RqUID", Guid.NewGuid().ToString());
                request.Content = new StringContent("scope=GIGACHAT_API_PERS");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Token = JsonSerializer.Deserialize<Token>(responseBody);
                ExpiresAt = Token.ExpiresAt;
                return Token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<Response?> CompletionsAsync(MessageQuery query)
        {
            if (ExpiresAt < ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds())
            {
                await CreateTokenAsync();
            }
            if (Token != null)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();

                string responseBody;
                Response? DeserializedResponse;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => { return true; };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URL + "chat/completions");

                    request.Headers.Add("Authorization", "Bearer " + Token.AccessToken);

                    request.Content = new StringContent(JsonSerializer.Serialize(query));
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    DeserializedResponse = JsonSerializer.Deserialize<Response>(responseBody);
                    Console.WriteLine(responseBody);
                    client.Dispose();
                }
                return DeserializedResponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<Response?> CompletionsAsync(string _message)
        {
            if (ExpiresAt < ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds())
            {
                await CreateTokenAsync();
            }
            if (Token != null)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();

                string responseBody;
                Response? DeserializedResponse;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => { return true; };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                MessageQuery query = new MessageQuery();
                query.messages.Add(new MessageContent("user", _message));

                using (var client = new HttpClient(clientHandler))
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URL + "chat/completions");

                    request.Headers.Add("Authorization", "Bearer " + Token.AccessToken);

                    request.Content = new StringContent(JsonSerializer.Serialize(query));
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    DeserializedResponse = JsonSerializer.Deserialize<Response>(responseBody);
                    Console.WriteLine(responseBody);
                    client.Dispose();
                }
                return DeserializedResponse;
            }
            else
            {
                return null;
            }
        }
    }
}
