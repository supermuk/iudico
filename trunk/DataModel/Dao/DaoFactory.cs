using IUDICO.DataModel.Dao;

namespace IUDICO.DataModel.Dao
{
    public class DaoFactory
    {
        public static CourseDao CourseDao
        {
            get
            {
                return new CourseDao();
            }
        }

        public static ThemeDao ChapterDao
        {
            get
            {
                return new ThemeDao();
            }
        }

        public static PageDao PageDao
        {
            get
            {
                return new PageDao();
            }
        }

        public static QuestionDao CorrectAnswerDao
        {
            get
            {
                return new QuestionDao();
            }
        }

        public static UserAnswerDao UserAnswerDao
        {
            get
            {
                return new UserAnswerDao();
            }
        }

        public static FilesDao FilesDao
        {
            get
            {
                return new FilesDao();
            }
        }

        public static CompiledQuestionDao CompiledQuestionDao
        {
            get
            {
                return new CompiledQuestionDao();
            }
        }

        public static CompiledQuestionDataDao CompiledQuestionDataDao
        {
            get
            {
                return new CompiledQuestionDataDao();
            }
        }
    }
}