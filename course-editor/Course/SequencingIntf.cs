using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireFly.CourseEditor.Course.Manifest
{
    public interface ISequencingPattern: IComparable
    {
        /// <summary>
        /// Gets identifier of Sequencing Pattern.
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Gets full title of the pattern.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets full description about pattern.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets int value indicating level of current pattern. Patterns with same level could not be applied to same node.
        /// </summary>
        int Level { get; }

        /// <summary>
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        void ApplyPattern([NotNull]object currentNode);

        /// <summary>
        /// Removes affected sequencing elements from node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to abolish sequencing from.</param>
        void AbolishPattern([NotNull]object currentNode);

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        bool CanApplyPattern([NotNull]object currentNode);
    }

    public interface ISequencingPatternCollection: IList<ISequencingPattern>
    {
        /// <summary>
        /// Defines node, sequencingCollection is applied to.
        /// </summary>
        [NotNull]
        object ParentNode
        {
            get;
            set;
        }

        /// <summary>
        /// Removes pattern of defined type from collection.
        /// </summary>
        /// <param name="patternType">Type of sequecning pattern.</param>
        /// <returns>Boolean 'true' if pattern was found and removed successfully, otherwise 'false'.</returns>
        bool Remove([NotNull]Type patternType);

       /* /// <summary>
        /// Adds pattern of defined type to collection.
        /// </summary>
        /// <param name="patternType">Type of sequencing pattern to add.</param>
        void Add([NotNull]Type patternType);
        */

        /// <summary>
        /// Applies all SequencingPatterns in collection to node.
        /// </summary>
        void ApplyAll();

        /// <summary>
        /// Defines, if all patterns in container are applicable to current ParrentNode.
        /// </summary>
        /// <returns>Boolean value 'true' if all patterns in collection are applicable. Otherwise 'false'.</returns>
        bool CanApplyAll();

        /// <summary>
        /// Removes all patterns, which are inapplicable.
        /// </summary>
        /// <returns>Integer value - a number of removed patterns.</returns>
        int RemoveInapplicable();

        /// <summary>
        /// Defines if collection contains pattern of defined type.
        /// </summary>
        /// <param name="patternType">Type of Pattern to check.</param>
        /// <returns>Boolean value 'true' if collection contains pattern of patternType. Otherwise returns 'false'.</returns>
        bool ContainsPattern([NotNull]Type patternType);

        event Action ParentNodeChanged;

        event Action<ISequencingPattern> PatternAdded;

        event Action<ISequencingPattern> PatternRemoved;
    }

    public interface ISequencing
    {
        SequencingType Sequencing { get; set; }
        SequencingPatternList SequencingPatterns { get; set; }
    }
}