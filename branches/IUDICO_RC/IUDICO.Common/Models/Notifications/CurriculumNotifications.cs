using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Notifications
{
    /// <summary>
    /// Holds Discipline - related notification's name constants with description.
    /// </summary>
    public static class DisciplineNotifications
    {
        /// <summary>
        /// Discipline Create notification is sent when discipline has been created(added).
        /// </summary>
        public const string DisciplineCreated = "discipline/created";

        /// <summary>
        /// Discipline Edit notification is sent when discipline has been modified (updated).
        /// </summary>
        public const string DisciplineEdited = "discipline/edited";

        /// <summary>
        /// Discipline Invalid notification is sent when discipline was made invalid.
        /// </summary>
        public const string DisciplineIsValidChange = "discipline/changeiIsValid";

        /// <summary>
        /// Discipline Delete notification is sent when discipline are being deleted.
        /// </summary>
        public const string DisciplineDeleting = "discipline/deleting";

        /// <summary>
        /// Discipline Delete notification is sent when discipline has been deleted.
        /// </summary>
        public const string DisciplineDeleted = "discipline/deleted";

        /// <summary>
        /// Chapter Create notification is sent when chapter has been created(added).
        /// </summary>
        public const string ChapterCreated = "chapter/created";

        /// <summary>
        /// Chapter Delete notification is sent when chapter are being deleted.
        /// </summary>
        public const string ChapterDeleting = "chapter/deleting";

        /// <summary>
        /// Topic Create notification is sent when topic has been created(added).
        /// </summary>
        public const string TopicCreated = "topic/created";

        /// <summary>
        /// Topic Edit notification is sent when topic has been modified (updated).
        /// </summary>
        public const string TopicEdited = "topic/edited";

        /// <summary>
        /// Topic Delete notification is sent when topic are being deleted.
        /// </summary>
        public const string TopicDeleting = "topic/deleting";

        /// <summary>
        /// Topic Delete notification is sent when topic has been deleted.
        /// </summary>
        public const string TopicDeleted = "topic/deleted";
    }
}
