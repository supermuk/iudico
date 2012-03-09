namespace CompileSystem.Compiling.Compile
{
    /// <summary>
    /// Represents the result of compilation.
    /// </summary>
    public class CompileResult
    {
        /// <summary>
        /// Gets flag that indicates if source was sucessfully compiled.
        /// </summary>
        /// 
        /// <value>
        /// The flag that indicates if source was sucessfully compiled.
        /// </value>
        public bool Compiled { get; private set; }

        /// <summary>
        /// Gets the standard output stream content of compiler.
        /// </summary>
        /// 
        /// <value>
        /// The standard output stream content of compiler.
        /// </value>
        public string StandartOutput { get; private set; }

        /// <summary>
        /// Gets the standard error stream content of compiler.
        /// </summary>
        /// 
        /// <value>
        /// The standard error stream content of compiler.
        /// </value>
        public string StandartError { get; private set; }

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
            Compiled = compiled;
            StandartOutput = standartOutput;
            StandartError = standartError;
        }
    }
}
