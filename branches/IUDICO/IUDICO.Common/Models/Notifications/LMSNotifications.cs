using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Notifications
{
    /// <summary>
    /// Holds LMS - related notification's name constants with description.
    /// </summary>
    public static class LMSNotifications
    {
        /// <summary>
        /// Application Start notification is sent when application has been started.
        /// </summary>
        public const string ApplicationStart = "application/start";

        /// <summary>
        /// Application Stop notification is sent when application has been stopped.
        /// </summary>
        public const string ApplicationStop = "application/stop";

        /// <summary>
        /// Application Request Begin notification is sent when new requested came to application.
        /// </summary>
        public const string ApplicationRequestStart = "application/request/start";

        /// <summary>
        /// Application Request End notification is sent when requested has been processed by applcation.
        /// </summary>
        public const string ApplicationRequestEnd = "application/request/end";

        /// <summary>
        /// Notification is sent when actions are changed
        /// </summary>
        public const string ActionsChanged = "lms/actions/changed";
    }
}
