namespace IUDICO.DisciplineManagement.Models.ViewDataClasses
{
    public class ViewTopicModel
    {
        #region Properties

        public int Id { get; set; }
        public string TopicName { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string TestTopicType { get; set; }
        public string TestCourseName { get; set; }
        public string TheoryTopicType { get; set; }
        public string TheoryCourseName { get; set; }

        #endregion
    }
}