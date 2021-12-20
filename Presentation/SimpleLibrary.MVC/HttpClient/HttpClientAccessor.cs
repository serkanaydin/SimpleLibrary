namespace SimpleLibrary.MVC.HttpClient;

public class HttpClientAccessor : IHttpClient
{
    public System.Net.Http.HttpClient Client { get; set; }

    public HttpClientAccessor()
    {
        Client = new System.Net.Http.HttpClient();
    }
}