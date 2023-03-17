using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Util.Curl;

/*
var curl = new Curl();

// GET request
var response = await curl.Get("https://api.example.com/v1/users");

// POST request
var body = "{\"name\": \"John Doe\", \"email\": \"john.doe@example.com\"}";
var response = await curl.Post("https://api.example.com/v1/users", headers: null, body: body);

// HEAD request
var headers = new Dictionary<string, string> { { "Authorization", "Bearer YOUR_TOKEN" } };
var response = await curl.Head("https://api.example.com/v1/users/123", headers);

// DELETE request
var headers = new Dictionary<string, string> { { "Authorization", "Bearer YOUR_TOKEN" } };
var response = await curl.Delete("https://api.example.com/v1/users/123", headers);

// OPTIONS request
var response = await curl.Options("https://api.example.com/v1/users");
 */
public class Curl
{
    private readonly HttpClient _client;

    public Curl()
    {
        _client = new HttpClient();
    }

    public async Task<string> Get(string url, IDictionary<string, string> headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        AddHeaders(request, headers);
        var response = await _client.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Post(string url, IDictionary<string, string> headers = null, string body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        AddHeaders(request, headers);
        if (body != null)
        {
            request.Content = new StringContent(body);
        }

        var response = await _client.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Head(string url, IDictionary<string, string> headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Head, url);
        AddHeaders(request, headers);
        var response = await _client.SendAsync(request);
        return response.Headers.ToString();
    }

    public async Task<string> Delete(string url, IDictionary<string, string> headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        AddHeaders(request, headers);
        var response = await _client.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Options(string url, IDictionary<string, string> headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Options, url);
        AddHeaders(request, headers);
        var response = await _client.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    private static void AddHeaders(HttpRequestMessage request, IDictionary<string, string> headers)
    {
        if (headers == null) return;
        foreach (var (key, value) in headers)
        {
            request.Headers.Add(key, value);
        }
    }
}