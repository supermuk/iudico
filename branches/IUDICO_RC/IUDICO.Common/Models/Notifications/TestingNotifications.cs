using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Common.Models.Notifications
{
    /// <summary>
    /// Holds Testing - related notification's name constants with description.
    /// </summary>
    public static class TestingNotifications
    {
        /// <summary>
        /// Testing completed is being dispatched, when user has submited testing and it's <see cref="AttemptStatus"/> has been set as Completed.
        /// </summary>
        public const string TestCompleted = "testing/completed";
    }
}
