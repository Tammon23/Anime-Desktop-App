namespace AnimeScraper.Codebase.Helper;

public interface IProvider : ISearcher
{
    public void GetStream(string uri);
    public Task GetHtml();
    public ProviderEnum GetProvider();
}