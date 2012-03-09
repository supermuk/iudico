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
        /// <param name="discipline">Discipline value represents created course object.</param>
        /// </summary>
        public const string DisciplineCreate = "discipline/create";

        /// <summary>
        /// Discipline Edit notification is sent when discipline has been modified (updated).
        /// <param name="discipline">Discipline value represents edited course object.</param>
        /// </summary>
        public const string DisciplineEdit = "discipline/edit";

        /// <summary>
        /// Discipline Delete notification is sent when discipline has been deleted.
        /// <param name="discipline">Discipline value represents deleted course object.</param>
        /// </summary>
        public const string DisciplineDelete = "discipline/delete";

        /// <summary>
        /// Topic Create notification is sent when topic has been created(added).
        /// <param name="discipline">Discipline value represents created course object.</param>
        /// </summary>
        public const string TopicCreate = "topic/create";

        /// <summary>
        /// Topic Edit notification is sent when topic has been modified (updated).
        /// <param name="discipline">Discipline value represents edited course object.</param>
        /// </summary>
        public const string TopicEdit = "topic/edit";

        /// <summary>
        /// Topic Delete notification is sent when topic has been deleted.
        /// <param name="discipline">Discipline value represents deleted course object.</param>
        /// </summary>
        public const string TopicDelete = "topic/delete";
    }
}
