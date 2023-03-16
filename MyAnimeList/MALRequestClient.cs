using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using MyAnimeList.Constants;
using MyAnimeList.Exceptions;

namespace MyAnimeList;

public static class MALRequestClient
{
    private static HttpClient? _myAnimeListClient;

    /// <summary>
    ///     A static constructor used to create the httpclient
    /// </summary>
    public static async Task<bool> Init(bool requireAuthInit = false)
    {
        if (requireAuthInit & _myAnimeListClient == null)
            await OAuth.Init();

        if (_myAnimeListClient == null)
        {
            _myAnimeListClient = HttpClientFactory.Create();
            _myAnimeListClient.DefaultRequestHeaders.Accept.Clear();
            _myAnimeListClient.BaseAddress = new Uri("https://api.myanimelist.net/");
            _myAnimeListClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        return true;
    }

    /// <summary>
    ///     Used to preform a get request to my anime list
    /// </summary>
    /// <param name="url">The url that a request will be made to</param>
    /// <param name="requiresLogin">Whether the request needs the user to be logged in to be fulfilled</param>
    /// <typeparam name="T">Generic return type</typeparam>
    /// <returns>A formatted object of type T, null if bad request</returns>
    public static async Task<T?> Get<T>(string url, bool requiresLogin = false)
    {
        if (_myAnimeListClient == null)
            throw new HttpClientNotInitialized("Client not initialized, did you call the Init function?");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        if (requiresLogin)
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", OAuth.GetToken());

        else
            requestMessage.Headers.Add("X-MAL-CLIENT-ID", MyAnimeListConstants.ClientId);

        var response = await _myAnimeListClient.SendAsync(requestMessage);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var unused = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadAsAsync<T>();
        }
        
        Debug.WriteLine($"Failure, status code: {response.StatusCode}");
        return default;
    }

    /// <summary>
    ///     Used to preform a get request to my anime list
    /// </summary>
    /// <param name="url">The url that a request will be made to</param>
    /// <param name="requiresLogin">Whether the request needs the user to be logged in to be fulfilled</param>
    /// <typeparam name="T">Generic return type</typeparam>
    /// <returns>A formatted object of type T, null if bad request</returns>
    public static async Task<T?> Get<T>(Uri url, bool requiresLogin = false)
    {
        if (_myAnimeListClient == null)
            throw new HttpClientNotInitialized("Client not initialized, did you call the Init function?");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        if (requiresLogin)
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", OAuth.GetToken());

        else
            requestMessage.Headers.Add("X-MAL-CLIENT-ID", MyAnimeListConstants.ClientId);

        var response = await _myAnimeListClient.SendAsync(requestMessage);
        if (response.StatusCode == HttpStatusCode.OK) return await response.Content.ReadAsAsync<T>();

        return default;
    }


    /// <summary>
    ///     Used to preform a delete request to my anime list
    /// </summary>
    /// <param name="url">The url that a request will be made to</param>
    /// <returns>True if the delete was successful else false if not found</returns>
    public static async Task<bool?> Delete(string url)
    {
        if (_myAnimeListClient == null)
            throw new HttpClientNotInitialized("Client not initialized, did you call the Init function?");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", OAuth.GetToken());

        var response = await _myAnimeListClient.SendAsync(requestMessage);

        return response.StatusCode switch
        {
            HttpStatusCode.OK => true,
            HttpStatusCode.NotFound => false,
            _ => default
        };
    }

    /// <summary>
    ///     Used to preform a delete request to my anime list
    /// </summary>
    /// <param name="url">The url that a request will be made to</param>
    /// <returns>True if the delete was successful else false</returns>
    public static async Task<bool> Delete(Uri url)
    {
        if (_myAnimeListClient == null)
            throw new HttpClientNotInitialized("Client not initialized, did you call the Init function?");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", OAuth.GetToken());

        var response = await _myAnimeListClient.SendAsync(requestMessage);

        return response.StatusCode switch
        {
            HttpStatusCode.OK => true,
            HttpStatusCode.NotFound => false,
            _ => default
        };
    }

    /// <summary>
    ///     Used to preform a patch request to my anime list
    /// </summary>
    /// <param name="url">The url that a request will be made to</param>
    /// <param name="body">The request body</param>
    /// <typeparam name="T">Generic return type</typeparam>
    /// <returns>A formatted object of type T, null if bad request</returns>
    public static async Task<T?> Patch<T>(string url, FormUrlEncodedContent body)
    {
        if (_myAnimeListClient == null)
            throw new HttpClientNotInitialized("Client not initialized, did you call the Init function?");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Patch, url);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", OAuth.GetToken());

        body.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        requestMessage.Content = body;

        var response = await _myAnimeListClient.SendAsync(requestMessage);
        if (response.StatusCode == HttpStatusCode.OK) return await response.Content.ReadAsAsync<T>();

        return default;
    }

    /// <summary>
    ///     Used to preform a patch request to my anime list
    /// </summary>
    /// <param name="url">The url that a request will be made to</param>
    /// <param name="body">The request body</param>
    /// <typeparam name="T">Generic return type</typeparam>
    /// <returns>A formatted object of type T, null if bad request</returns>
    public static async Task<T?> Patch<T>(Uri url, FormUrlEncodedContent body)
    {
        if (_myAnimeListClient == null)
            throw new HttpClientNotInitialized("Client not initialized, did you call the Init function?");

        using var requestMessage = new HttpRequestMessage(HttpMethod.Patch, url);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", OAuth.GetToken());


        var response = await _myAnimeListClient.SendAsync(requestMessage);
        if (response.StatusCode == HttpStatusCode.OK) return await response.Content.ReadAsAsync<T>();

        return default;
    }
}