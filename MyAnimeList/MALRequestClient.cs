using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace MyAnimeList
{
    public class MALRequestClient
    {
        private static HttpClient _myAnimeListClient;
        
        /// <summary>
        /// A static constructor used to create the httpclient
        /// </summary>
        public static void Init()
        {
            //OAuth.Init();
            //OAuth.Login();
            
            if (_myAnimeListClient == null)
            {
                _myAnimeListClient = HttpClientFactory.Create();
                _myAnimeListClient.DefaultRequestHeaders.Accept.Clear();
                _myAnimeListClient.BaseAddress = new Uri("https://api.myanimelist.net/");
                _myAnimeListClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
        
        /// <summary>
        /// Used to preform a get request to my anime list
        /// </summary>
        /// <param name="url"> The url that a request will be made to </param>
        /// <param name="requiresLogin"> Whether the request needs the user to be logged in to be fulfilled </param>
        /// <typeparam name="T"> Generic return type </typeparam>
        /// <returns> A formatted object of type T, null if bad request  </returns>
        public static async Task<T?> Get<T>(string url, bool requiresLogin = false)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (requiresLogin)
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "token goes here");
            
            else
                requestMessage.Headers.Add("X-MAL-CLIENT-ID", Constants.MyAnimeListConstants.ClientId);
            
            /*Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse)*/
            var response = await _myAnimeListClient.SendAsync(requestMessage);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            
            return default;
        }
        
        /// <summary>
        /// Used to preform a get request to my anime list
        /// </summary>
        /// <param name="url"> The url that a request will be made to </param>
        /// <typeparam name="T"> Generic return type </typeparam>
        /// <returns> A formatted object of type T, null if bad request </returns>
        public static async Task<T> Get<T>(Uri url)
        {
            var response = await _myAnimeListClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default;
        }
        
        
        /// <summary>
        /// Used to preform a delete request to my anime list
        /// </summary>
        /// <param name="url"> The url that a request will be made to </param>
        /// <returns> True if the delete was successful else false </returns>
        public static async Task<bool> Delete(string url)
        {
            var response = await _myAnimeListClient.DeleteAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Used to preform a delete request to my anime list
        /// </summary>
        /// <param name="url"> The url that a request will be made to </param>
        /// <returns> True if the delete was successful else false </returns>
        public static async Task<bool> Delete(Uri url)
        {
            var response = await _myAnimeListClient.DeleteAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Used to preform a patch request to my anime list
        /// </summary>
        /// <param name="url"> The url that a request will be made to </param>
        /// <param name="body"> The request body </param>
        /// <typeparam name="T"> Generic return type </typeparam>
        /// <returns> A formatted object of type T, null if bad request </returns>
        public static async Task<T> Patch<T>(string url, FormUrlEncodedContent body)
        {
            var response = await _myAnimeListClient.PatchAsync(url, body);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default;
        }
        
        /// <summary>
        /// Used to preform a patch request to my anime list
        /// </summary>
        /// <param name="url"> The url that a request will be made to </param>
        /// <param name="body"> The request body </param>
        /// <typeparam name="T"> Generic return type </typeparam>
        /// <returns> A formatted object of type T, null if bad request </returns>
        public static async Task<T> Patch<T>(Uri url, FormUrlEncodedContent body)
        {
            var response = await _myAnimeListClient.PatchAsync(url, body);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default;
        }
    }
}