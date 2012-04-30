namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class ViewCurriculumModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string DisciplineName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsValid { get; set; }
    }
}