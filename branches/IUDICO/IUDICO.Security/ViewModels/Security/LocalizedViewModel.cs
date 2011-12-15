using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common;

namespace IUDICO.Security.ViewModels.Security
{
    public class LocalizedViewModel
    {
        private LocalizationMessageProvider _provider;

        public LocalizedViewModel(LocalizationMessageProvider provider)
        {
            _provider = provider;
        }

        public String GetMessage(String key)
        {
            return _provider.getMessage(key);
        }
    }
}