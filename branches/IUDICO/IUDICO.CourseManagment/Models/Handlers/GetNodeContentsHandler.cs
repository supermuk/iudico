using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Messages.CourseMgt;
using IUDICO.CourseMgt.Models.Storage;
using MvcContrib.PortableAreas;

namespace IUDICO.CourseMgt.Models.Handlers
{
    public class GetNodeContentsHandler : MessageHandler<GetNodeContentsMessage>
    {
        public override void Handle(GetNodeContentsMessage message)
        {
            message.Result.Data = (HttpContext.Current.Application["CourseStorage"] as ICourseStorage).GetNodeContents(message.Input);
        }
    }
}