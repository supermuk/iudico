using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IUDICO.Common.Models.Services
{
    public interface ISecurityService : IService
    {
        bool CheckRequestSafety(HttpRequestBase request);
        HttpResponseBase ProcessRequest(HttpRequestBase request);
        
        bool CheckResponseSafety(HttpResponseBase response);
        HttpResponseBase ProcessResponse(HttpResponseBase response);
    }
}
