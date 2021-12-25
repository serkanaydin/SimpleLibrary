namespace SimpleLibrary.MVC.HttpClient;

public interface IHttpClient
{
    System.Net.Http.HttpClient Client { get; set; }

}