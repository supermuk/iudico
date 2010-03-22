using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FireFly.CourseEditor.Course.Manifest
{
    [Description("Base class represents base functionality of sequencing patterns.")]
    public class SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public static string ID
        {
            get
            {
                return "basePattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public static string Title
        {
            get
            {
                return "Base Sequencing Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public static string Description
        {
            get
            {
                return "Basic sequencing pattern. No elements affected with it.";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public static void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply pattern to current node!");
            }
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public static bool CanApplyPattern(object currentNode)
        {
            //Node couldnot be null.
            if (currentNode == null)
            {
                throw new ArgumentNullException("Node, to apply sequencing pattern to could not be null!");
            }

            //Node should be of ItemType or OrganizationType
            if (((currentNode is ItemType) || (currentNode is OrganizationType)) == false)
            {
                return false;
            }
            IItemContainer node;
            if (currentNode is ItemType)
            {
                node = currentNode as ItemType;
            }
            else
            {
                node = currentNode as OrganizationType;
            }
            //Node should have subItems
            if (node.IsLeaf == true)
            {
                return false;
            }

            //If node is ItemType then should be chapter.
            if (currentNode is ItemType)
            {
                if ((currentNode as ItemType).PageType != PageType.Chapter && (currentNode as ItemType).PageType != PageType.ControlChapter)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if all subItems PageType is Chapter or Control Chapter.
        /// </summary>
        /// <param name="items">Object to analize.</param>
        /// <returns>Boolean value 'true' if all subItems are Chapters or Control Chapters. Otherwise 'false'.</returns>
        public static bool AreAllChapters([NotNull]IItemContainer items)
        {
            bool result = true;
            foreach (ItemType item in items.SubItems)
            {
                if (item.PageType != PageType.Chapter && item.PageType != PageType.ControlChapter)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if all subItems PageType ia Summary, Question or Theory.
        /// </summary>
        /// <param name="items">Object to analize.</param>
        /// <returns>Boolean value 'true' if all subItems are Summary, Question or Theory. Otherwise 'false'.</returns>
        public static bool AreAllPages([NotNull]IItemContainer items)
        {
            bool result = true;
            foreach (ItemType item in items.SubItems)
            {
                if (item.PageType != PageType.Question && item.PageType != PageType.Summary && item.PageType != PageType.Theory)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if sequencing Collection contains sequencing with same ID. If not - adds sequencing to sequencing Collection.
        /// </summary>
        /// <param name="seq"><see cref="SequencingType"/> value to add to manifest's sequencingCollection.</param>
        /// <returns>Boolean value 'true' if sequencing was added. 'False' - if sequencing already existed.</returns>
        public static bool AddSeqCollectionSeq([NotNull]SequencingType seq)
        { 
            if (Course.Manifest.sequencingCollection == null)
            {
                Course.Manifest.sequencingCollection = new SequencingCollectionType();
            }

            if (Course.Manifest.sequencingCollection.sequencingCollection.Exists(curr => curr.ID == seq.ID))
            {
                return false;
            }
            else
            {
                Course.Manifest.sequencingCollection.sequencingCollection.Add(seq);
                return true;
            }
        }

        /// <summary>
        /// Checks is objectives contain objective with same ID and adds objective if not.
        /// </summary>
        /// <param name="item"><see cref="ItemType"/> value to add objective to.</param>
        /// <param name="obj"><see cref="ObjectivesTypeObjective"/> value represents objective to add.</param>
        /// <returns>Boolean value 'true' if objective was added. 'False' - if objective with same ID already existed.</returns>
        public static bool AddObjective([NotNull]ItemType item, [NotNull]ObjectivesTypeObjective obj)
        {
            if (item.Sequencing.objectives == null)
            {
                item.Sequencing.objectives = new ObjectivesType();
            }
            if (item.Sequencing.objectives.objective.Exists(curr => curr.objectiveID == obj.objectiveID) == true)
            {
                return false;
            }
            else
            {
                item.Sequencing.objectives.objective.Add(obj);
                return true;
            }
        }

        #endregion
    }

    [Description("Implements Forced Sequential Order sequencing pattern helper.")]
    public class ForcedSequentialOrderSequencingPattern: SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public static new string ID
        {
            get
            {
                return "fsoPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public static new string Title
        {
            get
            {
                return "Forced Sequential Order Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public static new string Description
        {
            get
            {
                return "Sequencing strategy that requires the learner to visit all SCOs in order. Once a SCO has been visited, the learner can jump backwards to review material, but the learner cannot jump ahead until the prerequisites are met.";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public static new void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply forced sequential order pattern to current node!");
            }            

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;
           
            string commonSeqRulesID = "common_seq_rules_"+ID;
            string prevScoObjective = "previous_sco_satisfied";

            Course.Organization.objectivesGlobalToSystem = false;

            #region sequencingCollection

            SequencingType seqForColl = new SequencingType();
            seqForColl.ID = commonSeqRulesID;
            seqForColl.rollupRules = new RollupRulesType();
            seqForColl.rollupRules.objectiveMeasureWeight = 0;
            seqForColl.deliveryControls = new DeliveryControlsType();
            seqForColl.deliveryControls.completionSetByContent = true;
            seqForColl.deliveryControls.objectiveSetByContent = true;
            
            AddSeqCollectionSeq(seqForColl);

            #endregion

            #region Sequencing

            seqNode.Sequencing = new SequencingType();
            
            if (seqNode.Sequencing.controlMode == null)
            {
                seqNode.Sequencing.controlMode = new ControlModeType();
            }
            seqNode.Sequencing.controlMode.choice = true;
            seqNode.Sequencing.controlMode.flow = true;

            #endregion

            #region Each SubItem

            for (int i = 0; i < containerNode.SubItems.Count; ++i)
            {
                ItemType item = containerNode.SubItems[i];
                ISequencing seqItem = item as ISequencing;
                item.Sequencing = SequencingManager.CreateNewSequencing(item);

                if (item.PageType != PageType.ControlChapter && item.PageType != PageType.Chapter)
                {
                    item.Sequencing.IDRef = commonSeqRulesID;
                }
                if (item.PageType != PageType.ControlChapter && item.PageType != PageType.Question)
                {                    
                    if (item.Sequencing.deliveryControls == null)
                    {
                        item.Sequencing.deliveryControls = new DeliveryControlsType();
                    }
                    item.Sequencing.deliveryControls.objectiveSetByContent = false;
                    item.Sequencing.deliveryControls.completionSetByContent = false;
                }

                var seqRef = item.Sequencing;
                ObjectiveTypeMapInfo mapInfo = SequencingManager.CustomizePrimaryObjectives(ref seqRef, item);
                item.Sequencing = seqRef;

                mapInfo.readSatisfiedStatus = true;
                mapInfo.writeSatisfiedStatus = true;
            }

            for (int i = 1; i < containerNode.SubItems.Count; ++i)
            {
                ItemType item = containerNode.SubItems[i];
               

                #region PreCondition Sequencing Rule

                item.Sequencing.sequencingRules = new SequencingRulesType();                

                PreConditionRuleType preRule = new PreConditionRuleType();
                preRule.ruleAction = new PreConditionRuleTypeRuleAction();
                preRule.ruleAction.action = PreConditionRuleActionType.disabled;
                preRule.ruleConditions = new SequencingRuleTypeRuleConditions();
                preRule.ruleConditions.conditionCombination = ConditionCombinationType.any;
                
                var cond1 = new SequencingRuleTypeRuleConditionsRuleCondition();
                cond1.referencedObjective = prevScoObjective;
                cond1.@operator = ConditionOperatorType.not;
                cond1.condition = SequencingRuleConditionType.satisfied;

                var cond2 = new SequencingRuleTypeRuleConditionsRuleCondition();
                cond2.referencedObjective = prevScoObjective;
                cond2.@operator = ConditionOperatorType.not;
                cond2.condition = SequencingRuleConditionType.objectiveStatusKnown;

                preRule.ruleConditions.ruleCondition.Add(cond1);
                preRule.ruleConditions.ruleCondition.Add(cond2);
                 
                item.Sequencing.sequencingRules.preConditionRule.Add(preRule);
               
                #endregion               

                #region Shared Objective

                ObjectivesTypeObjective sharedObjective = new ObjectivesTypeObjective(prevScoObjective);

                string prevPrimarySharedObjective = containerNode.SubItems[i - 1].Sequencing.objectives.primaryObjective.mapInfo[0].targetObjectiveID;
                var mapInfo = new ObjectiveTypeMapInfo();
                mapInfo.targetObjectiveID = prevPrimarySharedObjective;
                mapInfo.writeSatisfiedStatus = false;
                mapInfo.readSatisfiedStatus = true;
                sharedObjective.mapInfo.Add(mapInfo);

                AddObjective(item, sharedObjective);
               
                #endregion  
            }

            #endregion
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public static new bool CanApplyPattern(object currentNode)
        {
            if (SequencingPattern.CanApplyPattern(currentNode) == false)
            {
                return false;
            }

            bool resChildrenAllChapters = false;
            bool resChildrenAllPages = false;
            bool result = false;


            if (currentNode is ItemType || currentNode is OrganizationType)
            {
                IItemContainer node = currentNode as IItemContainer;

                //If we are about not chapter item.
                if (currentNode is ItemType)
                {
                    if (((ItemType)node).PageType != PageType.Chapter && ((ItemType)node).PageType != PageType.ControlChapter)
                    {
                        return false;
                    }
                }

                //Do All children are chapters?
                resChildrenAllChapters = AreAllChapters(node);

                //Do all children are pages with associated resources?
                resChildrenAllPages = AreAllPages(node);

                result = resChildrenAllChapters || resChildrenAllPages;
            }

            return result;
        }

        #endregion
    }

    [Description("Implements Forced forward-only sequencing pattern.")]
    public class ForcedForwardOnlySequencingPattern: SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public static new string ID
        {
            get
            {
                return "ffoPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public static new string Title
        {
            get
            {
                return "Forced Forward-Only Sequencing Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public static new string Description
        {
            get
            {
                return "Sequencing strategy that requires the learner to visit all Items in order. Once an Item has been visited, the learner can't jump backwards to review material but can skip current item.";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public static new void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply forced sequential order pattern to current node!");
            }

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            if (seqNode.Sequencing.controlMode == null)
            {
                seqNode.Sequencing.controlMode = new ControlModeType();
            }
            seqNode.Sequencing.controlMode.choice = false;
            seqNode.Sequencing.controlMode.forwardOnly = true;
            seqNode.Sequencing.controlMode.flow = true;

            for (int i = 0; i < containerNode.SubItems.Count; ++i)
            {
                ItemType item = containerNode.SubItems[i];
                item.Sequencing = SequencingManager.CreateNewSequencing(item);

                if (item.Sequencing.limitConditions == null)
                {
                    item.Sequencing.limitConditions = new LimitConditionsType();
                }
                item.Sequencing.limitConditions.attemptLimit = "1";
            }
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public static new bool CanApplyPattern(object currentNode)
        {
            if (SequencingPattern.CanApplyPattern(currentNode) == false)
            {
                return false;
            }

           // bool resChildrenAllChapters = false;
           // bool resChildrenAllPages = false;
            bool result = true;

            return result;
        }

        #endregion
    }
}