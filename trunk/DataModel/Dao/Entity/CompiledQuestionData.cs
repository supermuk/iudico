using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.Dao.Entity
{
    public class CompiledQuestionDataEntity
    {
        private readonly int id;
        private readonly int compiledQuestionRef;
        private readonly string input;
        private readonly string output;

        public CompiledQuestionDataEntity(int compiledQuestionRef, string input, string output)
        {
            id = UniqueId.Generate();
            this.compiledQuestionRef = compiledQuestionRef;
            this.input = input;
            this.output = output;
        }

        public string Output
        {
            get { return output; }
        }

        public string Input
        {
            get { return input; }
        }

        public int CompiledQuestionRef
        {
            get { return compiledQuestionRef; }
        }

        public int Id
        {
            get { return id; }
        }
    }
}