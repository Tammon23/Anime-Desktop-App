namespace AnimeScraper.Codebase.Helpers;

public interface IProvider : ISearcher
{
    public ProviderEnum GetProviderType();
}