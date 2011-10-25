using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.Analytics.Models.Storage
{
    public class MixedAnalyticsStorage: IAnalyticsStorage
    {
        private readonly ILmsService _LmsService;
        private DBDataContext _Db;
        
        protected DBDataContext GetDbDataContext()
        {
            return _LmsService.GetDbDataContext();
        }

        public MixedAnalyticsStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
            RefreshState();
        }

        public void RefreshState()
        {
            _Db = _LmsService.GetDbDataContext();
        }


    }
}