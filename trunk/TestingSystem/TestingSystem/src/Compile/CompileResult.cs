namespace TestingSystem.Compile
{
    /// <summary>
    /// Represents the result of compilation.
    /// </summary>
    public class CompileResult
    {
        /// <summary>
        /// Indicates if source was sucessfully compiled.
        /// </summary>
        private bool compiled;

        /// <summary>
        /// Represents the standard output stream content of compiler.
        /// </summary>
        private string standartOutput;

        /// <summary>
        /// Represents the standard error stream content of compiler.
        /// </summary>
        private string standartError;

        /// <summary>
        /// Gets flag that indicates if source was sucessfully compiled.
        /// </summary>
        /// 
        /// <value>
        /// The flag that indicates if source was sucessfully compiled.
        /// </value>
        public bool Compiled
        {
            get
            {
                return compiled;
            }
        }

        /// <summary>
        /// Gets the standard output stream content of compiler.
        /// </summary>
        /// 
        /// <value>
        /// The standard output stream content of compiler.
        /// </value>
        public string StandartOutput
        {
            get
            {
                return standartOutput;
            }
        }

        /// <summary>
        /// Gets the standard error stream content of compiler.
        /// </summary>
        /// 
        /// <value>
        /// The standard error stream content of compiler.
        /// </value>
        public string StandartError
        {
            get
            {
                return standartError;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="CompileResult"/> class with provided parameters.
        /// </summary>
        ///
        /// <param name="compiled">
        /// The flag that indicates if source was sucessfully compiled.
        /// </param>
        /// <param name="standartOutput">
        /// The standard output stream content of compiler.
        /// </param>
        /// <param name="standartError">
        /// The standard error stream content of compiler.
        /// </param>
        public CompileResult(bool compiled, string standartOutput, string standartError)
        {
            this.compiled = compiled;
            this.standartOutput = standartOutput;
            this.standartError = standartError;
        }
    }
}
