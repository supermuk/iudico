using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.DB
{
    public enum DB_OBJECT_TYPE
    {
        [SecuredObjectType("Course", typeof(TblCourses))]
        COURSE,

        [SecuredObjectType("Theme", typeof(TblThemes))]
        THEME,

        [SecuredObjectType("Page", typeof(TblPages))]
        PAGE,

        [SecuredObjectType("Question", typeof(TblQuestions))]
        QUESTION,

        [SecuredObjectType("CompiledQuestion", typeof(TblCompiledQuestions))]
        COMPILED_QUESTION,

        [SecuredObjectType("CompiledQuestionData", typeof(TblCompiledQuestionsData))]
        COMPILED_QUESTION_DATA,

        [SecuredObjectType("Curriculum", typeof(TblCurriculums))]
        CURRICULUMN,

        [SecuredObjectType("Stage", typeof(TblStages))]
        STAGE,

        [SecuredObjectType("User", typeof(TblUsers))]
        USER,

        [SecuredObjectType("Group", typeof(TblGroups))]
        GROUP
    }
}