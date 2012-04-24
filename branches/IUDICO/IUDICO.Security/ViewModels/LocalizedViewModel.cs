using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common;

namespace IUDICO.Security.ViewModels
{
    public class LocalizedViewModel
    {
        private LocalizationMessageProvider provider;

        public LocalizedViewModel()
        {
            this.provider = Localization.GetProvider();
        }

        public LocalizedViewModel(LocalizationMessageProvider provider)
        {
            this.provider = provider;
        }

        public string GetMessage(string key)
        {
            return this.provider.GetMessage(key);
        }
    }
}