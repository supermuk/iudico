using System;
using System.Collections.Generic;
using System.Text;
using FireFly.CourseEditor.Course.Manifest;

namespace FireFly.CourseEditor.Course.Manifest
{
    /// <summary>
    /// Enumerates types of sequencing rules.
    /// </summary>
    public enum SequencingRuleTypes
    {
        PreConditionRule,
        PostConditionRule,
        ExitConditionRule
    }

    /// <summary>
    /// Implements simple sequencing strategy managing during designing Course.
    /// </summary>
    public static class SequencingManager
    {
        #region Constants

        public const string primaryObjectiveSufix = "_satisfied";
        public const string sharedObjectivePrefix = "global.";

        #endregion

        #region Create Sequencing

        /// <summary>
        /// Main entry point for creating Sequencing object.
        /// </summary>
        /// <param name="item"><see cref="ItemType"/> value to customize default sequencing srategy for.</param>
        /// <returns>SequencingType value with default sequencing strategy elements for current item.</returns>
        public static SequencingType CreateNewSequencing([NotNull]ItemType item)
        {
            PageType pageType = item.PageType;
            SequencingType result = new SequencingType();

            switch (pageType)
            { 
                case PageType.Chapter:
                    item.SequencingPatterns.Add(new ChapterDefaultSequencingPattern());
                    //CustomizeChapter(ref result);
                    break;
                case PageType.ControlChapter:
                    item.SequencingPatterns.Add(new ControlChapterDefaultSequencingPattern());  
                    // CustomizeControlChapter(ref result);
                    break;
                case PageType.Question:
                    CustomizeQuestionPage(ref result);
                    break;
                case PageType.Theory:
                    CustomizeTheoryPage(ref result);
                    break;
                default:
                    
                    break;
            }

            CustomizePrimaryObjectives(ref result, item.Identifier);

            return result;
        }

        /// <summary>
        /// Creates simple Sequencing for organization.
        /// </summary>
        /// <returns>SequencingType value with default sequencing strategy elements for organization.</returns>
        public static void CreateOrganizationDefaultSequencing([NotNull]OrganizationType organization)
        {
            organization.SequencingPatterns.Add(new OrganizationDefaultSequencingPattern());
        }
        
       /* /// <summary>
        /// Customizes sequencing for simple chapter.
        /// </summary>
        /// <param name="sequencing">SequencingType value represents object to customize.</param>
        public static void CustomizeChapter(ref SequencingType sequencing)
        {
            if (sequencing.controlMode == null)
            {
                sequencing.controlMode = new ControlModeType();
            }
            sequencing.controlMode.flow = true;
            sequencing.controlMode.choice = true;
        }

        /// <summary>
        /// Customizes sequencing for control chapter.
        /// </summary>
        /// <param name="sequencing">SequencingType value represents object to customize.</param>
        public static void CustomizeControlChapter(ref SequencingType sequencing)
        {
            if (sequencing.controlMode == null)
            { 
                sequencing.controlMode = new ControlModeType();
            }            
            sequencing.controlMode.flow = true;
            sequencing.controlMode.forwardOnly = true;
            sequencing.controlMode.choice = false;
            sequencing.controlMode.choiceExit = false;

            if (sequencing.limitConditions == null)
            {
                sequencing.limitConditions = new LimitConditionsType();
            }
            sequencing.limitConditions.attemptLimit = "1";
        }
        */
        /// <summary>
        /// Customizes sequencing for question page.
        /// </summary>
        /// <param name="sequencing">SequencingType value represents object to customize.</param>
        public static void CustomizeQuestionPage(ref SequencingType sequencing)
        {
            if (sequencing.deliveryControls == null)
            { 
                sequencing.deliveryControls = new DeliveryControlsType();
            }            
            sequencing.deliveryControls.completionSetByContent = true;
            sequencing.deliveryControls.objectiveSetByContent = true;
            sequencing.deliveryControls.tracked = true;
            
            if (sequencing.limitConditions == null)
            {
                sequencing.limitConditions = new LimitConditionsType();
                sequencing.limitConditions.attemptLimit = "1";
            }

            if (sequencing.rollupRules == null)
            {
                sequencing.rollupRules = new RollupRulesType();
            }
            sequencing.rollupRules.objectiveMeasureWeight = 1;
        }

        /// <summary>
        /// Customizes sequencing for theory page.
        /// </summary>
        /// <param name="sequencing">SequencingType value represents object to customize.</param>
        public static void CustomizeTheoryPage(ref SequencingType sequencing)
        {
            if (sequencing.deliveryControls == null)
            {
                sequencing.deliveryControls = new DeliveryControlsType();
            }
            sequencing.deliveryControls.completionSetByContent = false;
            sequencing.deliveryControls.objectiveSetByContent = false;
        }

        /// <summary>
        /// Creates PreConditionRule with simple parameters.
        /// </summary>
        /// <param name="condition">SequencingRuleConditionType enumerable value represents Rule Condition.</param>
        /// <param name="action">PreConditionRuleActionType enumerable value represents PreCondition Rule Action.</param>
        /// <returns>PreConditionRuleType value with appropriate parameters.</returns>
        public static PreConditionRuleType CreateSimplePreConditionRule([NotNull]SequencingRuleConditionType condition, [NotNull]PreConditionRuleActionType action)
        {  
            PreConditionRuleType preConditionRule = new PreConditionRuleType();
            preConditionRule.ruleConditions = new SequencingRuleTypeRuleConditions();
            preConditionRule.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(preConditionRule.ruleConditions);
            preConditionRule.ruleConditions.ruleCondition.Add(new SequencingRuleTypeRuleConditionsRuleCondition());
            preConditionRule.ruleConditions.ruleCondition[0].condition = condition;
            preConditionRule.ruleAction = new PreConditionRuleTypeRuleAction();
            preConditionRule.ruleAction.action = action;
            return preConditionRule;
        }

        /// <summary>
        /// Creates PostConditionRule with simple parameters.
        /// </summary>
        /// <param name="condition">SequencingRuleConditionType enumerable value represents Rule Condition.</param>
        /// <param name="action">PostConditionRuleActionType enumerable value represents PostCondition Rule Action.</param>
        /// <returns>PostConditionRuleType value with appropriate parameters.</returns>
        public static PostConditionRuleType CreateSimplePostConditionRule([NotNull]SequencingRuleConditionType condition, [NotNull]PostConditionRuleActionType action)
        {
            PostConditionRuleType postConditionRule = new PostConditionRuleType();
            postConditionRule.ruleConditions = new SequencingRuleTypeRuleConditions();
            postConditionRule.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(postConditionRule.ruleConditions);
            postConditionRule.ruleConditions.ruleCondition.Add(new SequencingRuleTypeRuleConditionsRuleCondition());
            postConditionRule.ruleConditions.ruleCondition[0].condition = condition;
            postConditionRule.ruleAction = new PostConditionRuleTypeRuleAction();
            postConditionRule.ruleAction.action = action;
            return postConditionRule;
        }

        /// <summary>
        /// Creates Pre Condition Rule
        /// </summary>
        /// <param name="conditions">Sequencing Rule Conditions.</param>
        /// <param name="ruleAction">Action.</param>
        /// <returns>PreConditionRuleType generated from arguments.</returns>
        public static PreConditionRuleType CreatePreConditionRule(ConditionCombinationType conditionCombination, IEnumerable<SequencingRuleTypeRuleConditionsRuleCondition> conditions, PreConditionRuleActionType ruleAction)
        {
            PreConditionRuleType result = new PreConditionRuleType();
            result.ruleConditions = new SequencingRuleTypeRuleConditions();
            result.ruleConditions.conditionCombination = conditionCombination;
            result.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(result.ruleConditions);
            result.ruleConditions.ruleCondition.AddRange(conditions);
            result.ruleAction = new PreConditionRuleTypeRuleAction();
            result.ruleAction.action = ruleAction;

            return result;
        }

        /// <summary>
        /// Creates Post Condition Rule
        /// </summary>
        /// <param name="conditions">Sequencing Rule Conditions.</param>
        /// <param name="ruleAction">Action.</param>
        /// <returns>PostConditionRuleType generated from arguments.</returns>
        public static PostConditionRuleType CreatePostConditionRule(ConditionCombinationType conditionCombination, IEnumerable<SequencingRuleTypeRuleConditionsRuleCondition> conditions, PostConditionRuleActionType ruleAction)
        {
            PostConditionRuleType result = new PostConditionRuleType();
            result.ruleConditions = new SequencingRuleTypeRuleConditions();
            result.ruleConditions.conditionCombination = conditionCombination;
            result.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(result.ruleConditions);
            result.ruleConditions.ruleCondition.AddRange(conditions);
            result.ruleAction = new PostConditionRuleTypeRuleAction();
            result.ruleAction.action = ruleAction;

            return result;
        }

        /// <summary>
        /// Creates Exit Condition Rule
        /// </summary>
        /// <param name="conditions">Sequencing Rule Conditions.</param>
        /// <param name="ruleAction">Action.</param>
        /// <returns>ExitConditionRuleType generated from arguments.</returns>
        public static ExitConditionRuleType CreateExitConditionRule(ConditionCombinationType conditionCombination, IEnumerable<SequencingRuleTypeRuleConditionsRuleCondition> conditions, ExitConditionRuleActionType ruleAction)
        {
            ExitConditionRuleType result = new ExitConditionRuleType();
            result.ruleConditions = new SequencingRuleTypeRuleConditions();
            result.ruleConditions.conditionCombination = conditionCombination;
            result.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(result.ruleConditions);
            result.ruleConditions.ruleCondition.AddRange(conditions);
            result.ruleAction = new ExitConditionRuleTypeRuleAction();
            result.ruleAction.action = ruleAction;

            return result;
        }

        /// <summary>
        /// Creates Rollup Rule
        /// </summary>
        /// <param name="childActivitySet">Child activity set.</param>
        /// <param name="conditions">Rollup conditions</param>
        /// <param name="action">Rollup action.</param>
        /// <returns>Rollup rule initialized with arguments.</returns>
        public static RollupRuleType CreateRollupRule(ChildActivityType childActivitySet, ConditionCombinationType conditionCombination, [NotNull]IEnumerable<RollupRuleTypeRollupConditionsRollupCondition> conditions, RollupActionType action)
        {
            RollupRuleType result = new RollupRuleType();
            result.childActivitySet = childActivitySet;
            result.rollupConditions = new RollupRuleTypeRollupConditions();
            result.rollupConditions.rollupCondition = new ManifestNodeList<RollupRuleTypeRollupConditionsRollupCondition>(result.rollupConditions);
            result.rollupConditions.conditionCombination = conditionCombination;
            result.rollupConditions.rollupCondition.AddRange(conditions);
            result.rollupAction = new RollupRuleTypeRollupAction();
            result.rollupAction.action = action;

            return result;
        }

        /// <summary>
        /// Applies base elements to primary objective.
        /// </summary>
        /// <param name="sequencing">SequencingType value represents object to customize.</param>
        /// <param name="identifier">String value represents identifier.</param>
        /// <returns>Target Objective ID</returns>
        public static string CustomizePrimaryObjectives([NotNull]ref SequencingType sequencing, [NotNull]string identifier)
        { 
            //Adding primary objective and mapped global objective with default IDs.
            if (sequencing.objectives == null)
            {
                sequencing.objectives = new ObjectivesType();
            }
            if (sequencing.objectives.primaryObjective == null)
            {
                sequencing.objectives.primaryObjective = new ObjectivesTypePrimaryObjective();
            }            
            string primaryObjectiveID = identifier + primaryObjectiveSufix;

            if (sequencing.objectives.primaryObjective.objectiveID == null)
            {
                sequencing.objectives.primaryObjective.objectiveID = primaryObjectiveID;
            }
            string targetObjectiveID = sharedObjectivePrefix + identifier;
            
            /*ObjectiveTypeMapInfo mapInfo = new ObjectiveTypeMapInfo();
            mapInfo.targetObjectiveID = targetObjectiveID;
            mapInfo.readSatisfiedStatus = true;
            mapInfo.writeSatisfiedStatus = true;
            sequencing.objectives.primaryObjective.mapInfo.Add(mapInfo);
            */

            return targetObjectiveID;
        }

        /// <summary>
        /// Simply creates new instance of SequencingType object for item's sequencing property.
        /// </summary>
        /// <param name="item">Item to clear sequecing in.</param>
        public static SequencingType ClearSequencing([NotNull]ISequencing item)
        {
            item.Sequencing = new SequencingType();
            return item.Sequencing;
        }

        #endregion
    }
}
