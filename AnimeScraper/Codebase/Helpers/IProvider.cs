namespace AnimeScraper.Codebase.Helpers;

public interface IProvider : ISearcher
{
    public void GetStream(string uri);
    public Task GetHtml();
    public ProviderEnum GetProviderType();

    /// <summary>
    /// Static method used to get the instance of the provider, as providers follow the singleton design pattern
    /// </summary>
    /// <returns></returns>
    /*public IProvider GetProvider();*/
}