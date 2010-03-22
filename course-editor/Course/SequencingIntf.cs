using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireFly.CourseEditor.Course.Manifest
{
    public interface ISequencingPattern
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
        /// Applies sequencing elements to node and related nodes.
        /// </summary>
        /// <param name="currentNode">Node to apply sequencing to.</param>
        void ApplyPattern([NotNull]object currentNode);

        /// <summary>
        /// Checks structure for possibility to apply current sequencing pattern to node.
        /// </summary>
        /// <param name="currentNode">Node to check possibility of applying pattern.</param>
        /// <returns>Boolean value 'true' if can apply pattern to node. Otherwise 'false'.</returns>
        bool CanApplyPattern([NotNull]object currentNode);
    }

    public interface ISequencing
    {
        SequencingType Sequencing { get; set; }
    }
}
