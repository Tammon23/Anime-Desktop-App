using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class ExampleHttpClient
{
    static async Task NotMain()
    {
        // https://www.youtube.com/watch?v=k-5_xI3W0dk
        var httpClient = HttpClientFactory.Create();

        var url = "https://jsonplaceholder.typicode.com/todos/1";
        var data = await httpClient.GetAsync(url);

        if (data.StatusCode == HttpStatusCode.OK)
        {
            var content = data.Content;
            var result = await content.ReadAsAsync<Data2>();
            Console.WriteLine(result);
        }
    }


    private class Data2
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }

        public override string ToString()
        {
            return $"{UserId}, {Id}, {Title}, {Completed}";
            
        }
    }
}