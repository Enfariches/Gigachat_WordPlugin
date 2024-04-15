using RestSharp;
using System;

var options = new RestClientOptions()
{
    MaxTimeout = -1,
};
var client = new RestClient(options);
var request = new RestRequest("https://ngw.devices.sberbank.ru:9443/api/v2/oauth", Method.Post);
request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
request.AddHeader("Accept", "application/json");
request.AddHeader("RqUID", "fbf7eaa1-2962-4bb2-b208-7511fa82b37f");
request.AddHeader("Authorization", "Basic ZmJmN2VhYTEtMjk2Mi00YmIyLWIyMDgtNzUxMWZhODJiMzdmOmE2YjFlMGZjLWQxNWUtNGY2Zi1iMmVmLTk5YzJjZmNmYTgxOQ==");
request.AddParameter("scope", "GIGACHAT_API_PERS");
RestResponse response = await client.ExecuteAsync(request);
Console.WriteLine(response.Content.GetType());
Console.ReadLine();

//Необходимо авторизироваться на Gigachat Devs и получить все необходимые токены