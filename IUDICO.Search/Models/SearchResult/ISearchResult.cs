using System;

namespace IUDICO.Search.Models.SearchResult
{
    public interface ISearchResult
    {
        int GetId();
        string GetName();
        string GetText();
        string GetUrl();
    }
}