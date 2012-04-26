// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Rte1p2DataModelConverter.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

// using Resources;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

using IUDICO.TestingSystem;

using Microsoft.LearningComponents.DataModel;

namespace Microsoft.LearningComponents.Frameset
{
    using IUDICO.Common;

    /// <summary>
    /// Converts data model elements from SCORM 1.2 content to LearningDataModel elements, and vice versa.
    /// </summary>
    internal class Rte1p2DataModelConverter : RteDataModelConverter
    {
        /// <summary>
        /// Constructor. Create a converter for SCORM 1.2 content.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="dataModel"></param>
        internal Rte1p2DataModelConverter(SessionView view, LearningDataModel dataModel)
            : base(view, dataModel)
        {
        }

        /// <summary>
        /// The entry point for SetValue functions. Pass in the name (in SCORM terms) of the data model element
        /// and this method sets the appropriate value in the LearningDataModel class.
        /// </summary>
        /// <param name="inName">SCORM data model element name (e.g., "cmi.exit"). </param>
        /// <param name="inValue">The value of the element in SCORM terms (e.g., "logout").</param>
        /// <remarks>Note: InteractionIndexer and ObjectiveIndexer must be set prior to calling this method.
        /// <para>It is not valid to call SetValue in Review view.</para>
        /// <p/>This method is relatively lax about throwing exceptions for invalid names. It assumes the caller 
        /// is passing in valid information.
        /// </remarks>
        public override void SetValue(PlainTextString inName, PlainTextString inValue)
        {
            // It's not valid to call in Review mode
            if (this.View == SessionView.Review)
            {
                throw new InvalidOperationException(
                    ResHelper.GetMessage(Localization.GetMessage("CONV_InvalidViewOnSetValue")));
            }

            this.CurrentElementName = inName.ToString();
            string[] nameParts = this.CurrentElementName.Split('.');

            string value = inValue.ToString();

            if (nameParts[0] == "cmi")
            {
                if (nameParts.Length < 2)
                {
                    throw new InvalidOperationException(
                        ResHelper.GetMessage(
                            Localization.GetMessage("CONV_SetValueInvalidName"),
                            this.CurrentElementName));
                }

                switch (nameParts[1])
                {
                    case "core":
                        {
                            this.SetCoreValues(nameParts, value);
                            break;
                        }

                    case "suspend_data":
                        {
                            this.DataModel.SuspendData = value;
                            break;
                        }
                    case "comments":
                        {
                            this.SetCommentsFromLearner(value);
                            break;
                        }
                    case "objectives":
                        {
                            this.VerifyElementNameTokens(4, nameParts);
                            this.SetObjectives(this.CurrentElementName.Substring(15), value);
                            break;
                        }
                    case "student_preference":
                        {
                            this.VerifyElementNameTokens(3, nameParts);
                            this.SetLearnerPreferences(nameParts[2], value);
                            break;
                        }
                    case "interactions":
                        {
                            this.SetInteractions(this.CurrentElementName.Substring(17), value);
                            break;
                        }

                    default:
                        // All other values are read-only (or invalid names). This will throw exception.
                        this.SetReadOnlyValue();
                        break;
                }
            }
            else
            {
                this.DataModel.ExtensionData[this.CurrentElementName] = value;
            }
        }

        // Set cmi.core.* values
        private void SetCoreValues(string[] nameParts, string value)
        {
            this.VerifyElementNameTokens(3, nameParts);

            switch (nameParts[2])
            {
                case "lesson_location":
                    this.DataModel.Location = value;
                    break;
                case "lesson_status":
                    this.SetLessonStatus(value);
                    break;
                case "exit":
                    this.SetExit(value);
                    break;
                case "session_time":
                    this.DataModel.SessionTime = this.TimeSpanFromRte(value);
                    break;
                case "score":
                    {
                        this.VerifyElementNameTokens(3, nameParts);
                        string scorePart = nameParts[3];

                        // If this is setting the raw score, then we need to also increment/decrement the values in 
                        // DataModel.EvaluationPoints by the change in the raw score
                        float? oldRawPoints = this.DataModel.Score.Raw;

                        // This sets the values in DataModel.Score.*
                        this.SetScore(scorePart, value);

                        // If this is setting the raw score, then we need to also increment/decrement the values in 
                        // DataModel.EvaluationPoints by the change in the raw score
                        if (scorePart == "raw")
                        {
                            // This will not fail, since it's already been parsed by SetScore(...).
                            float newRawPoints = float.Parse(value, NumberFormatInfo.InvariantInfo);
                            if (oldRawPoints == null)
                            {
                                this.DataModel.EvaluationPoints = newRawPoints;
                            }
                            else
                            {
                                this.DataModel.EvaluationPoints += (newRawPoints - oldRawPoints);
                            }
                        }
                    }
                    break;
                default:
                    // Any other valid value is read only.  This will throw exception.
                    this.SetReadOnlyValue();
                    break;
            }
        }

        /// <summary>
        /// Sets the LessonStatus value in the data model
        /// </summary>
        private void SetLessonStatus(string value)
        {
            switch (value)
            {
                case "passed":
                    this.DataModel.LessonStatus = LessonStatus.Passed;
                    break;
                case "completed":
                    this.DataModel.LessonStatus = LessonStatus.Completed;
                    break;
                case "failed":
                    this.DataModel.LessonStatus = LessonStatus.Failed;
                    break;
                case "incomplete":
                    this.DataModel.LessonStatus = LessonStatus.Incomplete;
                    break;
                case "browsed":
                    this.DataModel.LessonStatus = LessonStatus.Browsed;
                    break;
                case "not attempted":
                    this.DataModel.LessonStatus = LessonStatus.NotAttempted;
                    break;
                default:
                    break;
            }
        }

        // Given an rte-formated timespan, return a TimeSpan object.
        protected override TimeSpan TimeSpanFromRte(string rteTimeSpan)
        {
            // Format is (HH)HH:MM:SS(.SS) where () represents optional elements

            MatchCollection matches = Regex.Matches(rteTimeSpan, @"^(\d{2,4}):(\d\d):(\d\d)(?:\.(\d{1,2}))?$");

            if (matches.Count != 1)
            {
                throw new InvalidOperationException(
                    ResHelper.GetMessage(
                        Localization.GetMessage("CONV_SetValueInvalidValue"),
                        rteTimeSpan,
                        this.CurrentElementName));
            }

            GroupCollection groups = matches[0].Groups;
            if (groups.Count != 5)
            {
                throw new InvalidOperationException(
                    ResHelper.GetMessage(
                        Localization.GetMessage("CONV_SetValueInvalidValue"),
                        rteTimeSpan,
                        this.CurrentElementName));
            }

            TimeSpan retVal;
            try
            {
                int hours = int.Parse(groups[1].Value, NumberFormatInfo.InvariantInfo);
                int mins = int.Parse(groups[2].Value, NumberFormatInfo.InvariantInfo);
                int secs = int.Parse(groups[3].Value, NumberFormatInfo.InvariantInfo);

                if (!string.IsNullOrEmpty(groups[4].Value))
                {
                    int ms = int.Parse(groups[4].Value, NumberFormatInfo.InvariantInfo);
                    retVal = new TimeSpan(0, hours, mins, secs, ms);
                }
                else
                {
                    retVal = new TimeSpan(hours, mins, secs);
                }

                return retVal;
            }
            catch (FormatException)
            {
                throw new InvalidOperationException(
                    ResHelper.GetMessage(
                        Localization.GetMessage("CONV_SetValueInvalidValue"),
                        rteTimeSpan,
                        this.CurrentElementName));
            }
        }

        // cmi.exit
        private void SetExit(string value)
        {
            ExitMode exitMode = ExitMode.Undetermined;
            switch (value)
            {
                case "time-out":
                    exitMode = ExitMode.TimeOut;
                    break;
                case "suspend":
                    exitMode = ExitMode.Suspended;
                    break;
                case "logout":
                    exitMode = ExitMode.Logout;
                    break;
                case "normal":
                    exitMode = ExitMode.Normal;
                    break;
                default:
                    break;
            }
            this.DataModel.NavigationRequest.ExitMode = exitMode;
        }

        private Dictionary<int, Objective> mObjectivesByIndex;

        /// <summary>
        /// Collection of interactions, including any pending objectives, keyed 
        /// by the 'n' in objectives.n.id.
        /// </summary>
        private IDictionary<int, Objective> ObjectivesByIndex
        {
            get
            {
                if (this.mObjectivesByIndex == null)
                {
                    this.mObjectivesByIndex = new Dictionary<int, Objective>(this.DataModel.Objectives.Count + 10);

                    int n = 0;
                    foreach (Objective objective in this.DataModel.Objectives)
                    {
                        this.mObjectivesByIndex.Add(n, objective);
                        n++;
                    }
                }

                return this.mObjectivesByIndex;
            }
        }

        /// <summary>
        /// SetValue("cmi.interactions.x.y.z")
        /// </summary>
        /// <param name="subElementName">The portion of the name following "cmi.interactions.". 
        /// In the example above, "x.y.z".</param>
        /// <param name="value">The value for the element</param>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")] // large switch statement
        private void SetInteractions(string subElementName, string value)
        {
            string[] elementParts = subElementName.Split('.');
            if (elementParts.Length < 2)
            {
                throw new InvalidOperationException(
                    ResHelper.GetMessage(
                        Localization.GetMessage("CONV_SetValueInvalidName"),
                        this.CurrentElementName));
            }

            int index;
            if (!int.TryParse(elementParts[0], out index))
            {
                return;
            }

            Interaction interaction;
            if (index < this.InteractionsByIndex.Count)
            {
                // It's already in the list, so find the object
                interaction = this.InteractionsByIndex[index];
            }
            else
            {
                // It's a new Interaction. Add it to the list of pending Interactions and 
                // add it to the mapping table.
                interaction = this.DataModel.CreateInteraction();
                this.PendingInteractions.Add(interaction);
                this.InteractionsByIndex.Add(index, interaction);
            }

            switch (elementParts[1])
            {
                case "id":
                    {
                        if (string.CompareOrdinal(value, interaction.Id) != 0)
                        {
                            interaction.Id = value;
                        }
                    }
                    break;
                case "type":
                    {
                        interaction.InteractionType = GetInteractionType(value);
                    }
                    break;
                case "objectives":
                    {
                        // First find 'x' in the element name interactions.n.objectives.x.id.
                        if (elementParts.Length < 4)
                        {
                            throw new InvalidOperationException(
                                ResHelper.GetMessage(
                                    Localization.GetMessage("CONV_SetValueInvalidName"),
                                    this.CurrentElementName));
                        }

                        int objIndex;
                        if (!int.TryParse(elementParts[2], out objIndex))
                        {
                            throw new InvalidOperationException(
                                ResHelper.GetMessage(
                                    Localization.GetMessage("CONV_SetValueInvalidName"),
                                    this.CurrentElementName));
                        }

                        InteractionObjective objective;
                        bool isNewObjective = false;
                        if (objIndex >= interaction.Objectives.Count)
                        {
                            objective = this.DataModel.CreateInteractionObjective();
                            isNewObjective = true;
                        }
                        else
                        {
                            objective = interaction.Objectives[objIndex];
                        }

                        if (string.CompareOrdinal(value, objective.Id) != 0)
                        {
                            objective.Id = value;
                        }

                        if (isNewObjective)
                        {
                            interaction.Objectives.Add(objective);
                        }
                    }
                    break;

                case "time":
                    {
                        interaction.Timestamp = value;
                    }
                    break;
                case "correct_responses":
                    {
                        // This is of the form: interactions.n.correct_responses.x.y

                        // First find 'x' in the element name interactions.n.correct_responses.x.y.
                        if (elementParts.Length < 4)
                        {
                            throw new InvalidOperationException(
                                ResHelper.GetMessage(
                                    Localization.GetMessage("CONV_SetValueInvalidName"),
                                    this.CurrentElementName));
                        }

                        int responseIndex;
                        if (!int.TryParse(elementParts[2], out responseIndex))
                        {
                            throw new InvalidOperationException(
                                ResHelper.GetMessage(
                                    Localization.GetMessage("CONV_SetValueInvalidName"),
                                    this.CurrentElementName));
                        }

                        CorrectResponse response;
                        bool isNewResponse = false;
                        if (responseIndex >= interaction.CorrectResponses.Count)
                        {
                            // It's a new one
                            response = this.DataModel.CreateCorrectResponse();
                            isNewResponse = true;
                        }
                        else
                        {
                            // Note the assumption that the index is actually the index into the array.
                            // There's no other way to match them up (since there's no identifier).
                            response = interaction.CorrectResponses[responseIndex];
                        }

                        if (elementParts[3] != "pattern")
                        {
                            throw new InvalidOperationException(
                                ResHelper.GetMessage(
                                    Localization.GetMessage("CONV_SetValueInvalidName"),
                                    this.CurrentElementName));
                        }

                        response.Pattern = value;

                        if (isNewResponse)
                        {
                            interaction.CorrectResponses.Add(response);
                        }
                    }
                    break;
                case "weighting":
                    {
                        float weighting;
                        if (!float.TryParse(value, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out weighting))
                        {
                            throw new InvalidOperationException(
                                ResHelper.GetMessage(
                                    Localization.GetMessage("CONV_SetValueInvalidValue"),
                                    value,
                                    this.CurrentElementName));
                        }
                        interaction.Weighting = weighting;
                    }
                    break;
                case "student_response":
                    {
                        // This must be set after InteractionType.

                        switch (interaction.InteractionType)
                        {
                            case InteractionType.TrueFalse:
                                {
                                    // Unfortunately, cannot use XmlConvert because it doesn't accept 't' and 'f'.
                                    if ((value == "t") || (value == "true") || (value == "1"))
                                    {
                                        interaction.LearnerResponse = true;
                                    }
                                    else if (((value == "f") || (value == "false") || (value == "0")))
                                    {
                                        interaction.LearnerResponse = false;
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException(
                                            ResHelper.GetMessage(
                                                Localization.GetMessage(
                                                    "CONV_SetValueInvalidValue"),
                                                value,
                                                this.CurrentElementName));
                                    }
                                }
                                break;
                            case InteractionType.Numeric:
                                {
                                    interaction.LearnerResponse = (float)XmlConvert.ToDouble(value);
                                }
                                break;
                            default:
                                interaction.LearnerResponse = value;
                                break;
                        }
                    }
                    break;
                case "result":
                    {
                        switch (value)
                        {
                            case "correct":
                                interaction.Result.State = InteractionResultState.Correct;
                                break;
                            case "wrong":
                                interaction.Result.State = InteractionResultState.Incorrect;
                                break;
                            case "neutral":
                                interaction.Result.State = InteractionResultState.Neutral;
                                break;
                            case "unanticipated":
                                interaction.Result.State = InteractionResultState.Unanticipated;
                                break;
                            default:
                                {
                                    // Should be a number. If it's not, then throw a basic error that says the 
                                    // value is not valid.
                                    float resultNumeric;
                                    try
                                    {
                                        resultNumeric = (float)XmlConvert.ToDouble(value);
                                    }
                                    catch (FormatException)
                                    {
                                        throw new InvalidOperationException(
                                            ResHelper.GetMessage(
                                                Localization.GetMessage(
                                                    "CONV_SetValueInvalidValue"),
                                                value,
                                                this.CurrentElementName));
                                    }
                                    interaction.Result.NumericResult = resultNumeric;
                                    interaction.Result.State = InteractionResultState.Numeric;
                                }
                                break;
                        }
                    }
                    break;
                case "latency":
                    {
                        interaction.Latency = this.TimeSpanFromRte(value);
                    }
                    break;
                default:
                    throw new InvalidOperationException(
                        ResHelper.GetMessage(
                            Localization.GetMessage("CONV_SetValueInvalidName"),
                            this.CurrentElementName));
            }
        }

        // Set the score field (eg, "raw") on a Score object.
        protected override void SetScoreSubField(string scoreField, string value, Score score)
        {
            float scoreValue;
            if (!float.TryParse(value, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out scoreValue))
            {
                throw new InvalidOperationException(
                    ResHelper.GetMessage(
                        Localization.GetMessage("CONV_SetValueInvalidValue"),
                        value,
                        this.CurrentElementName));
            }

            switch (scoreField)
            {
                case "raw":
                    {
                        score.Raw = scoreValue;
                    }
                    break;
                case "min":
                    {
                        score.Minimum = scoreValue;
                    }
                    break;
                case "max":
                    {
                        score.Maximum = scoreValue;
                    }
                    break;
                default:
                    throw new InvalidOperationException(
                        ResHelper.GetMessage(
                            Localization.GetMessage("CONV_SetValueInvalidName"),
                            this.CurrentElementName));
            }
        }

        private void SetLearnerPreferences(string subElementName, string value)
        {
            switch (subElementName)
            {
                case "audio":
                    {
                        int level;
                        if (int.TryParse(value, out level))
                        {
                            this.DataModel.Learner.AudioLevel = (float)level;
                            return;
                        }
                        // couldn't parse it...
                        throw new InvalidOperationException(
                            ResHelper.GetMessage(
                                Localization.GetMessage("CONV_SetValueInvalidValue"),
                                value,
                                this.CurrentElementName));
                    }
                case "language":
                    {
                        this.DataModel.Learner.Language = value;
                        return;
                    }
                case "speed":
                    {
                        int deliverySpeed;
                        if (int.TryParse(value, out deliverySpeed))
                        {
                            this.DataModel.Learner.DeliverySpeed = (float)deliverySpeed;
                            return;
                        }
                        // couldn't parse it
                        throw new InvalidOperationException(
                            ResHelper.GetMessage(
                                Localization.GetMessage("CONV_SetValueInvalidValue"),
                                value,
                                this.CurrentElementName));
                    }
                case "text":
                    {
                        try
                        {
                            AudioCaptioning captioning;
                            captioning = (AudioCaptioning)Enum.Parse(typeof(AudioCaptioning), value);
                            this.DataModel.Learner.AudioCaptioning = captioning;
                            return;
                        }
                        catch
                        {
                            throw new InvalidOperationException(
                                ResHelper.GetMessage(
                                    Localization.GetMessage("CONV_SetValueInvalidValue"),
                                    value,
                                    this.CurrentElementName));
                        }
                    }
            }

            // If we got here, the element name wasn't valid
            throw new InvalidOperationException(
                ResHelper.GetMessage(
                    Localization.GetMessage("CONV_SetValueInvalidName"), this.CurrentElementName));
        }

        // In 1.2, there is just one string for comments
        private void SetCommentsFromLearner(string value)
        {
            // If there are no comments, then add a new one. Otherwise, update the existing one.
            if (this.DataModel.CommentsFromLearner.Count == 0)
            {
                Comment comment = this.DataModel.CreateComment();
                comment.CommentText = value;
                this.DataModel.CommentsFromLearner.Add(comment);
            }
            else
            {
                this.DataModel.CommentsFromLearner[0].CommentText = value;
            }
        }

        // Set cmi.objectives.n.<x> values. SubElementName is n.<x>
        private void SetObjectives(string subElementName, string value)
        {
            string[] elementParts = subElementName.Split('.');

            if (elementParts.Length < 2)
            {
                throw new InvalidOperationException(
                    ResHelper.GetMessage(
                        Localization.GetMessage("CONV_SetValueInvalidName"),
                        this.CurrentElementName));
            }

            int index;
            if (!int.TryParse(elementParts[0], out index))
            {
                throw new InvalidOperationException(
                    ResHelper.GetMessage(
                        Localization.GetMessage("CONV_SetValueInvalidName"),
                        this.CurrentElementName));
            }

            Objective objective;

            if (index < this.ObjectivesByIndex.Count)
            {
                // It's already in the list, so find the object
                objective = this.ObjectivesByIndex[index];
            }
            else
            {
                // It's a new objective. Add it to the list of pending objectives.
                objective = this.DataModel.CreateObjective();
                this.PendingObjectives.Add(objective);
                this.ObjectivesByIndex.Add(index, objective);
            }

            switch (elementParts[1])
            {
                case "id":
                    objective.Id = value;
                    break;
                case "score":
                    this.SetScoreSubField(elementParts[2], value, objective.Score);
                    break;
                case "status":
                    objective.Status = this.GetLessonStatus(value);
                    break;
                default:
                    throw new InvalidOperationException(
                        ResHelper.GetMessage(
                            Localization.GetMessage("CONV_SetValueInvalidName"),
                            this.CurrentElementName));
            }
        }

        // Translate the value (eg, "passed") into a LessonStatus value
        private LessonStatus GetLessonStatus(string value)
        {
            switch (value)
            {
                case "passed":
                    return LessonStatus.Passed;
                case "completed":
                    return LessonStatus.Completed;
                case "failed":
                    return LessonStatus.Failed;
                case "incomplete":
                    return LessonStatus.Incomplete;
                case "browsed":
                    return LessonStatus.Browsed;
                case "not attempted":
                    return LessonStatus.NotAttempted;
                default:
                    throw new InvalidOperationException(
                        ResHelper.GetMessage(
                            Localization.GetMessage("CONV_SetValueInvalidValue"),
                            value,
                            this.CurrentElementName));
            }
        }

        #region GetValue helper functions

        /// <summary>
        /// Helper function to get the cmi.interactions.n.result value
        /// </summary>
        public override string GetRteResult(InteractionResult result)
        {
            switch (result.State)
            {
                case InteractionResultState.Correct:
                    return "correct";
                case InteractionResultState.Incorrect:
                    return "wrong";
                case InteractionResultState.Neutral:
                    return "neutral";
                case InteractionResultState.Unanticipated:
                    return "unanticipated";
                case InteractionResultState.Numeric:
                    {
                        if (result.NumericResult != null)
                        {
                            return RteFloatValue((float)result.NumericResult);
                        }
                    }
                    break;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// Helper function to convert .NET TimeSpan to RTE string value;
        /// </summary>
        public static string GetRteTimeSpan(TimeSpan? ts)
        {
            if (ts == null)
            {
                return string.Empty;
            }

            TimeSpan ts2 = ts.Value;
            return GetRteTimeSpan(ts2.Hours, ts2.Minutes, ts2.Seconds, ts2.Milliseconds);
        }

        public override string GetRteTimeSpan(TimeSpan ts)
        {
            return GetRteTimeSpan(ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }

        private static string GetRteTimeSpan(int hours, int min, int sec, int ms)
        {
            return ResHelper.GetMessage(
                "{0}:{1}:{2}.{3}",
                RteIntValue(hours, "D4"),
                RteIntValue(min, "D2"),
                RteIntValue(sec, "D2"),
                RteIntValue(ms, "D2"));
        }

        /// <summary>
        /// Helper function to get the comment text for the RTE.
        /// </summary>
        public static string GetRteComment(IList<Comment> comments)
        {
            // If there is a comment, return it. We only send one comment in 1.2.
            if ((comments.Count > 0) && (comments[0].CommentText != null))
            {
                return comments[0].CommentText;
            }

            return string.Empty;
        }

        /// <summary>
        /// Helper function to get the commentfromlms text for the RTE
        /// </summary>
        /// <param name="comments"></param>
        /// <returns></returns>
        public static string GetRteCommentFromLms(IList<CommentFromLms> comments)
        {
            // If there is a comment, return it. We only send one comment in 1.2.
            if ((comments.Count > 0) && (comments[0].CommentText != null))
            {
                return comments[0].CommentText;
            }

            return string.Empty;
        }

        /// <summary>
        /// Helper function to convert LessonStatus to the RTE string value
        /// </summary>
        public static string GetRteLessonStatus(LessonStatus status)
        {
            switch (status)
            {
                case LessonStatus.Browsed:
                    return "browsed";
                case LessonStatus.Completed:
                    return "completed";
                case LessonStatus.Failed:
                    return "failed";
                case LessonStatus.Incomplete:
                    return "incomplete";
                case LessonStatus.Passed:
                    return "passed";
                default: // case LessonStatus.NotAttempted:
                    return "not attempted";
            }
        }

        /// <summary>
        /// Helper function to convert float? to RTE form. Empty string is returned if 
        /// <paramref name="floatVal"/> is null.
        /// </summary>
        public static string GetRteFloat(float? value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return RteFloatValue(value.Value);
        }

        #endregion  // GetValue helper functions

        #region GetValues as RTE strings

        /// <summary>
        /// Return the encoded string of all current data model values to pass to the client. This method
        /// reinitializes the ObjectiveIndexer and InteractionIndexer values. 
        /// </summary>
        public override DataModelValues GetDataModelValues(AddDataModelValue addDataModelValue)
        {
            StringBuilder dataModelValuesBuffer = new StringBuilder(4096); // data model values
            string n;

            LearningDataModel dm = this.DataModel;

            Learner learner = dm.Learner;
            addDataModelValue(dataModelValuesBuffer, "cmi.core.student_id", learner.Id);
            addDataModelValue(dataModelValuesBuffer, "cmi.core.student_name", learner.Name);

            // cmi.core values
            addDataModelValue(dataModelValuesBuffer, "cmi.core.lesson_location", dm.Location);
            addDataModelValue(dataModelValuesBuffer, "cmi.core.credit", GetRteCredit(dm.Credit, this.View));
            addDataModelValue(dataModelValuesBuffer, "cmi.core.lesson_status", GetRteLessonStatus(dm.LessonStatus));
            addDataModelValue(dataModelValuesBuffer, "cmi.core.entry", GetRteEntry(dm.Entry));

            AddDataModelScore(addDataModelValue, dataModelValuesBuffer, "cmi.core.score", dm.Score);

            addDataModelValue(dataModelValuesBuffer, "cmi.core.total_time", GetRteTimeSpan(dm.TotalTime));
            addDataModelValue(dataModelValuesBuffer, "cmi.core.lesson_mode", GetRteMode(this.View));

            addDataModelValue(dataModelValuesBuffer, "cmi.suspend_data", dm.SuspendData);
            addDataModelValue(dataModelValuesBuffer, "cmi.launch_data", dm.LaunchData);
            addDataModelValue(dataModelValuesBuffer, "cmi.comments", GetRteComment(dm.CommentsFromLearner));
            addDataModelValue(dataModelValuesBuffer, "cmi.comments_from_lms", GetRteCommentFromLms(dm.CommentsFromLms));

            int objCountOrig = dm.Objectives.Count; // num objectives in data model
            int objCountToClient = 0; // num objectives sent to client
            for (int i = 0; i < objCountOrig; i++)
            {
                Objective objective = dm.Objectives[i];

                n = XmlConvert.ToString(objCountToClient);
                addDataModelValue(
                    dataModelValuesBuffer, ResHelper.FormatInvariant("cmi.objectives.{0}.id", n), objective.Id);

                AddDataModelScore(
                    addDataModelValue,
                    dataModelValuesBuffer,
                    ResHelper.FormatInvariant("cmi.objectives.{0}.score", n),
                    objective.Score);

                if (objective.Status != null)
                {
                    addDataModelValue(
                        dataModelValuesBuffer,
                        ResHelper.FormatInvariant("cmi.objectives.{0}.status", n),
                        GetRteLessonStatus(objective.Status.Value));
                }

                objCountToClient++;
            }
            addDataModelValue(dataModelValuesBuffer, "cmi.objectives._count", RteIntValue(objCountToClient));

            // cmi.student_data
            addDataModelValue(dataModelValuesBuffer, "cmi.student_data.mastery_score", GetRteFloat(dm.MasteryScore));
            addDataModelValue(
                dataModelValuesBuffer, "cmi.student_data.max_time_allowed", GetRteTimeSpan(dm.MaxTimeAllowed));
            addDataModelValue(
                dataModelValuesBuffer, "cmi.student_data.time_limit_action", GetTimeLimitAction(dm.TimeLimitAction));

            // cmi.student_preference
            addDataModelValue(dataModelValuesBuffer, "cmi.student_preference.audio", GetRteFloat(learner.AudioLevel));
            addDataModelValue(dataModelValuesBuffer, "cmi.student_preference.language", learner.Language);
            addDataModelValue(dataModelValuesBuffer, "cmi.student_preference.speed", GetRteFloat(learner.DeliverySpeed));
            addDataModelValue(
                dataModelValuesBuffer, "cmi.student_preference.text", GetRteAudioCaptioning(learner.AudioCaptioning));

            // Interactions
            int numInterations = dm.Interactions.Count;
            addDataModelValue(
                dataModelValuesBuffer,
                "cmi.interactions._count",
                numInterations.ToString(NumberFormatInfo.InvariantInfo));

            int count = 0;
            foreach (Interaction interaction in dm.Interactions)
            {
                n = XmlConvert.ToString(count);

                // Not intuitive: interaction.n.id, time, weighting, student_response and type are write only in Scorm 1.2, so don't add them here.

                int numObjectives = interaction.Objectives.Count;
                for (int j = 0; j < numObjectives; j++)
                {
                    InteractionObjective obj = interaction.Objectives[j];
                    addDataModelValue(
                        dataModelValuesBuffer,
                        ResHelper.FormatInvariant("cmi.interactions.{0}.objectives.{1}.id", n, RteIntValue(j)),
                        obj.Id);
                }
                addDataModelValue(
                    dataModelValuesBuffer,
                    ResHelper.FormatInvariant("cmi.interactions.{0}.objectives._count", n),
                    RteIntValue(numObjectives));

                int numResponses = interaction.CorrectResponses.Count;
                addDataModelValue(
                    dataModelValuesBuffer,
                    ResHelper.FormatInvariant("cmi.interactions.{0}.correct_responses._count", n),
                    RteIntValue(numResponses));
                for (int resI = 0; resI < numResponses; resI++)
                {
                    CorrectResponse response = interaction.CorrectResponses[resI];
                    if (response.Pattern != null)
                    {

                        addDataModelValue(
                            dataModelValuesBuffer,
                            ResHelper.FormatInvariant("cmi.interactions.{0}.correct_responses.{1}.pattern", n, RteIntValue(resI)),
                            response.Pattern);
                    }
                }

                count++;
            } // end interactions

            return new DataModelValues(new PlainTextString(dataModelValuesBuffer.ToString()));
        }

        /// <summary>
        /// Adds the subfields of a score to the datamodel values to send to the client.
        /// </summary>
        private static void AddDataModelScore(
            AddDataModelValue addDataModelValue, StringBuilder dataModelValuesBuffer, string cmiName, Score score)
        {
            string rteValue;
            float? tmpFloat;

            tmpFloat = score.Raw;
            rteValue = (tmpFloat == null) ? string.Empty : RteDataModelConverter.RteFloatValue((float)tmpFloat);
            addDataModelValue(dataModelValuesBuffer, ResHelper.FormatInvariant("{0}.raw", cmiName), rteValue);

            tmpFloat = score.Minimum;
            rteValue = (tmpFloat == null) ? string.Empty : RteDataModelConverter.RteFloatValue((float)tmpFloat);
            addDataModelValue(dataModelValuesBuffer, ResHelper.FormatInvariant("{0}.min", cmiName), rteValue);

            tmpFloat = score.Maximum;
            rteValue = (tmpFloat == null) ? string.Empty : RteDataModelConverter.RteFloatValue((float)tmpFloat);
            addDataModelValue(dataModelValuesBuffer, ResHelper.FormatInvariant("{0}.max", cmiName), rteValue);
        }

        #endregion // GetValues as RTE strings
    }
}