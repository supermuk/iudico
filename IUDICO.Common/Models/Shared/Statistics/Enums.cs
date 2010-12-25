namespace IUDICO.Common.Models.Shared.Statistics
{
    public enum CompletionStatus
    {
        Unknown = 0,
        Completed = 1,
        Incomplete = 2,
        NotAttempted = 3,
    }

    public enum AttemptStatus
    {
        Active = 0,
        Abandoned = 1,
        Completed = 2,
        Suspended = 3,
    }

    public enum SuccessStatus
    {
        Unknown = 0,
        Failed = 1,
        Passed = 2,
    }

    public enum LessonStatus
    {
        NotAttempted = 0,
        Browsed = 1,
        Completed = 2,
        Failed = 3,
        Incomplete = 4,
        Passed = 5,
    }
}
