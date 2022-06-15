using System.Collections.Immutable;

namespace AnimeScraper.Codebase.Helpers;

[AttributeUsage(AttributeTargets.Field)] 
public class ProviderAttribute : Attribute  
{  
    public ProviderAttribute(string baseUri, string? filterPath = null, string? contentPath = null, params string[] baseUriAlternatives)
    {
        BaseUri = baseUri;
        FilterPath = filterPath;
        ContentPath = contentPath;
        BaseUriAlternatives = baseUriAlternatives.ToImmutableArray();
    }

    /*public UriBuilder BuildSearchPath()
    {
        if (FilterPath == null) return new UriBuilder(BaseUri);
        return FilterPath.ToLower().StartsWith("http") ? new UriBuilder(FilterPath) : new UriBuilder(BaseUri + FilterPath);
    }

    public UriBuilder BuildContentPath()
    {
        if (ContentPath == null) return new UriBuilder(BaseUri);
        return ContentPath.ToLower().StartsWith("http") ? new UriBuilder(ContentPath) : new UriBuilder(BaseUri + ContentPath);
    }*/

    public string BaseUri { get; }

    public string? FilterPath { get; }

    public string? ContentPath { get; }

    public ImmutableArray<string>? BaseUriAlternatives { get; }
}   

public static class ProviderEnumExtenstionClass  
{  
    public static string GetBaseUri(this Enum value)  
    {  
        var type = value.GetType();  
        var fieldInfo = type.GetField(value.ToString());  
        var attribs = fieldInfo!.GetCustomAttributes(  
            typeof(ProviderAttribute), false) as ProviderAttribute[];

        return attribs![0].BaseUri;
    }
    
    public static string? GetFilterPath(this Enum value)  
    {  
        var type = value.GetType();  
        var fieldInfo = type.GetField(value.ToString());  
        var attribs = fieldInfo?.GetCustomAttributes(  
            typeof(ProviderAttribute), false) as ProviderAttribute[];

        return attribs?.Length > 0 ? attribs[0].FilterPath : null;
    }  
    
    public static string? GetContentPath(this ProviderEnum value)  
    {  
        var type = value.GetType();  
        var fieldInfo = type.GetField(value.ToString());  
        var attribs = fieldInfo?.GetCustomAttributes(  
            typeof(ProviderAttribute), false) as ProviderAttribute[];

        return attribs?.Length > 0 ? attribs[0].ContentPath : null;
    }  
    
    public static ImmutableArray<string>? GetBaseUriAlternatives(this ProviderEnum value)  
    {  
        var type = value.GetType();  
        var fieldInfo = type.GetField(value.ToString());  
        var attribs = fieldInfo?.GetCustomAttributes(  
            typeof(ProviderAttribute), false) as ProviderAttribute[];

        return attribs?.Length > 0 ? attribs[0].BaseUriAlternatives : null;
    }
    
    public static Uri BuildSearchPath(this ProviderEnum value)
    {
        var filterPath = value.GetFilterPath();
        var baseUri = value.GetBaseUri();

        if (filterPath == null) return new Uri(baseUri);
        return filterPath.ToLower().StartsWith("http") ? new Uri(filterPath) : new Uri(baseUri + filterPath);
    }

    public static Uri BuildContentPath(this ProviderEnum value)
    {
        var contentPath = value.GetContentPath();
        var baseUri = value.GetBaseUri();
        if (contentPath == null) return new Uri(baseUri);
        return contentPath.ToLower().StartsWith("http") ? new Uri(contentPath) : new Uri(baseUri + contentPath);
    }
} 