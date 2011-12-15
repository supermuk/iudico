namespace IUDICO.Common.Models.Notifications
{
    /// <summary>
    /// Holds Course - related notification's name constants with description.
    /// </summary>
    public static class CourseNotifications
    {
        /// <summary>
        /// Course Create notification is sent when course has been created(added).
        /// <param name="course">Course value represents created course object.</param>
        /// </summary>
        public const string CourseCreate = "course/create";

        /// <summary>
        /// Course Edit notification is sent when course has been modified (updated).
        /// <param name="course">Course value represents edited course object.</param>
        /// </summary>
        public const string CourseEdit = "course/edit";

        /// <summary>
        /// Course Delete notification is sent when course has been deleted.
        /// <param name="course">Course value represents deleted course object.</param>
        /// </summary>
        public const string CourseDelete = "course/delete";
    }
}