// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayModel.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.TestingSystem.ViewModels
{
    public class PlayModel
    {
        public long AttemptId { get; set; }

        public int CurriculumChapterTopicId { get; set; }

        public TopicTypeEnum TopicType { get; set; }
    }
}