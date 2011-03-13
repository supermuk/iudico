using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    [Serializable]
    public enum TimeLimitAction
    {
        [XmlEnum(SCORM.ContinueNoMessage)]
        ContinueNoMessage = 0,
        [XmlEnum(SCORM.ContinueWithMessage)]
        ContinueWithMessage = 1,
        [XmlEnum(SCORM.ExitNoMessage)]
        ExitNoMessage = 2,
        [XmlEnum(SCORM.ExitWithMessage)]
        ExitWithMessage = 3,
    }

    [Serializable]
    public enum ScormType
    {
        [XmlEnum(SCORM.SCO)]
        SCO = 0,
        [XmlEnum(SCORM.Asset)]
        Asset = 1,
    }

    [Serializable]
    public enum ConditionCombination
    {
        [XmlEnum(SCORM.All)]
        All = 0,
        [XmlEnum(SCORM.Any)]
        Any = 1,
    }

    [Serializable]
    public enum Operator
    {
        [XmlEnum(SCORM.Not)]
        Not = 0,
        [XmlEnum(SCORM.NoOp)]
        NoOp = 1,
    }

    [Serializable]
    public enum Condition
    {
        [XmlEnum("satisfied")]
        Satisfied,
        [XmlEnum("objectiveStatusKnown")]
        ObjectiveStatusKnown,
        [XmlEnum("objectiveMeasureKnown")]
        ObjectiveMeasureKnown,
        [XmlEnum("objectiveMeasureGreaterThan")]
        ObjectiveMeasureGreaterThan,
        [XmlEnum("objectiveMeasureLessThan")]
        ObjectiveMeasureLessThan,
        [XmlEnum("completed")]
        Completed,
        [XmlEnum("activityProgressKnown")]
        ActivityProgressKnown,
        [XmlEnum("attempted")]
        Attempted,
        [XmlEnum("attemptLimitExceeded")]
        AttemptLimitExceeded,
        [XmlEnum("timeLimitExceeded")]
        TimeLimitExceeded,
        [XmlEnum("outsideAvailableTimeRange")]
        OutsideAvailableTimeRange,
        [XmlEnum("always")]
        Always,
    }

    [Serializable]
    public enum Action
    {
        [XmlEnum("skip")]
        Skip,
        [XmlEnum("disabled")]
        Disabled,
        [XmlEnum("hiddenFromChoise")]
        HiddenFromChoise,
        [XmlEnum("stopFromChoise")]
        StopFromChoise,
        [XmlEnum("stopForwardTraversal")]
        StopForwardTraversal,
        [XmlEnum("exitParent")]
        ExitParent,
        [XmlEnum("exitAll")]
        ExitAll,
        [XmlEnum("retry")]
        Retry,
        [XmlEnum("retryAll")]
        RetryAll,
        [XmlEnum("continue")]
        Continue,
        [XmlEnum("previous")]
        Previous,
        [XmlEnum("exit")]
        Exit,
    }

    [Serializable]
    public enum ChildActivitySet
    {
        [XmlEnum("all")]
        All,
        [XmlEnum("any")]
        Any,
        [XmlEnum("none")]
        None,
        [XmlEnum("atLeastCount")]
        AtLeastCount,
        [XmlEnum("atLeastPercent")]
        AtLeastPercent,
    }

    [Serializable]
    public enum RollupActions
    {
        [XmlEnum("satisfied")]
        Satisfied,
        [XmlEnum("notSatisfied")]
        NotSatisfied,
        [XmlEnum("completed")]
        Completed,
        [XmlEnum("incomplete")]
        Incomplete,
    }

    [Serializable]
    public enum Timing
    {
        [XmlEnum("never")]
        Never,
        [XmlEnum("once")]
        Once,
        [XmlEnum("onEachNewAttempt")]
        OnEachNewAttempt,
    }

    [Serializable]
    public enum Required
    {
        [XmlEnum("always")]
        Always,
        [XmlEnum("ifAttempted")]
        IfAttempted,
        [XmlEnum("ifNotSkipped")]
        IfNotSkipped,
        [XmlEnum("ifNotSuspended")]
        IfNotSuspended,
    }

    public enum SequencingPattern
    {
        OrganizationDefaultSequencingPattern,
        ControlChapterSequencingPattern,
        ForcedSequentialOrderSequencingPattern,
        ForcedForwardOnlySequencingPattern,
        PostTestSequencingPattern,
        RandomSetSequencingPattern,
        PrePostTestSequencingPattern,
        RandomPostTestSequencingPattern
    }
}