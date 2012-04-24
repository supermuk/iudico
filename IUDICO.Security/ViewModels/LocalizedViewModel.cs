using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common;

namespace IUDICO.Security.ViewModels
{
    public class LocalizedViewModel
    {
        private LocalizationMessageProvider _provider;

        public LocalizedViewModel()
        {
            _provider = Localization.GetProvider();
        }

        public LocalizedViewModel(LocalizationMessageProvider provider)
        {
            _provider = provider;
        }

        public String GetMessage(String key)
        {
            return _provider.GetMessage(key);
        }
    }
}