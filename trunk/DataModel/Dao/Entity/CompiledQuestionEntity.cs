using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.Dao.Entity
{
    public class CompiledQuestionEntity
    {
        private readonly int id;
        private readonly int languageRef;
        private readonly int timeLimit;
        private readonly int memoryLimit;
        private readonly int outputLimit;

        public CompiledQuestionEntity(Language language, int timeLimit, int memoryLimit, int outputLimit)
        {
            id = UniqueId.Generate();
            languageRef = (int)language;
            this.timeLimit = timeLimit;
            this.memoryLimit = memoryLimit;
            this.outputLimit = outputLimit;
        }

        public int OutputLimit
        {
            get { return outputLimit; }
        }

        public int MemoryLimit
        {
            get { return memoryLimit; }
        }

        public int TimeLimit
        {
            get { return timeLimit; }
        }

        public int LanguageRef
        {
            get { return languageRef; }
        }

        public int Id
        {
            get { return id; }
        }
    }
}