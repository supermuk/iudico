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
    
    public enum InteractionType
    {
        Other = 0,
        FillIn = 1,
        Likert = 2,
        LongFillIn = 3,
        Matching = 4,
        MultipleChoice = 5,
        Numeric = 6,
        Performance = 7,
        Sequencing = 8,
        TrueFalse = 9,
        Essay = 10,
        Attachment = 11
    }
}
