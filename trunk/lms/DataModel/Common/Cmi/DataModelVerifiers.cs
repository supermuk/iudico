using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace IUDICO.DataModel.Common.Cmi
{
  /// <summary>
  /// Validator for cmi properties elements
  /// </summary>
  public class DataModelVerifier
  {
    private string elementName;

    public DataModelVerifier(string elementName)
    {
      this.elementName = elementName;
    }

    public void Validate(string value)
    {
      switch (elementName)
      {
        case "completion_status":
          DataModelElementsVerifier.ValidateCompletionStatus(value);
          break;
        case "credit":
          DataModelElementsVerifier.ValidateCredit(value);
          break;
        case "entry":
          DataModelElementsVerifier.ValidateEntry(value);
          break;
        case "location":
          DataModelElementsVerifier.ValidateLocation(value);
          break;
        case "mode":
          DataModelElementsVerifier.ValidateMode(value);
          break;
        case "progress_measure":
          DataModelElementsVerifier.ValidateProgressMeasure(value);
          break;
        case "scaled_passing_score":
          DataModelElementsVerifier.ValidateScaledPassingScore(value);
          break;
        case "success_status":
          DataModelElementsVerifier.ValidateSuccessStatus(value);
          break;
        case "suspend_data":
          DataModelElementsVerifier.ValidateSuspendData(value);
          break;
        case "time_limit_action":
          DataModelElementsVerifier.ValidateTimeLimitAction(value);
          break;
        case "session_time":
          DataModelElementsVerifier.ValidateSessionTime(value);
          break;

        case "interaction.id":
          DataModelInteractionVerifier.ValidateId(value);
          break;
        case "interaction.type":
          DataModelInteractionVerifier.ValidateType(value);
          break;
        case "interaction.timestamp":
          DataModelInteractionVerifier.ValidateTimeStamp(value);
          break;
        case "interaction.weighting":
          DataModelInteractionVerifier.ValidateWeighting(value);
          break;
        case "interaction.result":
          DataModelInteractionVerifier.ValidateResult(value);
          break;
        case "interaction.latency":
          DataModelInteractionVerifier.ValidateLatency(value);
          break;
        case "interaction.description":
          DataModelInteractionVerifier.ValidateDescription(value);
          break;
        case "interaction.learner_response":
          DataModelInteractionVerifier.ValidateLearnerResponse(value);
          break;

        case "interaction.objective.id":
          DataModelInteractionObjectiveVerifier.ValidateId(value);
          break;

        case "interaction.correct_response.pattern":
          DataModelCorrectResponseVerifier.ValidatePattern(value);
          break;

        case "score.scaled":
          DataModelScoreVerifier.ValidateScaled(value);
          break;
        case "score.row":
          DataModelScoreVerifier.ValidateRow(value);
          break;
        case "score.max":
          DataModelScoreVerifier.ValidateMaximum(value);
          break;
        case "score.min":
          DataModelScoreVerifier.ValidateMinimum(value);
          break;
        default:
          break;
      }
    }
  }

  internal class DataModelElementsVerifier
  {
    /// <summary>
    /// All normal enum values are valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateCompletionStatus(string value)
    {
      switch (value)
      {
        case "completed":
        case "incomplete":
        case "not_attempted":
        case "unknown":
          break;
        default:
          throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// All normal enum values are valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateCredit(string value)
    {
      switch (value)
      {
        case "credit":
        case "no-credit":
          break;
        default:
          throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// All normal enum values are valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateEntry(string value)
    {
      switch (value)
      {
        case "ab-initio":
        case "":
        case "resume":
          break;
        default:
          throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Just validate for length.  SCORM 2004 defines an SPM of 1000.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateLocation(string value)
    {
      if (value != null && value.Length > BaseSchemaInternal.ActivityAttemptItem.MaxLocationLength)
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// All normal enum values are valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateMode(string value)
    {
      switch (value)
      {
        case "browse":
        case "normal":
        case "review":
          break;
        default:
          throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Valid values are 0.0 to 1.0
    /// </summary>
    /// <param name="value">The value to validate</param>
    public static void ValidateProgressMeasure(string stringValue)
    {
      float value;
      if (float.TryParse(stringValue, out value))
      {
        if (value < 0.0 || value > 1.0 || Single.IsNaN(value) || Single.IsInfinity(value))
        {
          throw new ArgumentOutOfRangeException("value");
        }
      }
      else
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Valid values are 0.0 to 1.0
    /// </summary>
    /// <param name="value">The value to validate</param>
    public static void ValidateScaledPassingScore(string stringValue)
    {
      float value;
      if (float.TryParse(stringValue, out value))
      {
        if (value < 0.0 || value > 1.0 || Single.IsNaN(value) || Single.IsInfinity(value))
        {
          throw new ArgumentOutOfRangeException("value");
        }
      }
      else
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// All normal enum values are valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateSuccessStatus(string value)
    {
      switch (value)
      {
        case "failed":
        case "passed":
        case "unknown":
          break;
        default:
          throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Just validate for length.  SCORM 2004 defines an SPM of 4000.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateSuspendData(string value)
    {
      if (value != null && value.Length > BaseSchemaInternal.ActivityAttemptItem.MaxSuspendDataLength)
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// All normal enum values are valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateTimeLimitAction(string value)
    {
      switch (value)
      {
        case "exit,message":
        case "continue,message":
        case "exit,no message":
        case "continue,no message":
          break;
        default:
          throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// All values are valid
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateTotalTime(string value)
    {
    }

    /// <summary>
    /// All values are valid
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateSessionTime(string value)
    {
    }
  }

  internal class DataModelInteractionVerifier
  {
    /// <summary>
    /// Validates that the ID is a valid URI. String length is validated according to MLC limits.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateId(string value)
    {
      if (String.IsNullOrEmpty(value))
      {
        throw new ArgumentException("no identifier");
      }
      if (value.Length > BaseSchemaInternal.InteractionItem.MaxInteractionIdFromCmiLength)
      {
        throw new ArgumentOutOfRangeException("value");
      }
      if (!Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
      {
        throw new ArgumentException("incorrect format");
      }
    }

    /// <summary>
    /// All normal enum values are valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateType(string value)
    {
      switch (value)
      {
        case "fill-in":
        case "likert":
        case "long-fill-in":
        case "matching":
        case "choice":
        case "numeric":
        case "other":
        case "performance":
        case "sequencing":
        case "true-false":
          break;
        default:
          throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Validates the value as a time(second, 10, 0)
    /// </summary>
    /// <param name="value">The value to validate</param>
    public static void ValidateTimeStamp(string value)
    {
      string[] ValidTimeStampFormats = {"yyyy",
                                        "yyyy-MM",
                                        "yyyy-MM-dd",
                                        "yyyy-MM-ddTHH",
                                        "yyyy-MM-ddTHH:mm",
                                        "yyyy-MM-ddTHH:mm:ss",
                                        "yyyy-MM-ddTHH:mm:ss.f",
                                        "yyyy-MM-ddTHH:mm:ss.fzz",
                                        "yyyy-MM-ddTHH:mm:ss.fzzz",
                                        "yyyy-MM-ddTHH:mm:ss.fZ",
                                        "yyyy-MM-ddTHH:mm:ss.ff",
                                        "yyyy-MM-ddTHH:mm:ss.ffzz",
                                        "yyyy-MM-ddTHH:mm:ss.ffzzz",
                                        "yyyy-MM-ddTHH:mm:ss.ffZ"};

      // let DateTime.ParseExact throw an appropriate exception if the string
      // is not valid
      DateTime d = DateTime.ParseExact(value, ValidTimeStampFormats,
                                       CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
      // check that the years are in SCORM valid range
      if (d.Year < 1970 || d.Year > 2038)
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Any value is valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateWeighting(string stringValue)
    {
      float value;
      if (float.TryParse(stringValue, out value))
      {
        if (Single.IsNaN(value) || Single.IsInfinity(value))
        {
          throw new ArgumentOutOfRangeException("value");
        }
      }
      else
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Validates learner response
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateLearnerResponse(string value)
    {
      float fvalue;
      bool bvalue;
      if (float.TryParse(value, out fvalue))
      {
        if (Single.IsNaN(fvalue) || Single.IsInfinity(fvalue))
        {
          throw new ArgumentOutOfRangeException("Invalid Learner Response");
        }
      }
      else if (value != null)
      {
        if (value.Length > BaseSchemaInternal.InteractionItem.MaxLearnerResponseStringLength)
        {
          throw new ArgumentOutOfRangeException("value");
        }
      }
      else if (!(value == null || bool.TryParse(value, out bvalue)))
      {
        throw new ArgumentException("Invalid Learner Response");
      }
    }

    /// <summary>
    /// Any valid enum value is valid for value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateResult(string value)
    {
      switch (value)
      {
        case "correct":
        case "incorrect":
        case "neutral":
        case "unanticipated":
          break;
        default:
          float fvalue;
          if (float.TryParse(value, out fvalue))
          {
            if (Single.IsNaN(fvalue) || Single.IsInfinity(fvalue))
            {
              throw new ArgumentOutOfRangeException("value");
            }
          }
          else
          {
            throw new ArgumentOutOfRangeException("value");
          }
          break;
      }
    }

    /// <summary>
    /// Any valid TimeSpan is valid.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateLatency(string value)
    {
    }

    /// <summary>
    /// Just validate for length.  SCORM 2004 defines an SPM of 250. String length is validated according to MLC limits.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateDescription(string value)
    {
      if (value != null && value.Length > BaseSchemaInternal.InteractionItem.MaxDescriptionLength)
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }
  }

  internal class DataModelInteractionObjectiveVerifier
  {
    /// <summary>
    /// Validates that the ID is a valid URI and is within the defined length for SCORM 2004.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    public static void ValidateId(string value)
    {
      if (String.IsNullOrEmpty(value))
      {
        throw new ArgumentException("null argument");
      }
      if (value.Length > BaseSchemaInternal.AttemptObjectiveItem.MaxKeyLength)
      {
        throw new ArgumentOutOfRangeException("value");
      }
      if (!Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
      {
        throw new ArgumentException("not well-formed argument");
      }
    }
  }

  internal class DataModelCorrectResponseVerifier
  {
    /// <summary>
    /// Validate the value against the max database length.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <remarks>
    /// Since the actual valid contents of the string vary based on the 
    /// Interaction.InteractionType, it is not reliably possible to validate this 
    /// field for anything beyond length without getting into some questions that 
    /// we really don't want to answer (e.g. what should we do with these values 
    /// if the type has not yet been set, what should be done with them if the 
    /// type changes).
    /// </remarks>
    public static void ValidatePattern(string value)
    {
      if (value != null && value.Length > BaseSchemaInternal.CorrectResponseItem.MaxResponsePatternLength)
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }
  }

  internal class DataModelScoreVerifier
  {
    /// <summary>
    /// Valid values are -1.0 to 1.0
    /// </summary>
    /// <param name="value">The value to validate</param>
    public static void ValidateScaled(string value)
    {
      float fvalue;
      if (float.TryParse(value, out fvalue))
      {
        if (fvalue < -1.0 || fvalue > 1.0 || Single.IsNaN(fvalue) || Single.IsInfinity(fvalue))
        {
          throw new ArgumentOutOfRangeException("value");
        }
      }
      else
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Any value is valid
    /// </summary>
    /// <param name="value">The value to validate</param>
    public static void ValidateRow(string value)
    {
      float fvalue;
      if (float.TryParse(value, out fvalue))
      {
        if (Single.IsNaN(fvalue) || Single.IsInfinity(fvalue))
        {
          throw new ArgumentOutOfRangeException("value");
        }
      }
      else
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Any value is valid
    /// </summary>
    /// <param name="value">The value to validate</param>
    public static void ValidateMinimum(string value)
    {
      float fvalue;
      if (float.TryParse(value, out fvalue))
      {
        if (Single.IsNaN(fvalue) || Single.IsInfinity(fvalue))
        {
          throw new ArgumentOutOfRangeException("value");
        }
      }
      else
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }

    /// <summary>
    /// Any value is valid
    /// </summary>
    /// <param name="value">The value to validate</param>
    public static void ValidateMaximum(string value)
    {
      float fvalue;
      if (float.TryParse(value, out fvalue))
      {
        if (Single.IsNaN(fvalue) || Single.IsInfinity(fvalue))
        {
          throw new ArgumentOutOfRangeException("value");
        }
      }
      else
      {
        throw new ArgumentOutOfRangeException("value");
      }
    }
  }
}
