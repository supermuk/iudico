using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace FireFly.CourseEditor.Course.Manifest
{
    [Description("Implements Sequencing Patterns Collection logic, based on list container.")]
    public class SequencingPatternList : List<ISequencingPattern>, ISequencingPatternCollection
    {
        #region Protected Fields

        protected object _parentNode;

        #endregion

        #region Public Properties

        [Description("Defines node, sequencingCollection is applied to.")]
        [NotNull]
        public object ParentNode
        {
            get
            {
                return this._parentNode;
            }
            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Parent cannot be null");
                }
                this._parentNode = value;

                if (ParentNodeChanged != null)
                {
                    ParentNodeChanged();
                }
            }
        }

        #endregion

        #region Constructors

        public SequencingPatternList()
        {
            Course.CourseChanged += new Action(Course_CourseChanged);
            Course.CourseSaving += new Action(Course_CourseChanged);
            this.ParentNodeChanged += new Action(Course_CourseChanged);
            this.PatternAdded += new Action<ISequencingPattern>(OnPatternAdded);
            this.PatternRemoved += new Action<ISequencingPattern>(OnPatternRemoved);
        }

        public SequencingPatternList([NotNull]object parentNode)
            : this()
        {
            this.ParentNode = parentNode;
        }

        #endregion

        #region Methods

        protected void Course_ManifestChanged(ManifestChangedEventArgs obj)
        {
            //Course_CourseChanged();
        }

        protected void Course_CourseChanged()
        {
            ApplyAll();
        }

        void OnPatternRemoved(ISequencingPattern pattern)
        {
            pattern.AbolishPattern(this.ParentNode);
            this.ApplyAll();
        }

        void OnPatternAdded(ISequencingPattern pattern)
        {
            this.Sort();
            this.ApplyAll();
        }

        public void Add([NotNull]Type patternType)
        {
            if (typeof(SequencingPattern).IsSubclassOf(patternType))
            {
                throw new ArgumentException("Wrong type specified for sequencing pattern adding!");
            }

            this.Add(patternType.GetConstructor(new Type[] { }).Invoke(new object[] { }) as ISequencingPattern);
        }

        public new void Add([NotNull]ISequencingPattern pattern)
        {
            if (this.Contains(pattern) == true || this.ContainsPattern(pattern.GetType()) == true)
            {
                return;
                //throw new Exception(pattern.Title + " is already applied to current node.");
            }

            ISequencingPattern sameLevelPattern = this.Find(curr => curr.Level == pattern.Level);
            if (sameLevelPattern != null)
            {
                this.Remove(sameLevelPattern);
            }

            base.Add(pattern);

            if (this.PatternAdded != null)
            {
                PatternAdded(pattern);
            }
        }

        public new void AddRange([NotNull]IEnumerable<ISequencingPattern> patternList)
        {
            foreach (ISequencingPattern pattern in patternList)
            {
                this.Add(pattern);
            }
        }

        public bool Remove([NotNull]Type patternType)
        {
            if (this.ContainsPattern(patternType) == false)
            {
                return false;
            }

            foreach (ISequencingPattern pattern in this)
            {
                if (pattern.GetType() == patternType)
                {
                    return this.Remove(pattern);
                }
            }
            return false;
        }

        public new bool Remove([NotNull]ISequencingPattern pattern)
        {
            bool result = base.Remove(pattern);
            if (result == true)
            {
                if (this.PatternRemoved != null)
                {
                    PatternRemoved(pattern);
                }
            }
            return result;
        }

        public new void RemoveAt(int index)
        {
            ISequencingPattern removePattern = this[index];
            base.RemoveAt(index);
            if (this.PatternRemoved != null)
            {
                PatternRemoved(removePattern);
            }
        }

        public new void RemoveRange(int index, int count)
        {
            // collect removed item for event            
            for (int i = index; i < index + count; i++)
            {
                ISequencingPattern remPattern = this[i];
                this.Remove(remPattern);
            }
        }

        public void ApplyAll()
        {
            if (CanApplyAll() == false)
            {
                RemoveInapplicable();
            }

            foreach (ISequencingPattern pattern in this)
            {
                pattern.ApplyPattern(this.ParentNode);
            }
        }

        public bool CanApplyAll()
        {
            foreach (ISequencingPattern pattern in this)
            {
                if (pattern.CanApplyPattern(this.ParentNode) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public int RemoveInapplicable()
        {
            int count = 0;
            for (int i = 0; i < this.Count; ++i)
            {
                ISequencingPattern pattern = this[i];
                if (pattern.CanApplyPattern(this.ParentNode) == false)
                {
                    this.Remove(pattern);
                    count++;
                }
            }

            return count;
        }

        public bool ContainsPattern([NotNull]Type patternType)
        {
            foreach (ISequencingPattern item in this)
            {
                if (item.GetType() == patternType)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<Type> GetAllKnownPatterns()
        {
            List<Type> result = new List<Type>();
            result.Add(typeof(OrganizationDefaultSequencingPattern));
            result.Add(typeof(ChapterDefaultSequencingPattern));
            result.Add(typeof(ControlChapterDefaultSequencingPattern));
            result.Add(typeof(ForcedForwardOnlySequencingPattern));
            result.Add(typeof(ForcedSequentialOrderSequencingPattern));
            result.Add(typeof(PostTestSequencingPattern));
            result.Add(typeof(RandomSetSequencingPattern));
            result.Add(typeof(PrePostTestSequencingPattern));
            result.Add(typeof(RandomPostTestSequencingPattern));
            //-> Place for new one

            return result;
        }

        #endregion

        #region Events, actions

        public event Action ParentNodeChanged;

        public event Action<ISequencingPattern> PatternAdded;

        public event Action<ISequencingPattern> PatternRemoved;

        #endregion
    }

    [Description("Base class represents base functionality of sequencing patterns.")]
    public class SequencingPattern : ISequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public virtual string ID
        {
            get
            {
                return "basePattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public virtual string Title
        {
            get
            {
                return "Base Sequencing Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public virtual string Description
        {
            get
            {
                return "Basic sequencing pattern. No elements affected with it.";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public virtual int Level
        {
            get
            {
                return 0;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public virtual void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply pattern to current node!");
            }
        }

        /// <summary>
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public virtual void AbolishPattern(object currentNode)
        {
            /* if (CanApplyPattern(currentNode) == false)
             {
                 throw new InvalidOperationException("Can't abolish pattern from current node!");
             }*/
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public virtual bool CanApplyPattern(object currentNode)
        {
            //Node couldnot be null.
            if (currentNode == null)
            {
                return false;
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
            throw new NotImplementedException();
            /*
            if (Course.Manifest == null)
            {
                return false;
            }

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
             */
        }

        /// <summary>
        /// Checks if sequencing Collection contains sequencing with same ID and removes it from collection.
        /// </summary>
        /// <param name="sequencingID">String value represents sequecning ID to remove.</param>
        /// <returns>Boolean value 'true' if sequencing was removed or was not in collection. Otherwise 'false'.</returns>
        public static bool RemoveSeqCollectionSeq([NotNull]string sequencingID)
        {
            throw new NotImplementedException();
            /*
            if (Course.Manifest.sequencingCollection == null)
            {
                return true;
            }
            bool exists = Course.Manifest.sequencingCollection.sequencingCollection.Exists(curr => curr.ID.CompareTo(sequencingID) == 0);
            if (exists == true)
            {
                int result = Course.Manifest.sequencingCollection.sequencingCollection.RemoveAll(curr => curr.ID == sequencingID);
                return (result > 0);
            }
            else
            {
                return true;
            }
            */
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

        #region IComparable

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Object obj");
            }

            if (this == obj)
            {
                return 0;
            }
            if (obj is ISequencingPattern)
            {
                ISequencingPattern objSeq = obj as ISequencingPattern;
                return this.Level.CompareTo(objSeq.Level);
            }
            throw new InvalidOperationException("Could not compare objects of different type!");
        }

        #endregion
    }

    [Description("Implements Organization Default Sequecning pattern.")]
    public class OrganizationDefaultSequencingPattern : SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "orgDefPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Organization Default Sequencing";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return "Any sub activity may be experienced at any moment of time any number of times. If Chapter is attempted– selection flows to the first child activity.";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 1;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                return;
            }

            var seqNode = currentNode as ISequencing;


            if (seqNode.Sequencing == null)
            {
                seqNode.Sequencing = new SequencingType();
            }

            if (seqNode.Sequencing.controlMode == null)
            {
                seqNode.Sequencing.controlMode = new ControlModeType();
            }
            seqNode.Sequencing.controlMode.choice = true;
            seqNode.Sequencing.controlMode.flow = true;

            SequencingType seq = seqNode.Sequencing;
            if (currentNode is ItemType)
            {
                var itemNode = currentNode as ItemType;
                SequencingManager.CustomizePrimaryObjectives(ref seq, itemNode.Identifier);
            }
            else
            {
                var orgNode = currentNode as OrganizationType;
                SequencingManager.CustomizePrimaryObjectives(ref seq, orgNode.identifier);
            }
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public override bool CanApplyPattern(object currentNode)
        {
            //Node couldnot be null.
            if (currentNode == null)
            {
                return false;
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
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;

            if (seqNode.Sequencing == null)
            {
                seqNode.Sequencing = new SequencingType();
            }

            if (seqNode.Sequencing.controlMode != null)
            {
                seqNode.Sequencing.RemoveChild(seqNode.Sequencing.controlMode);
            }
        }

        #endregion
    }

    [Description("Implements Organization Default Sequecning pattern.")]
    public class ChapterDefaultSequencingPattern : SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "chDefPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Chapter Default Sequencing";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return " Any sub activity may be experienced at any moment of time any number of times. If Chapter is attempted– selection flows to the first child activity. ";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 1;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply forced sequential order pattern to current node!");
            }

            var seqNode = currentNode as ISequencing;

            if (seqNode.Sequencing == null)
            {
                seqNode.Sequencing = new SequencingType();
            }

            if (seqNode.Sequencing.controlMode == null)
            {
                seqNode.Sequencing.controlMode = new ControlModeType();
            }
            seqNode.Sequencing.controlMode.choice = true;
            seqNode.Sequencing.controlMode.flow = true;

            SequencingType seq = seqNode.Sequencing;
            if (currentNode is ItemType)
            {
                var itemNode = currentNode as ItemType;
                SequencingManager.CustomizePrimaryObjectives(ref seq, itemNode.Identifier);
            }
            else
            {
                var orgNode = currentNode as OrganizationType;
                SequencingManager.CustomizePrimaryObjectives(ref seq, orgNode.identifier);
            }
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public override bool CanApplyPattern(object currentNode)
        {
            //Node couldnot be null.
            if (currentNode == null)
            {
                return false;

                //throw new ArgumentNullException("Node, to apply sequencing pattern to could not be null!");
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
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;

            if (seqNode.Sequencing == null)
            {
                seqNode.Sequencing = new SequencingType();
            }

            if (seqNode.Sequencing.controlMode != null)
            {
                seqNode.Sequencing.RemoveChild(seqNode.Sequencing.controlMode);
            }
        }

        #endregion
    }

    [Description("Implements Organization Default Sequecning pattern.")]
    public class ControlChapterDefaultSequencingPattern : SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "cochDefPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Control Chapter Default Sequencing";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return "Student can attempt this chapter only once. Sub activities (children) may be passed in straight forward-only order only. Student can’t see other activities except current control chapter and it's sub activities. If Control Chapter is attempted– selection flows to the first child activity.";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 1;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                return;
            }

            var seqNode = currentNode as ISequencing;

            if (seqNode.Sequencing == null)
            {
                seqNode.Sequencing = new SequencingType();
            }

            if (seqNode.Sequencing.controlMode == null)
            {
                seqNode.Sequencing.controlMode = new ControlModeType();
            }
            seqNode.Sequencing.controlMode.choice = false;
            seqNode.Sequencing.controlMode.flow = true;
            seqNode.Sequencing.controlMode.forwardOnly = true;
            seqNode.Sequencing.controlMode.choiceExit = false;

            if (seqNode.Sequencing.limitConditions == null)
            {
                seqNode.Sequencing.limitConditions = new LimitConditionsType();
            }
            if (seqNode.Sequencing.limitConditions.attemptLimit == null)
            {
                seqNode.Sequencing.limitConditions.attemptLimit = "1";
            }

            SequencingType seq = seqNode.Sequencing;
            if (currentNode is ItemType)
            {
                var itemNode = currentNode as ItemType;
                SequencingManager.CustomizePrimaryObjectives(ref seq, itemNode.Identifier);
            }
            else
            {
                var orgNode = currentNode as OrganizationType;
                SequencingManager.CustomizePrimaryObjectives(ref seq, orgNode.identifier);
            }
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public override bool CanApplyPattern(object currentNode)
        {
            //Node couldnot be null.
            if (currentNode == null)
            {
                return false;
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
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;

            if (seqNode.Sequencing == null)
            {
                seqNode.Sequencing = new SequencingType();
            }

            if (seqNode.Sequencing.controlMode != null)
            {
                seqNode.Sequencing.RemoveChild(seqNode.Sequencing.controlMode);
            }

            if (seqNode.Sequencing.limitConditions != null)
            {
                seqNode.Sequencing.RemoveChild(seqNode.Sequencing.limitConditions);
            }
        }

        #endregion
    }

    [Description("Implements Forced Sequential Order sequencing pattern.")]
    public class ForcedSequentialOrderSequencingPattern : SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "fsoPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Forced Sequential Order Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return "Sequencing strategy that requires the learner to visit all SCOs in order. Once a SCO has been visited, the learner can jump backwards to review material, but the learner cannot jump ahead until the prerequisites are met.";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 2;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply forced sequential order pattern to current node!");
            }

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            string commonSeqRulesID = "common_seq_rule_" + ID;
            string prevScoObjective = "previous_sco_satisfied";

            if (Course.Organization != null)
            {
                Course.Organization.objectivesGlobalToSystem = false;
            }

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
                if (item.Sequencing == null)
                {
                    item.Sequencing = SequencingManager.CreateNewSequencing(item);
                }

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
                string targetObjectiveID = SequencingManager.CustomizePrimaryObjectives(ref seqRef, item.Identifier);
                item.Sequencing = seqRef;

                ObjectiveTypeMapInfo mapInfo = new ObjectiveTypeMapInfo();
                mapInfo.targetObjectiveID = targetObjectiveID;
                mapInfo.readSatisfiedStatus = true;
                mapInfo.writeSatisfiedStatus = true;
                item.Sequencing.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
                item.Sequencing.objectives.primaryObjective.mapInfo.Add(mapInfo);
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
        public override bool CanApplyPattern(object currentNode)
        {
            if (base.CanApplyPattern(currentNode) == false)
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

        /// <summary>
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            string commonSeqRulesID = "common_seq_rule_" + ID;

            Course.Organization.objectivesGlobalToSystem = true;

            RemoveSeqCollectionSeq(commonSeqRulesID);

            if (seqNode.Sequencing != null)
            {
                seqNode.Sequencing = null;
            }

            for (int i = 0; i < containerNode.SubItems.Count; ++i)
            {
                ItemType item = containerNode.SubItems[i];
                ISequencing seqItem = item as ISequencing;

                seqItem.Sequencing = SequencingManager.CreateNewSequencing(item);
            }

        }

        #endregion
    }

    [Description("Implements Forced forward-only sequencing pattern.")]
    public class ForcedForwardOnlySequencingPattern : SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "ffoPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Forced Forward-Only Sequencing Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return "Sequencing strategy that requires the learner to visit all Items in order. Once an Item has been visited, the learner can't jump backwards to review material but can skip current item.";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 2;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply forced forward-only sequencing pattern to current node!");
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
                if (item.Sequencing == null)
                {
                    item.Sequencing = SequencingManager.CreateNewSequencing(item);
                }

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
        public override bool CanApplyPattern(object currentNode)
        {
            if (base.CanApplyPattern(currentNode) == false)
            {
                return false;
            }

            // bool resChildrenAllChapters = false;
            // bool resChildrenAllPages = false;
            bool result = true;

            return result;
        }

        /// <summary>
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            if (seqNode.Sequencing.controlMode != null)
            {
                seqNode.Sequencing.RemoveChild(seqNode.Sequencing.controlMode);
            }

            for (int i = 0; i < containerNode.SubItems.Count; ++i)
            {
                ItemType item = containerNode.SubItems[i];
                if (item.Sequencing != null)
                {
                    item.Sequencing = SequencingManager.CreateNewSequencing(item);
                }
            }
        }

        #endregion
    }

    [Description("Implements Post-Test sequencing pattern.")]
    public class PostTestSequencingPattern : ForcedSequentialOrderSequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "postTestPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Post-Test Sequencing Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return "Based on the Forced Sequential Order pattern. Score, completion and satisfaction evaluation of affected activity is based on the result of last Post Test.";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 2;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply Post-Test pattern to current node!");
            }

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            string commonSeqRulesID = "common_seq_rule_" + this.ID;
            string prevScoObjective = "previous_sco_satisfied";

            if (Course.Organization != null)
            {
                Course.Organization.objectivesGlobalToSystem = false;
            }

            #region sequencingCollection

            SequencingType seqForColl = new SequencingType();
            seqForColl.ID = commonSeqRulesID;
            seqForColl.rollupRules = new RollupRulesType();
            seqForColl.rollupRules.objectiveMeasureWeight = 0;
            seqForColl.rollupRules.rollupObjectiveSatisfied = false;
            seqForColl.rollupRules.rollupProgressCompletion = false;

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

            //All sub items, besides last one.
            for (int i = 0; i < containerNode.SubItems.Count - 1; ++i)
            {
                ItemType item = containerNode.SubItems[i];
                ISequencing seqItem = item as ISequencing;
                if (item.Sequencing == null)
                {
                    item.Sequencing = SequencingManager.CreateNewSequencing(item);
                }

                item.Sequencing.IDRef = commonSeqRulesID;

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
                string targetObjectiveID = SequencingManager.CustomizePrimaryObjectives(ref seqRef, item.Identifier);
                item.Sequencing = seqRef;

                ObjectiveTypeMapInfo mapInfo = new ObjectiveTypeMapInfo();
                mapInfo.targetObjectiveID = targetObjectiveID;
                mapInfo.readSatisfiedStatus = true;
                mapInfo.writeSatisfiedStatus = true;
                item.Sequencing.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
                item.Sequencing.objectives.primaryObjective.mapInfo.Add(mapInfo);

                if (seqItem.Sequencing.rollupRules != null)
                {
                    seqItem.Sequencing.rollupRules = null;
                }
            }

            //Conditions on previous_sco_satisfied
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

            #region Post Test sequencing

            var lastItem = containerNode.SubItems.LastOrDefault();

            var seqRef1 = lastItem.Sequencing;
            SequencingManager.CustomizePrimaryObjectives(ref seqRef1, lastItem.Identifier);
            lastItem.Sequencing = seqRef1;

            lastItem.Sequencing.objectives.primaryObjective.mapInfo = null;

            if (lastItem.Sequencing.rollupRules == null)
            {
                lastItem.Sequencing.rollupRules = new RollupRulesType();
            }
            lastItem.Sequencing.rollupRules.rollupObjectiveSatisfied = true;
            lastItem.Sequencing.rollupRules.rollupProgressCompletion = true;
            lastItem.Sequencing.rollupRules.objectiveMeasureWeight = 1;

            #endregion
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public override bool CanApplyPattern(object currentNode)
        {
            if (base.CanApplyPattern(currentNode) == false)
            {
                return false;
            }

            var itemContainer = currentNode as IItemContainer;
            var lastSub = itemContainer.SubItems.LastOrDefault();

            bool result = lastSub.PageType == PageType.ControlChapter || lastSub.PageType == PageType.Question;

            return result;
        }

        /// <summary>
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            string commonSeqRulesID = "common_seq_rule_" + ID;

            RemoveSeqCollectionSeq(commonSeqRulesID);

            for (int i = 0; i < containerNode.SubItems.Count; ++i)
            {
                ItemType item = containerNode.SubItems[i];
            }

        }

        #endregion
    }

    [Description("Implements Random Set of Tests sequencing pattern.")]
    public class RandomSetSequencingPattern : SequencingPattern
    {
        #region Constants

        protected const string numericTitle = "Random Set of Tests";
        protected const string numericDescription = "Please enter number of tests to select:";

        #endregion

        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "randSetPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Random Set of Tests Sequencing Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return "Control Chapter Default sequencing is applied to the affected activity. Student progresses through the M activities, chosen randomly from a set of Question activities. ";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 2;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets numeric value, invoking numeric dialog for user input.
        /// </summary>
        /// <param name="minimum">Integer value represent lower bound.</param>
        /// <param name="maximum">Integer value represent upper bound.</param>
        /// <param name="value">Referenced integer to set value to.</param>
        /// <returns>Boolean value 'true' if user submited value, 'false' - if canceled.</returns>
        protected static bool GetNumericValue(int minimum, int maximum, ref int value)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Gets selectCount value from defined node's sequencing elements.
        /// </summary>
        /// <param name="currentNode">ISequencing node to get value from.</param>
        /// <param name="value">Referenced integer value represents selectCount value.</param>
        /// <returns>Boolean value 'true' if selectCount was successfully got, otherwise 'false'.</returns>
        public static bool GetSelectCount(object currentNode, ref int value)
        {
            if (currentNode is ISequencing == false)
            {
                throw new InvalidOperationException("Can't get selectCount value from node without Sequencing support!");
            }

            ISequencing seqNode = currentNode as ISequencing;

            if (seqNode.Sequencing == null || seqNode.Sequencing.randomizationControls == null || seqNode.Sequencing.randomizationControls.selectCount == null)
            {
                return false;
            }

            int selectCount = 0;
            if (Int32.TryParse(seqNode.Sequencing.randomizationControls.selectCount, out selectCount) == false)
            {
                return false;
            }

            value = selectCount;
            return true;
        }

        /// <summary>
        /// Sets selectCount value to defined node by getting user input.
        /// </summary>
        /// <param name="currentNode">Node to set value to.</param>
        /// <param name="value">Referenced integer to set value from.</param>
        /// <returns>Boolean value 'true' if user submited value, 'false' - if canceled.</returns>
        public static bool SetSelectCount(object currentNode, ref int value)
        {
            if (currentNode is ISequencing == false)
            {
                throw new InvalidOperationException("Can't set selectCount value for node without Sequencing support!");
            }

            if (currentNode is IItemContainer == false)
            {
                throw new InvalidOperationException("Can't set selectCount value for node which is not container for other nodes!");
            }

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            if (seqNode.Sequencing.randomizationControls == null)
            {
                seqNode.Sequencing.randomizationControls = new RandomizationType();
            }

            int selectCount = value;
            //GetSelectCount(currentNode, ref selectCount);

            int numOfChildren = containerNode.SubItems.Count;

            if (selectCount > numOfChildren)
            {
                selectCount = numOfChildren;
            }
            if (selectCount < 0)
            {
                selectCount = 0;
            }
            bool result = GetNumericValue(0, numOfChildren, ref selectCount);
            if (result == true)
            {
                value = selectCount;
                seqNode.Sequencing.randomizationControls.selectCount = selectCount.ToString();
            }

            return result;
        }

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply Random Set of Tests sequencing pattern to current node!");
            }

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            int numOfChildren = containerNode.SubItems.Count;

            if (seqNode.Sequencing.randomizationControls == null)
            {
                seqNode.Sequencing.randomizationControls = new RandomizationType();
            }
            seqNode.Sequencing.randomizationControls.reorderChildren = true;
            seqNode.Sequencing.randomizationControls.selectionTiming = RandomTimingType.once;
            int selectCount = numOfChildren;
            bool canGetSelectCount = GetSelectCount(currentNode, ref selectCount);
            if (canGetSelectCount == false || selectCount > numOfChildren)
            {
                SetSelectCount(currentNode, ref selectCount);
            }

            double normalizationCoef = (double)numOfChildren / (double)selectCount;
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public override bool CanApplyPattern(object currentNode)
        {
            if (base.CanApplyPattern(currentNode) == false)
            {
                return false;
            }

            if (currentNode is IItemContainer == false)
            {
                return false;
            }
            IItemContainer containerNode = currentNode as IItemContainer;

            if (currentNode is ItemType == false)
            {
                return false;
            }
            ItemType itemNode = currentNode as ItemType;

            bool resCurrControlChapter = itemNode.PageType == PageType.ControlChapter;
            bool resChildrenAllPages = SequencingPattern.AreAllPages(containerNode);
            bool resChildrenAllQuestions = containerNode.SubItems.All(curr => curr.PageType == PageType.Question);

            bool result = resChildrenAllPages && resChildrenAllQuestions && resCurrControlChapter;

            return result;
        }

        /// <summary>
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;

            seqNode.Sequencing.randomizationControls = null;
        }

        #endregion
    }

    [Description("Implements Pre-Test or Post-Test sequencing pattern.")]
    public class PrePostTestSequencingPattern : SequencingPattern
    {
        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "prePostPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Pre-Test or Post-Test Sequencing Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return "Student is free to experience any sub-activity. If student passed pre-test, post-test is disabled. Pre-test may be attempted only once. If pre-test failed, post-test is enabled only if all other sub activities are completed/satisfied. Score, completion and satisfaction evaluation of affected activity is based on the result of Pre-test or Post-Test.";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 2;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply Pre-Test or Post-Test sequencing pattern to current node!");
            }

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            if (Course.Organization != null)
            {
                Course.Organization.objectivesGlobalToSystem = false;
            }

            string assessmentObjectiveID = "assessment_satisfied";
            string assessmentMappedObjectiveID = "global." + assessmentObjectiveID + "_" + Guid.NewGuid().ToString();

            string contentObjectiveID = "content_satisfied";
            string contentMappedObjeciveID = "global." + contentObjectiveID + "_" + Guid.NewGuid().ToString();

            //Sequencing preferences for currentNode.
            #region currentNode Sequencing

            seqNode.Sequencing = new SequencingType();

            if (seqNode.Sequencing.controlMode == null)
            {
                seqNode.Sequencing.controlMode = new ControlModeType();
            }
            seqNode.Sequencing.controlMode.choice = true;
            seqNode.Sequencing.controlMode.flow = true;

            RollupRuleTypeRollupConditionsRollupCondition cond1 = new RollupRuleTypeRollupConditionsRollupCondition();
            cond1.condition = RollupRuleConditionType.satisfied;

            RollupRuleType rule0 = new RollupRuleType();

            rule0.rollupConditions = new RollupRuleTypeRollupConditions();
            rule0.rollupConditions.rollupCondition.Add(cond1);
            rule0.childActivitySet = ChildActivityType.any;
            rule0.rollupAction = new RollupRuleTypeRollupAction();
            rule0.rollupAction.action = RollupActionType.completed;

            seqNode.Sequencing.rollupRules = new RollupRulesType();
            seqNode.Sequencing.rollupRules.rollupRule.Add(rule0);

            #endregion

            ItemType dummyGroupingChapter;
            //If sub item count is 1, item is a chapter and is invisible - dummy grouping item has been already created.
            if (containerNode.SubItems.Count == 1 && containerNode.SubItems[0].isvisible == false && containerNode.SubItems[0].PageType == PageType.Chapter)
            {
                dummyGroupingChapter = containerNode.SubItems[0];
            }
            //otherwise create this dummy chapter.
            else
            {
                dummyGroupingChapter = ItemType.CreateNewItem("Dummy Item", Guid.NewGuid().ToString(), null, PageType.Chapter);
                dummyGroupingChapter.isvisible = false;

                containerNode.InsertGroupingItem(dummyGroupingChapter);
            }

            //Customize dummy grouping chapter sequencing
            #region Dummy Grouping Chapter sequencing

            dummyGroupingChapter.isvisible = false;
            dummyGroupingChapter.Sequencing = new SequencingType();

            #region Control Mode

            dummyGroupingChapter.Sequencing.controlMode = new ControlModeType();
            dummyGroupingChapter.Sequencing.controlMode.choice = true;
            dummyGroupingChapter.Sequencing.controlMode.flow = true;

            #endregion

            #region Rollup Rules

            RollupRuleTypeRollupConditionsRollupCondition condDGC1 = new RollupRuleTypeRollupConditionsRollupCondition();
            condDGC1.condition = RollupRuleConditionType.completed;

            RollupRuleType ruleDGC1 = new RollupRuleType();
            ruleDGC1.childActivitySet = ChildActivityType.any;
            ruleDGC1.rollupConditions = new RollupRuleTypeRollupConditions();
            ruleDGC1.rollupConditions.rollupCondition = new ManifestNodeList<RollupRuleTypeRollupConditionsRollupCondition>(ruleDGC1.rollupConditions);
            ruleDGC1.rollupConditions.rollupCondition.Add(condDGC1);

            ruleDGC1.rollupAction = new RollupRuleTypeRollupAction();
            ruleDGC1.rollupAction.action = RollupActionType.incomplete;

            RollupRuleTypeRollupConditionsRollupCondition condDGC2 = new RollupRuleTypeRollupConditionsRollupCondition();
            condDGC2.condition = RollupRuleConditionType.completed;

            RollupRuleType ruleDGC2 = new RollupRuleType();
            ruleDGC2.childActivitySet = ChildActivityType.all;
            ruleDGC2.rollupConditions = new RollupRuleTypeRollupConditions();
            ruleDGC2.rollupConditions.rollupCondition = new ManifestNodeList<RollupRuleTypeRollupConditionsRollupCondition>(ruleDGC2.rollupConditions);
            ruleDGC2.rollupConditions.rollupCondition.Add(condDGC2);

            ruleDGC2.rollupAction = new RollupRuleTypeRollupAction();
            ruleDGC2.rollupAction.action = RollupActionType.completed;

            dummyGroupingChapter.Sequencing.rollupRules = new RollupRulesType();
            dummyGroupingChapter.Sequencing.rollupRules.rollupRule = new ManifestNodeList<RollupRuleType>(dummyGroupingChapter.Sequencing.rollupRules);
            dummyGroupingChapter.Sequencing.rollupRules.rollupRule.Add(ruleDGC1);
            dummyGroupingChapter.Sequencing.rollupRules.rollupRule.Add(ruleDGC2);

            #endregion

            #region Objectives

            dummyGroupingChapter.Sequencing.objectives = new ObjectivesType();
            dummyGroupingChapter.Sequencing.objectives.primaryObjective = new ObjectivesTypePrimaryObjective();
            dummyGroupingChapter.Sequencing.objectives.primaryObjective.objectiveID = assessmentObjectiveID;

            ObjectiveTypeMapInfo dummyObjectiveMapInfo = new ObjectiveTypeMapInfo();
            dummyObjectiveMapInfo.readNormalizedMeasure = true;
            dummyObjectiveMapInfo.readSatisfiedStatus = true;
            dummyObjectiveMapInfo.targetObjectiveID = assessmentMappedObjectiveID;

            dummyGroupingChapter.Sequencing.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
            dummyGroupingChapter.Sequencing.objectives.primaryObjective.mapInfo.Add(dummyObjectiveMapInfo);

            #endregion

            #endregion
            //From this point working with dummy grouping item as parent node for further subnodes.

            ItemType contentWrapper;
            //If there are more than 3 subitems in the dummyGroupingChapter, or only one leaf content is present -> group all subitems, besides first and last into content wrapper dummy invisible chapter.
            if (dummyGroupingChapter.SubItems.Count > 3 || dummyGroupingChapter.SubItems[1].IsLeaf == true)
            {
                contentWrapper = ItemType.CreateNewItem("Content Wrapper", Guid.NewGuid().ToString(), null, PageType.Chapter);
                ItemType preTestItem, postTestItem;
                //1. Add children to grouping item.
                for (int i = 1; i < dummyGroupingChapter.SubItems.Count - 1; ++i)
                {
                    contentWrapper.SubItems.Add(dummyGroupingChapter.SubItems[i]);
                }

                preTestItem = dummyGroupingChapter.SubItems.First();
                postTestItem = dummyGroupingChapter.SubItems.Last();

                //2. Clear list of children, but not use Removing tool!!!
                dummyGroupingChapter.SubItems = new ManifestNodeList<ItemType>(dummyGroupingChapter);

                //3. Add pre, grouping, post items to list of children.
                dummyGroupingChapter.SubItems.Add(preTestItem);
                dummyGroupingChapter.SubItems.Add(contentWrapper);
                dummyGroupingChapter.SubItems.Add(postTestItem);
            }
            else
            {
                contentWrapper = dummyGroupingChapter.SubItems[1];
            }

            //Now we have 3 subitems: question(/control chapter) - (control)chapter - question(/control chapter)
            //Customizing preTest:
            #region PreTest Sequencing

            ItemType preTest = dummyGroupingChapter.SubItems.First();

            preTest.Sequencing = new SequencingType();

            #region Sequencing rules

            SequencingRuleTypeRuleConditionsRuleCondition ruleConditionPreTest1 = new SequencingRuleTypeRuleConditionsRuleCondition();
            ruleConditionPreTest1.condition = SequencingRuleConditionType.attemptLimitExceeded;

            PreConditionRuleType preRulePreTest1 = new PreConditionRuleType();
            preRulePreTest1.ruleConditions = new SequencingRuleTypeRuleConditions();
            preRulePreTest1.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(preRulePreTest1.ruleConditions);
            preRulePreTest1.ruleConditions.ruleCondition.Add(ruleConditionPreTest1);
            preRulePreTest1.ruleAction = new PreConditionRuleTypeRuleAction();
            preRulePreTest1.ruleAction.action = PreConditionRuleActionType.disabled;

            SequencingRuleTypeRuleConditionsRuleCondition ruleConditionPreTest2 = new SequencingRuleTypeRuleConditionsRuleCondition();
            ruleConditionPreTest2.condition = SequencingRuleConditionType.satisfied;
            ruleConditionPreTest2.referencedObjective = assessmentObjectiveID;

            PreConditionRuleType preRulePreTest2 = new PreConditionRuleType();
            preRulePreTest2.ruleConditions = new SequencingRuleTypeRuleConditions();
            preRulePreTest2.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(preRulePreTest2.ruleConditions);
            preRulePreTest2.ruleConditions.ruleCondition.Add(ruleConditionPreTest2);
            preRulePreTest2.ruleAction = new PreConditionRuleTypeRuleAction();
            preRulePreTest2.ruleAction.action = PreConditionRuleActionType.disabled;

            preTest.Sequencing.sequencingRules = new SequencingRulesType();
            preTest.Sequencing.sequencingRules.preConditionRule = new ManifestNodeList<PreConditionRuleType>(preTest.Sequencing.sequencingRules);
            preTest.Sequencing.sequencingRules.preConditionRule.Add(preRulePreTest1);
            preTest.Sequencing.sequencingRules.preConditionRule.Add(preRulePreTest2);

            #endregion

            #region Limit conditions

            int preTestAttemptLimit = 1;
            if (preTest.SubItems.Count > 0)
            {
                preTestAttemptLimit = preTest.SubItems.Count;
            }

            preTest.Sequencing.limitConditions = new LimitConditionsType();
            preTest.Sequencing.limitConditions.attemptLimit = preTestAttemptLimit.ToString();

            #endregion

            #region Objectives

            preTest.Sequencing.objectives = new ObjectivesType();
            preTest.Sequencing.objectives.primaryObjective = new ObjectivesTypePrimaryObjective();
            preTest.Sequencing.objectives.primaryObjective.objectiveID = assessmentObjectiveID;

            ObjectiveTypeMapInfo preTestPrimaryMapInfo = new ObjectiveTypeMapInfo();
            preTestPrimaryMapInfo.targetObjectiveID = assessmentMappedObjectiveID;
            preTestPrimaryMapInfo.writeNormalizedMeasure = true;
            preTestPrimaryMapInfo.writeSatisfiedStatus = true;

            preTest.Sequencing.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
            preTest.Sequencing.objectives.primaryObjective.mapInfo.Add(preTestPrimaryMapInfo);

            #endregion

            #region Delivery Controls

            if (preTest.PageType == PageType.Question)
            {
                preTest.Sequencing.deliveryControls = new DeliveryControlsType();
                preTest.Sequencing.deliveryControls.completionSetByContent = true;
                preTest.Sequencing.deliveryControls.objectiveSetByContent = true;
            }

            #endregion

            #endregion

            //Customizing Content Wrappper
            contentWrapper.isvisible = false;
            #region Content Wrapper Sequencing

            contentWrapper.Sequencing = new SequencingType();

            #region Control Mode

            contentWrapper.Sequencing.controlMode = new ControlModeType();
            contentWrapper.Sequencing.controlMode.choice = true;
            contentWrapper.Sequencing.controlMode.flow = true;

            #endregion

            #region Rollup Rules
            ///
            RollupRuleTypeRollupConditionsRollupCondition rollupCondContWrap0 = new RollupRuleTypeRollupConditionsRollupCondition();
            rollupCondContWrap0.condition = RollupRuleConditionType.completed;

            RollupRuleType rollupRuleContWrapper0 = new RollupRuleType();
            rollupRuleContWrapper0.childActivitySet = ChildActivityType.any;
            rollupRuleContWrapper0.rollupConditions = new RollupRuleTypeRollupConditions();
            rollupRuleContWrapper0.rollupConditions.rollupCondition = new ManifestNodeList<RollupRuleTypeRollupConditionsRollupCondition>(rollupRuleContWrapper0.rollupConditions);
            rollupRuleContWrapper0.rollupConditions.rollupCondition.Add(rollupCondContWrap0);
            rollupRuleContWrapper0.rollupAction = new RollupRuleTypeRollupAction();
            rollupRuleContWrapper0.rollupAction.action = new RollupActionType();
            rollupRuleContWrapper0.rollupAction.action = RollupActionType.notSatisfied;
            ///
            RollupRuleTypeRollupConditionsRollupCondition rollupCondContWrap1 = new RollupRuleTypeRollupConditionsRollupCondition();
            rollupCondContWrap1.condition = RollupRuleConditionType.completed;

            RollupRuleType rollupRuleContWrapper1 = new RollupRuleType();
            rollupRuleContWrapper1.childActivitySet = ChildActivityType.all;
            rollupRuleContWrapper1.rollupConditions = new RollupRuleTypeRollupConditions();
            rollupRuleContWrapper1.rollupConditions.rollupCondition = new ManifestNodeList<RollupRuleTypeRollupConditionsRollupCondition>(rollupRuleContWrapper1.rollupConditions);
            rollupRuleContWrapper1.rollupConditions.rollupCondition.Add(rollupCondContWrap1);
            rollupRuleContWrapper1.rollupAction = new RollupRuleTypeRollupAction();
            rollupRuleContWrapper1.rollupAction.action = new RollupActionType();
            rollupRuleContWrapper1.rollupAction.action = RollupActionType.satisfied;

            contentWrapper.Sequencing.rollupRules = new RollupRulesType();
            contentWrapper.Sequencing.rollupRules.rollupRule = new ManifestNodeList<RollupRuleType>(contentWrapper.Sequencing.rollupRules);
            contentWrapper.Sequencing.rollupRules.rollupRule.Add(rollupRuleContWrapper0);
            contentWrapper.Sequencing.rollupRules.rollupRule.Add(rollupRuleContWrapper1);

            #endregion

            #region Objectives

            contentWrapper.Sequencing.objectives = new ObjectivesType();
            contentWrapper.Sequencing.objectives.primaryObjective = new ObjectivesTypePrimaryObjective();
            contentWrapper.Sequencing.objectives.primaryObjective.objectiveID = contentObjectiveID;

            ObjectiveTypeMapInfo contentPrimaryMapInfo = new ObjectiveTypeMapInfo();
            contentPrimaryMapInfo.targetObjectiveID = contentMappedObjeciveID;
            contentPrimaryMapInfo.writeSatisfiedStatus = true;

            contentWrapper.Sequencing.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
            contentWrapper.Sequencing.objectives.primaryObjective.mapInfo.Add(contentPrimaryMapInfo);

            #endregion

            #region Sub Items delivery controls

            for (int i = 0; i < contentWrapper.SubItems.Count; ++i)
            {
                var currContentNode = contentWrapper.SubItems[i];
                if (currContentNode.IsLeaf == false)
                {
                    continue;
                }
                if (currContentNode.Sequencing == null)
                {
                    currContentNode.Sequencing = new SequencingType();
                }
                if (currContentNode.Sequencing.deliveryControls == null)
                {
                    currContentNode.Sequencing.deliveryControls = new DeliveryControlsType();
                }
                currContentNode.Sequencing.deliveryControls.objectiveSetByContent = true;
                currContentNode.Sequencing.deliveryControls.completionSetByContent = true;
            }

            #endregion

            #endregion

            //Customizing Post Test
            #region Post Test Sequencing

            string contentCompletedObjectiveID = "content_completed";

            ItemType postTest = dummyGroupingChapter.SubItems.Last();
            postTest.Sequencing = new SequencingType();

            #region Sequencing Rules

            //Once a test (pre or post) has been satisfied, disable it so it can't be taken again.

            SequencingRuleTypeRuleConditionsRuleCondition ruleConditionPostTest1 = new SequencingRuleTypeRuleConditionsRuleCondition();
            ruleConditionPostTest1.condition = SequencingRuleConditionType.satisfied;
            ruleConditionPostTest1.referencedObjective = assessmentObjectiveID;

            PreConditionRuleType preRulePostTest1 = new PreConditionRuleType();
            preRulePostTest1.ruleConditions = new SequencingRuleTypeRuleConditions();
            preRulePostTest1.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(preRulePostTest1.ruleConditions);
            preRulePostTest1.ruleConditions.ruleCondition.Add(ruleConditionPostTest1);
            preRulePostTest1.ruleAction = new PreConditionRuleTypeRuleAction();
            preRulePostTest1.ruleAction.action = PreConditionRuleActionType.disabled;

            //If the content is not completed, then don't allow access to the post test 

            SequencingRuleTypeRuleConditionsRuleCondition ruleConditionPostTest2 = new SequencingRuleTypeRuleConditionsRuleCondition();
            ruleConditionPostTest2.condition = SequencingRuleConditionType.satisfied;
            ruleConditionPostTest2.referencedObjective = contentCompletedObjectiveID;
            ruleConditionPostTest2.@operator = ConditionOperatorType.not;

            SequencingRuleTypeRuleConditionsRuleCondition ruleConditionPostTest3 = new SequencingRuleTypeRuleConditionsRuleCondition();
            ruleConditionPostTest3.condition = SequencingRuleConditionType.objectiveStatusKnown;
            ruleConditionPostTest3.referencedObjective = contentCompletedObjectiveID;
            ruleConditionPostTest3.@operator = ConditionOperatorType.not;

            PreConditionRuleType preRulePostTest2 = new PreConditionRuleType();
            preRulePostTest2.ruleConditions = new SequencingRuleTypeRuleConditions();
            preRulePostTest2.ruleConditions.conditionCombination = ConditionCombinationType.any;
            preRulePostTest2.ruleConditions.ruleCondition = new ManifestNodeList<SequencingRuleTypeRuleConditionsRuleCondition>(preRulePostTest2.ruleConditions);
            preRulePostTest2.ruleConditions.ruleCondition.Add(ruleConditionPostTest2);
            preRulePostTest2.ruleConditions.ruleCondition.Add(ruleConditionPostTest3);
            preRulePostTest2.ruleAction = new PreConditionRuleTypeRuleAction();
            preRulePostTest2.ruleAction.action = PreConditionRuleActionType.disabled;

            //Adding rules
            postTest.Sequencing.sequencingRules = new SequencingRulesType();
            postTest.Sequencing.sequencingRules.preConditionRule = new ManifestNodeList<PreConditionRuleType>(postTest.Sequencing.sequencingRules);
            postTest.Sequencing.sequencingRules.preConditionRule.Add(preRulePostTest1);
            postTest.Sequencing.sequencingRules.preConditionRule.Add(preRulePostTest2);

            #endregion

            #region Objectives

            postTest.Sequencing.objectives = new ObjectivesType();
            postTest.Sequencing.objectives.primaryObjective = new ObjectivesTypePrimaryObjective();
            postTest.Sequencing.objectives.primaryObjective.objectiveID = assessmentObjectiveID;

            ObjectiveTypeMapInfo postTestPrimaryMapInfo = new ObjectiveTypeMapInfo();
            postTestPrimaryMapInfo.targetObjectiveID = assessmentMappedObjectiveID;
            postTestPrimaryMapInfo.writeNormalizedMeasure = true;
            postTestPrimaryMapInfo.writeSatisfiedStatus = true;

            postTest.Sequencing.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
            postTest.Sequencing.objectives.primaryObjective.mapInfo.Add(postTestPrimaryMapInfo);

            ObjectivesTypeObjective postTestContentCompletedObjecive = new ObjectivesTypeObjective();
            postTestContentCompletedObjecive.objectiveID = contentCompletedObjectiveID;

            ObjectiveTypeMapInfo postTestContentCompletedMapInfo = new ObjectiveTypeMapInfo();
            postTestContentCompletedMapInfo.targetObjectiveID = contentMappedObjeciveID;
            postTestContentCompletedMapInfo.readSatisfiedStatus = true;
            postTestContentCompletedObjecive.mapInfo = new List<ObjectiveTypeMapInfo>();
            postTestContentCompletedObjecive.mapInfo.Add(postTestContentCompletedMapInfo);

            postTest.Sequencing.objectives.objective = new ManifestNodeList<ObjectivesTypeObjective>(postTest.Sequencing.objectives);
            postTest.Sequencing.objectives.objective.Add(postTestContentCompletedObjecive);

            #endregion

            #region Delivery Controls

            if (postTest.PageType == PageType.Question)
            {
                postTest.Sequencing.deliveryControls = new DeliveryControlsType();
                postTest.Sequencing.deliveryControls.completionSetByContent = true;
                postTest.Sequencing.deliveryControls.objectiveSetByContent = true;
            }

            #endregion

            #endregion
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public override bool CanApplyPattern(object currentNode)
        {
            if (base.CanApplyPattern(currentNode) == false)
            {
                return false;
            }

            var itemContainer = currentNode as IItemContainer;

            bool result;
            //In case dummy item was already created.
            if (itemContainer.SubItems.Count == 1)
            {
                var dummyItem = itemContainer.SubItems[0];

                //Not invisible dummy item...
                if (dummyItem.isvisible == true)
                {
                    result = false;
                }
                else
                {
                    result = CheckPrePostPresence(dummyItem);
                }
            }
            else
            {
                result = CheckPrePostPresence(currentNode);
            }
            return result;
        }

        /// <summary>
        /// Checks if node contains pre and post tests and some theory part to complete. 
        /// </summary>
        /// <param name="currentNode">Node to examine.</param>
        /// <returns>Boolean 'true' if node contains pre and post tests and some theory part to complete, otherwise 'false'.</returns>
        protected bool CheckPrePostPresence(object currentNode)
        {
            bool result;

            var itemContainer = currentNode as IItemContainer;

            bool checkCount = itemContainer.SubItems.Count >= 3;

            if (checkCount == false)
            {
                return false;
            }

            var firstSub = itemContainer.SubItems.First();
            var lastSub = itemContainer.SubItems.Last();

            bool firstCheck = firstSub.PageType == PageType.ControlChapter || firstSub.PageType == PageType.Question;
            bool lastCheck = lastSub.PageType == PageType.ControlChapter || lastSub.PageType == PageType.Question;

            result = checkCount && firstCheck && lastCheck;

            return result;
        }

        /// <summary>
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            seqNode.Sequencing.controlMode = null;
            seqNode.Sequencing.rollupRules = null;

            //Removing dummy item
            if (containerNode.SubItems.Count == 1 && containerNode.SubItems[0].isvisible == false && containerNode.SubItems[0].PageType == PageType.Chapter)
            {
                var dummyGroupingChapter = containerNode.SubItems[0];
                dummyGroupingChapter.Parent = containerNode;
                dummyGroupingChapter.RemoveAndMerge();
            }

            ItemType preTest = containerNode.SubItems.First();
            ItemType postTest = containerNode.SubItems.Last();

            //removing contetn wrapper.
            if (containerNode.SubItems.Count == 3 && containerNode.SubItems[1].isvisible == false)
            {
                var contentWrapper = containerNode.SubItems[1];
                //Remove subItems sequencing.
                for (int i = 0; i < contentWrapper.SubItems.Count; ++i)
                {
                    contentWrapper.SubItems[i].Sequencing = SequencingManager.CreateNewSequencing(contentWrapper.SubItems[i]);
                }
                contentWrapper.Parent = containerNode;

                contentWrapper.RemoveAndMerge();
            }

            //abolishing preTest
            preTest.Sequencing = SequencingManager.CreateNewSequencing(preTest);

            //abolishing postTest
            postTest.Sequencing = SequencingManager.CreateNewSequencing(postTest);
        }

        #endregion
    }

    [Description("Implements Random Post-Test sequencing pattern.")]
    public class RandomPostTestSequencingPattern : SequencingPattern
    {
        #region Constants

        protected const string numericTitle = "Random Post-Test";
        protected const string numericDescription = "Please enter number of tries:";

        #endregion

        #region Properties

        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        public override string ID
        {
            get
            {
                return "randPostPattern";
            }
        }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Random Post-Test Sequencing Pattern";
            }
        }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        public override string Description
        {
            get
            {
                return "Student is free to experience any sub-activity. Post-test is available only after all other sub-activities are completed/satisfied. Score, completion and satisfaction evaluation of affected activity is based on the result of the Post-Test. Student has ability to try pass Post-test N times. Each time, test is selected randomly from a set of tests. After N unsuccessful tries exit from current affected activity happens.";
            }
        }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        public override int Level
        {
            get
            {
                return 2;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets numeric value, invoking numeric dialog for user input.
        /// </summary>
        /// <param name="minimum">Integer value represent lower bound.</param>
        /// <param name="maximum">Integer value represent upper bound.</param>
        /// <param name="value">Referenced integer to set value to.</param>
        /// <returns>Boolean value 'true' if user submited value, 'false' - if canceled.</returns>
        protected static bool GetNumericValue(int minimum, int maximum, ref int value)
        {
            throw new NotImplementedException();

           // return NumericDialog.GetValue(minimum, maximum, ref value, 1, numericTitle, numericDescription);
        }

        /// <summary>
        /// Gets attemptLimit from defined node's sequencing elements.
        /// </summary>
        /// <param name="currentNode">ISequencing node to get value from.</param>
        /// <param name="value">Referenced integer value represents attemptLimit value.</param>
        /// <returns>Boolean value 'true' if attemptLimit was successfully got, otherwise 'false'.</returns>
        public static bool GetAttemptLimit(object currentNode, ref int value)
        {
            if (currentNode is ISequencing == false)
            {
                throw new InvalidOperationException("Can't get attemptLimit value from node without Sequencing support!");
            }

            ISequencing seqNode = currentNode as ISequencing;

            if (seqNode.Sequencing == null || seqNode.Sequencing.limitConditions == null || seqNode.Sequencing.limitConditions.attemptLimit == null)
            {
                return false;
            }

            int attemptLimit = 0;
            if (Int32.TryParse(seqNode.Sequencing.limitConditions.attemptLimit, out attemptLimit) == false)
            {
                return false;
            }

            value = attemptLimit;
            return true;
        }

        /// <summary>
        /// Sets attemptLimit value to defined node by getting user input.
        /// </summary>
        /// <param name="currentNode">Node to set value to.</param>
        /// <param name="value">Referenced integer to set value from.</param>
        /// <returns>Boolean value 'true' if user submited value, 'false' - if canceled.</returns>
        public static bool SetAttemptLimit(object currentNode, ref int value)
        {
            if (currentNode is ISequencing == false)
            {
                throw new InvalidOperationException("Can't set attemptLimit value for node without Sequencing support!");
            }

            var seqNode = currentNode as ISequencing;
           
            if (seqNode.Sequencing.limitConditions == null)
            {
                seqNode.Sequencing.limitConditions = new LimitConditionsType();
            }

            int attemptLimit = value;

            if (attemptLimit < 1)
            {
                attemptLimit = 1;
            }
            bool result = GetNumericValue(1, 9999, ref attemptLimit);
            if (result == true)
            {
                value = attemptLimit;
                seqNode.Sequencing.limitConditions.attemptLimit = attemptLimit.ToString();
            }

            return result;
        }

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        public override bool CanApplyPattern(object currentNode)
        {
            if (base.CanApplyPattern(currentNode) == false)
            {
                return false;
            }

            var itemContainer = currentNode as IItemContainer;

            //item should contain  post test 
            if (itemContainer.SubItems.Count < 2)
            {
                return false;
            }
            var postTest = itemContainer.SubItems.Last();

            //last item should be a control chapter with at least one item.
            bool result = postTest.PageType == PageType.ControlChapter && postTest.SubItems.Count > 0;

            return result;
        }

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        public override void ApplyPattern(object currentNode)
        {
            if (CanApplyPattern(currentNode) == false)
            {
                throw new InvalidOperationException("Can't apply Random Post-Test sequencing pattern to current node!");
            }

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            if (Course.Organization != null)
            {
                Course.Organization.objectivesGlobalToSystem = false;
            }

            string commonSeqRulesID = "common_test_rule_" + ID;

            string contentObjectiveID = "content_satisfied";
            string contentMappedObjeciveID = "global." + contentObjectiveID + "_" + Guid.NewGuid().ToString();
            string postTestObjectiveID = "post_test_satisfied";
            string postTestMappedObjeciveID = "global." + postTestObjectiveID + "_" + Guid.NewGuid().ToString();

            #region Sequencing Collection

            SequencingType seqForColl = new SequencingType();
            seqForColl.ID = commonSeqRulesID;

            #region Sequencing Poat Condition

            PostConditionRuleType collPostRule = SequencingManager.CreateSimplePostConditionRule(SequencingRuleConditionType.always, PostConditionRuleActionType.exitParent);

            seqForColl.sequencingRules = new SequencingRulesType();
            seqForColl.sequencingRules.postConditionRule = new ManifestNodeList<PostConditionRuleType>(seqForColl.sequencingRules);
            seqForColl.sequencingRules.postConditionRule.Add(collPostRule);

            #endregion

            #region Objectives

            seqForColl.objectives = new ObjectivesType();
            seqForColl.objectives.primaryObjective = new ObjectivesTypePrimaryObjective();
            seqForColl.objectives.primaryObjective.objectiveID = postTestObjectiveID;

            ObjectiveTypeMapInfo collMapInfo = new ObjectiveTypeMapInfo();
            collMapInfo.readNormalizedMeasure = false;
            collMapInfo.readSatisfiedStatus = false;
            collMapInfo.writeNormalizedMeasure = true;
            collMapInfo.targetObjectiveID = postTestMappedObjeciveID;

            seqForColl.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
            seqForColl.objectives.primaryObjective.mapInfo.Add(collMapInfo);


            #endregion

            #region Delivery Controls

            seqForColl.deliveryControls = new DeliveryControlsType();
            seqForColl.deliveryControls.completionSetByContent = true;
            seqForColl.deliveryControls.objectiveSetByContent = true;

            #endregion

            AddSeqCollectionSeq(seqForColl);

            #endregion

            #region Creating Content Wrapper

            ItemType contentWrapper;
            //If node contains only 2 elements: chapter and control chapter
            //then first one is set as dummy content wraper
            if (containerNode.SubItems.Count == 2 && containerNode.SubItems[0].PageType == PageType.Chapter)
            {
                contentWrapper = containerNode.SubItems[0];
            }
            //else group all, besides last elements to content wrapper.
            else
            {
                contentWrapper = ItemType.CreateNewItem("Content Wrapper", Guid.NewGuid().ToString(), null, PageType.Chapter);
                //1. Add children to grouping item.
                contentWrapper.SubItems.AddRange(containerNode.SubItems.GetRange(0, containerNode.SubItems.Count - 1));
                //2. Remove children
                containerNode.SubItems.RemoveRange(0, containerNode.SubItems.Count - 1);
                //3.Insert Content Wrapper
                containerNode.SubItems.Insert(0, contentWrapper);
            }
            
            #endregion

            #region Current Node Sequencing

            seqNode.Sequencing = new SequencingType();

            #region Control Mode

            seqNode.Sequencing.controlMode = new ControlModeType();
            seqNode.Sequencing.controlMode.choice = true;
            seqNode.Sequencing.controlMode.flow = true;

            #endregion

            #region Rollup Rules

            seqNode.Sequencing.rollupRules = new RollupRulesType();
            seqNode.Sequencing.rollupRules.rollupRule = new ManifestNodeList<RollupRuleType>(seqNode.Sequencing.rollupRules);

            RollupRuleTypeRollupConditionsRollupCondition nodeRollupRuleCond1 = new RollupRuleTypeRollupConditionsRollupCondition(ConditionOperatorType.noOp, RollupRuleConditionType.satisfied);
            RollupRuleTypeRollupConditionsRollupCondition nodeRollupRuleCond2 = new RollupRuleTypeRollupConditionsRollupCondition(ConditionOperatorType.noOp, RollupRuleConditionType.attemptLimitExceeded);
 
            RollupRuleType nodeRollupRule = SequencingManager.CreateRollupRule(ChildActivityType.any, ConditionCombinationType.any, new RollupRuleTypeRollupConditionsRollupCondition[] {nodeRollupRuleCond1, nodeRollupRuleCond2 }, RollupActionType.completed);
            
            seqNode.Sequencing.rollupRules.rollupRule.Add(nodeRollupRule);
            
            #endregion

            #endregion

            #region Content Wrapper Customizing/Sequencing

            contentWrapper = containerNode.SubItems.First();

            contentWrapper.isvisible = false;
            
            contentWrapper.Sequencing = new SequencingType();

            #region Control Mode

            contentWrapper.Sequencing.controlMode = new ControlModeType();
            contentWrapper.Sequencing.controlMode.flow = true;
            contentWrapper.Sequencing.controlMode.choice = true;

            #endregion

            #region Rollup Rules

            contentWrapper.Sequencing.rollupRules = new RollupRulesType();
            contentWrapper.Sequencing.rollupRules.rollupRule = new ManifestNodeList<RollupRuleType>(contentWrapper.Sequencing.rollupRules);
           
            contentWrapper.Sequencing.rollupRules.rollupObjectiveSatisfied = false;
            contentWrapper.Sequencing.rollupRules.rollupProgressCompletion = false;
            contentWrapper.Sequencing.rollupRules.objectiveMeasureWeight = 0;

            RollupRuleTypeRollupConditionsRollupCondition contentRollupCondion = new RollupRuleTypeRollupConditionsRollupCondition(ConditionOperatorType.noOp, RollupRuleConditionType.completed);

            RollupRuleType contentWrapperRule = SequencingManager.CreateRollupRule(ChildActivityType.all, ConditionCombinationType.any, new RollupRuleTypeRollupConditionsRollupCondition[] { contentRollupCondion }, RollupActionType.satisfied);
            contentWrapper.Sequencing.rollupRules.rollupRule.Add(contentWrapperRule);
            
            #endregion

            #region Objectives

            var seq = contentWrapper.Sequencing;
            SequencingManager.CustomizePrimaryObjectives(ref seq, contentWrapper.Identifier);
            contentWrapper.Sequencing = seq;

            contentWrapper.Sequencing.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
            ObjectiveMappingType contentMapInfo = new ObjectiveMappingType();
            contentMapInfo.writeSatisfiedStatus = true;
            contentMapInfo.targetObjectiveID = contentMappedObjeciveID;
            contentWrapper.Sequencing.objectives.primaryObjective.mapInfo.Add(contentMapInfo);

            #endregion
            
            #region Content Wrapper SubItems Sequencing

            for (int i = 0; i < contentWrapper.SubItems.Count; ++i)
            { 
                var currSubItem = contentWrapper.SubItems[i];
                if (currSubItem.IsLeaf == true)
                {
                    if (currSubItem.Sequencing == null)
                    {
                        currSubItem.Sequencing = new SequencingType();
                    }
                    currSubItem.Sequencing.deliveryControls = new DeliveryControlsType();
                    currSubItem.Sequencing.deliveryControls.objectiveSetByContent = true;
                    currSubItem.Sequencing.deliveryControls.completionSetByContent = true;
                }
            }

            #endregion
            
            #endregion

            #region Post Test Sequencing

            ItemType postTest = containerNode.SubItems[1];
            int numOfChildren = postTest.SubItems.Count;

            //Remove other patterns            
            //postTest.SequencingPatterns.RemoveAll(patt => patt.Level == 1);

            #region Sequencing

            if (postTest.Sequencing == null)
            {
                postTest.Sequencing = new SequencingType();
            }

            #region Control Mode

            postTest.Sequencing.controlMode = new ControlModeType();
            postTest.Sequencing.controlMode.choice = false;
            postTest.Sequencing.controlMode.flow = true;

            #endregion

            #region Sequencing Rules
            
            postTest.Sequencing.sequencingRules = new SequencingRulesType();
            postTest.Sequencing.sequencingRules.preConditionRule = new ManifestNodeList<PreConditionRuleType>(postTest.Sequencing.sequencingRules);
            postTest.Sequencing.sequencingRules.postConditionRule = new ManifestNodeList<PostConditionRuleType>(postTest.Sequencing.sequencingRules);


            //If the content is not completed, then don't allow access to the post test 
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond1 = new SequencingRuleTypeRuleConditionsRuleCondition(contentObjectiveID, 0, ConditionOperatorType.not, SequencingRuleConditionType.satisfied);
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond2 = new SequencingRuleTypeRuleConditionsRuleCondition(contentObjectiveID, 0, ConditionOperatorType.not, SequencingRuleConditionType.objectiveStatusKnown);
            postTest.Sequencing.sequencingRules.preConditionRule.Add(SequencingManager.CreatePreConditionRule(ConditionCombinationType.any, new SequencingRuleTypeRuleConditionsRuleCondition[] { postTestSeqRuleCond1, postTestSeqRuleCond2 }, PreConditionRuleActionType.disabled));

            //Once the attempt limit has been exceeded or a test has been passed, the post test is disabled
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond3 = new SequencingRuleTypeRuleConditionsRuleCondition(null, 0, ConditionOperatorType.noOp, SequencingRuleConditionType.attemptLimitExceeded);
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond4 = new SequencingRuleTypeRuleConditionsRuleCondition(null, 0, ConditionOperatorType.noOp, SequencingRuleConditionType.satisfied);
            postTest.Sequencing.sequencingRules.preConditionRule.Add(SequencingManager.CreatePreConditionRule(ConditionCombinationType.any, new SequencingRuleTypeRuleConditionsRuleCondition[] { postTestSeqRuleCond3, postTestSeqRuleCond4 }, PreConditionRuleActionType.disabled));

            //When a test exits, its exit will bubble up to this parent. When we 
            //  detect the exit, we want to retry the activity if it was not satisfied and
            //  there are attempts left. Note that we need two rules here because there is 
            //  no logical grouping operator (i.e. you can't have AND and OR conditions
            //  present in the same rule).
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond5 = new SequencingRuleTypeRuleConditionsRuleCondition(null, 0, ConditionOperatorType.not, SequencingRuleConditionType.satisfied);
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond6 = new SequencingRuleTypeRuleConditionsRuleCondition(null, 0, ConditionOperatorType.not, SequencingRuleConditionType.attemptLimitExceeded);
            postTest.Sequencing.sequencingRules.postConditionRule.Add(SequencingManager.CreatePostConditionRule(ConditionCombinationType.all, new SequencingRuleTypeRuleConditionsRuleCondition[] { postTestSeqRuleCond5, postTestSeqRuleCond6 }, PostConditionRuleActionType.retry));

            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond7 = new SequencingRuleTypeRuleConditionsRuleCondition(null, 0, ConditionOperatorType.not, SequencingRuleConditionType.objectiveStatusKnown);
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond8 = new SequencingRuleTypeRuleConditionsRuleCondition(null, 0, ConditionOperatorType.not, SequencingRuleConditionType.attemptLimitExceeded);
            postTest.Sequencing.sequencingRules.postConditionRule.Add(SequencingManager.CreatePostConditionRule(ConditionCombinationType.all, new SequencingRuleTypeRuleConditionsRuleCondition[] { postTestSeqRuleCond7, postTestSeqRuleCond8 }, PostConditionRuleActionType.retry));
            
            //Once the test is completed (either because of too many attempts or because 
            //it was passed, exit the course)
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond9 = new SequencingRuleTypeRuleConditionsRuleCondition(null, 0, ConditionOperatorType.noOp, SequencingRuleConditionType.objectiveStatusKnown);
            SequencingRuleTypeRuleConditionsRuleCondition postTestSeqRuleCond10 = new SequencingRuleTypeRuleConditionsRuleCondition(null, 0, ConditionOperatorType.noOp, SequencingRuleConditionType.attemptLimitExceeded);
            postTest.Sequencing.sequencingRules.postConditionRule.Add(SequencingManager.CreatePostConditionRule(ConditionCombinationType.any, new SequencingRuleTypeRuleConditionsRuleCondition[] { postTestSeqRuleCond9, postTestSeqRuleCond10 }, PostConditionRuleActionType.exitAll));
            
            #endregion

            #region Limit Conditions

            if (postTest.Sequencing.limitConditions == null)
            {
                postTest.Sequencing.limitConditions = new LimitConditionsType();
            }

            int attemptLimit = numOfChildren;
            bool canGetAttemptLimit = GetAttemptLimit(postTest, ref attemptLimit);
            if (canGetAttemptLimit == false)
            {
                SetAttemptLimit(postTest, ref attemptLimit);
            }

            #endregion

            #region Rollup Rules

            postTest.Sequencing.rollupRules = new RollupRulesType();
            postTest.Sequencing.rollupRules.rollupRule = new ManifestNodeList<RollupRuleType>(postTest.Sequencing.rollupRules);

            //If one test becomes satisfied, then the post test as a whole is satisfied 
            RollupRuleTypeRollupConditionsRollupCondition postTestRollupCond1 = new RollupRuleTypeRollupConditionsRollupCondition(ConditionOperatorType.noOp, RollupRuleConditionType.satisfied);
            postTest.Sequencing.rollupRules.rollupRule.Add(SequencingManager.CreateRollupRule(ChildActivityType.any, ConditionCombinationType.any, new RollupRuleTypeRollupConditionsRollupCondition[] { postTestRollupCond1 }, RollupActionType.satisfied));

            //Once a test is failed, the post test is marked as failed. It will stay that way 
            // if the attempt limit is exceeded and the test can't be attempted again.
            RollupRuleTypeRollupConditionsRollupCondition postTestRollupCond2 = new RollupRuleTypeRollupConditionsRollupCondition(ConditionOperatorType.not, RollupRuleConditionType.satisfied);
            postTest.Sequencing.rollupRules.rollupRule.Add(SequencingManager.CreateRollupRule(ChildActivityType.any, ConditionCombinationType.any, new RollupRuleTypeRollupConditionsRollupCondition[] { postTestRollupCond2 }, RollupActionType.notSatisfied));
           
            #endregion

            #region Objectives

            postTest.Sequencing.objectives = new ObjectivesType();
            postTest.Sequencing.objectives.primaryObjective = new ObjectivesTypePrimaryObjective();
            postTest.Sequencing.objectives.primaryObjective.objectiveID = postTestObjectiveID;

            ObjectiveMappingType postTestPrimaryMapInfo = new ObjectiveMappingType(postTestMappedObjeciveID)
            {
                readNormalizedMeasure=true, readSatisfiedStatus = false
            };

            postTest.Sequencing.objectives.primaryObjective.mapInfo = new List<ObjectiveTypeMapInfo>();
            postTest.Sequencing.objectives.primaryObjective.mapInfo.Add(postTestPrimaryMapInfo);

            postTest.Sequencing.objectives.objective = new ManifestNodeList<ObjectivesTypeObjective>(postTest.Sequencing.objectives);

            //Read from the content completed global to disable the post test until the content is completed
            ObjectivesTypeObjective postTestObjective1 = new ObjectivesTypeObjective()
            {
                mapInfo = new List<ObjectiveTypeMapInfo>()
            };

            ObjectiveMappingType postTestObjective1MapInfo = new ObjectiveMappingType(contentObjectiveID)
            {
               readSatisfiedStatus = true
            };

            postTestObjective1.mapInfo.Add(postTestObjective1MapInfo);

            postTest.Sequencing.objectives.objective.Add(postTestObjective1);

            #endregion

            #region Randomization Controls

            //On every new attempt of the post test, randomize the order of the tests.
            postTest.Sequencing.randomizationControls = new RandomizationType();
            postTest.Sequencing.randomizationControls.randomizationTiming = RandomTimingType.onEachNewAttempt;
            postTest.Sequencing.randomizationControls.reorderChildren = true;

            #endregion

            #endregion

            #region Sub Items Sequencing

            for (int i = 0; i < postTest.SubItems.Count; ++i)
            {
                ItemType currSubItem = postTest.SubItems[i];
                
                currSubItem.isvisible = false;

                currSubItem.Sequencing = new SequencingType();
                currSubItem.Sequencing.IDRef = commonSeqRulesID;
/*
                currSubItem.Presentation = new PresentationType();
                currSubItem.Presentation.navigationInterface = new List<HideLMSUIType>();
                currSubItem.Presentation.navigationInterface.Add(HideLMSUIType.suspendAll);
  */          }

            #endregion

            #endregion
        }

        /// <summary>
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        public override void AbolishPattern(object currentNode)
        {
            base.AbolishPattern(currentNode);

            var seqNode = currentNode as ISequencing;
            var containerNode = currentNode as IItemContainer;

            string commonSeqRulesID = "common_test_rule_" + ID;

            RemoveSeqCollectionSeq(commonSeqRulesID);

            seqNode.Sequencing = new SequencingType();

            var contentWrapper = containerNode.SubItems[0];
            var postTest = containerNode.SubItems[1];

            contentWrapper.Sequencing = SequencingManager.CreateNewSequencing(contentWrapper);
            postTest.Sequencing = SequencingManager.CreateNewSequencing(postTest);
 
            for (int i = 0; i < contentWrapper.SubItems.Count; ++i)
            {
                contentWrapper.SubItems[i].Sequencing = SequencingManager.CreateNewSequencing(contentWrapper.SubItems[i]);
            }

            for (int i = 0; i < postTest.SubItems.Count; ++i)
            {
                postTest.SubItems[i].Sequencing = SequencingManager.CreateNewSequencing(postTest.SubItems[i]);
            }
        }

        #endregion
    }
}