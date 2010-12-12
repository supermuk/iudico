using System;

namespace IUDICO.Search.Models.SearchResult
{
    public interface ISearchResult
    {
        int GetId();
        String GetName();
        String GetText();
        String GetUrl();
    }
}