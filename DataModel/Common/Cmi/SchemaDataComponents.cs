using System;
using System.Diagnostics.CodeAnalysis;

namespace IUDICO.DataModel.Common.Cmi
{
  internal abstract class BaseSchemaInternal
  {
    /// <summary>
    /// Contains constants related to the ActivityAttemptItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class ActivityAttemptItem
    {

      /// <summary>
      /// Maximum length of the Location property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxLocationLength = 1000;

      /// <summary>
      /// Maximum length of the SuspendData property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxSuspendDataLength = 64000;
      //public const int MaxSuspendDataLength = 4096;
    }

    /// <summary>
    /// Contains constants related to the ActivityObjectiveItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class ActivityObjectiveItem
    {

      /// <summary>
      /// Maximum length of the Key property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxKeyLength = 4096;
    }

    /// <summary>
    /// Contains constants related to the ActivityPackageItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class ActivityPackageItem
    {

      /// <summary>
      /// Maximum length of the ActivityIdFromManifest property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxActivityIdFromManifestLength = 4096;

      /// <summary>
      /// Maximum length of the PrimaryResourceFromManifest property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxPrimaryResourceFromManifestLength = 2000;

      /// <summary>
      /// Maximum length of the LaunchData property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxLaunchDataLength = 4096;

      /// <summary>
      /// Maximum length of the ResourceParameters property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxResourceParametersLength = 1000;

      /// <summary>
      /// Maximum length of the Title property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxTitleLength = 200;
    }

    /// <summary>
    /// Contains constants related to the PackageGlobalObjectiveItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class PackageGlobalObjectiveItem
    {
    }

    /// <summary>
    /// Contains constants related to the AttemptItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class AttemptItem
    {
    }

    /// <summary>
    /// Contains constants related to the AttemptObjectiveItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class AttemptObjectiveItem
    {

      /// <summary>
      /// Maximum length of the Description property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxDescriptionLength = 255;

      /// <summary>
      /// Maximum length of the Key property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxKeyLength = 4096;
    }

    /// <summary>
    /// Contains constants related to the CommentFromLearnerItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class CommentFromLearnerItem
    {

      /// <summary>
      /// Maximum length of the Comment property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxCommentLength = 4096;

      /// <summary>
      /// Maximum length of the Location property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxLocationLength = 255;

      /// <summary>
      /// Maximum length of the Timestamp property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxTimestampLength = 28;
    }

    /// <summary>
    /// Contains constants related to the CommentFromLmsItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class CommentFromLmsItem
    {

      /// <summary>
      /// Maximum length of the Comment property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxCommentLength = 4096;

      /// <summary>
      /// Maximum length of the Location property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxLocationLength = 255;

      /// <summary>
      /// Maximum length of the Timestamp property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxTimestampLength = 28;
    }

    /// <summary>
    /// Contains constants related to the CorrectResponseItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class CorrectResponseItem
    {

      /// <summary>
      /// Maximum length of the ResponsePattern property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxResponsePatternLength = 1073741822;
    }

    /// <summary>
    /// Contains constants related to the EvaluationCommentItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class EvaluationCommentItem
    {

      /// <summary>
      /// Maximum length of the Comment property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxCommentLength = 4096;

      /// <summary>
      /// Maximum length of the Location property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxLocationLength = 255;

      /// <summary>
      /// Maximum length of the Timestamp property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxTimestampLength = 28;
    }

    /// <summary>
    /// Contains constants related to the ExtensionDataItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class ExtensionDataItem
    {

      /// <summary>
      /// Maximum length of the Name property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxNameLength = 256;

      /// <summary>
      /// Maximum length of the AttachmentValue property in bytes.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxAttachmentValueLength = 2147483645;

      /// <summary>
      /// Maximum length of the StringValue property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxStringValueLength = 4096;
    }

    /// <summary>
    /// Contains constants related to the GlobalObjectiveItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class GlobalObjectiveItem
    {

      /// <summary>
      /// Maximum length of the Key property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxKeyLength = 4096;

      /// <summary>
      /// Maximum length of the Description property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxDescriptionLength = 4096;
    }

    /// <summary>
    /// Contains constants related to the InteractionItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class InteractionItem
    {

      /// <summary>
      /// Maximum length of the InteractionIdFromCmi property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxInteractionIdFromCmiLength = 4096;

      /// <summary>
      /// Maximum length of the Timestamp property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxTimestampLength = 28;

      /// <summary>
      /// Maximum length of the Description property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxDescriptionLength = 255;

      /// <summary>
      /// Maximum length of the LearnerResponseString property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxLearnerResponseStringLength = 1073741822;
    }

    /// <summary>
    /// Contains constants related to the InteractionObjectiveItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class InteractionObjectiveItem
    {
    }

    /// <summary>
    /// Contains constants related to the LearnerGlobalObjectiveItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class LearnerGlobalObjectiveItem
    {
    }

    /// <summary>
    /// Contains constants related to the MapActivityObjectiveToGlobalObjectiveItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class MapActivityObjectiveToGlobalObjectiveItem
    {
    }

    /// <summary>
    /// Contains constants related to the PackageItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class PackageItem
    {

      /// <summary>
      /// Maximum length of the Location property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxLocationLength = 260;
    }

    /// <summary>
    /// Contains constants related to the RubricItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class RubricItem
    {
    }

    /// <summary>
    /// Contains constants related to the ResourceItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class ResourceItem
    {
    }

    /// <summary>
    /// Contains constants related to the SequencingLogEntryItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class SequencingLogEntryItem
    {

      /// <summary>
      /// Maximum length of the Message property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxMessageLength = 1073741822;
    }

    /// <summary>
    /// Contains constants related to the UserItem item type.
    /// </summary>
    [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
    [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
    public abstract class UserItem
    {

      /// <summary>
      /// Maximum length of the Key property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxKeyLength = 250;

      /// <summary>
      /// Maximum length of the Name property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxNameLength = 255;

      /// <summary>
      /// Maximum length of the Language property in characters.
      /// </summary>
      [SuppressMessageAttribute("Microsoft.Naming", "CA1726")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1702")]
      [SuppressMessageAttribute("Microsoft.Naming", "CA1704")]
      public const int MaxLanguageLength = 255;
    }
  }
}
