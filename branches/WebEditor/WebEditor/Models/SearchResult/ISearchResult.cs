using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEditor.Models.SearchResult
{
    public interface ISearchResult
    {
        int GetID();
        String GetName();
        String GetText();
        String GetUrl();
    }
}