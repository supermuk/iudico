using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;

namespace IUDICO.Security
{
    class SecurityService : ISecurityService
    {
        public bool CheckRequestSafety(HttpRequestBase request)
        {
            throw new NotImplementedException();
        }
        public HttpResponseBase ProcessRequest(HttpRequestBase request)
        {
            throw new NotImplementedException();
        }

        public bool CheckResponseSafety(HttpResponseBase response)
        {
            throw new NotImplementedException();
        }
        public HttpResponseBase ProcessResponse(HttpResponseBase response)
        {
            throw new NotImplementedException();
        }
    }
}