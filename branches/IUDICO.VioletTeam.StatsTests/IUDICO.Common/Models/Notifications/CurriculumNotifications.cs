using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Notifications
{
    /// <summary>
    /// Holds Curriculum - related notification's name constants with description.
    /// </summary>
    public static class CurriculumNotifications
    {
        /// <summary>
        /// Curriculum Create notification is sent when curriculum has been created(added).
        /// <param name="curriculum">Curriculum value represents created course object.</param>
        /// </summary>
        public const string CurriculumCreate = "curriculum/create";

        /// <summary>
        /// Curriculum Edit notification is sent when curriculum has been modified (updated).
        /// <param name="curriculum">Curriculum value represents edited course object.</param>
        /// </summary>
        public const string CurriculumEdit = "curriculum/edit";

        /// <summary>
        /// Curriculum Delete notification is sent when curriculum has been deleted.
        /// <param name="curriculum">Curriculum value represents deleted course object.</param>
        /// </summary>
        public const string CurriculumDelete = "curriculum/delete";

        /// <summary>
        /// Theme Create notification is sent when theme has been created(added).
        /// <param name="curriculum">Curriculum value represents created course object.</param>
        /// </summary>
        public const string ThemeCreate = "theme/create";

        /// <summary>
        /// Theme Edit notification is sent when theme has been modified (updated).
        /// <param name="curriculum">Curriculum value represents edited course object.</param>
        /// </summary>
        public const string ThemeEdit = "theme/edit";

        /// <summary>
        /// Theme Delete notification is sent when theme has been deleted.
        /// <param name="curriculum">Curriculum value represents deleted course object.</param>
        /// </summary>
        public const string ThemeDelete = "theme/delete";
    }
}
