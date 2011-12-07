using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;

namespace IUDICO.Security.Models
{
    public class SecurityService : ISecurityService
    {
        private const int MAXIMUM_VALID_CONTENT_LENGTH = 500;

        public bool CheckRequestSafety(HttpRequestBase request)
        {
            return request.ContentLength <= MAXIMUM_VALID_CONTENT_LENGTH;
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