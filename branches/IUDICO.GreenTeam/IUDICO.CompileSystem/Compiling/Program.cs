using System;

namespace CompileSystem.Compiling
{
    /// <summary>
    /// This class represents program to test.
    /// </summary>
    [Serializable]
    public class Program
    {
        /// <summary>
        /// Gets or sets the source of provided program.
        /// </summary>
        /// 
        /// <value>
        /// The source of provided program.
        /// </value>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the input test for provided program.
        /// </summary>
        /// 
        /// <value>
        /// The input test for provided program.
        /// </value>
        public string InputTest { get; set; }

        /// <summary>
        /// Gets or sets the output test for provided program.
        /// </summary>
        /// 
        /// <value>
        /// The output test for provided program.
        /// </value>
        public string OutputTest { get; set; }

        /// <summary>
        /// Gets or sets the language of provided program.
        /// </summary>
        /// 
        /// <value>
        /// The language of provided program.
        /// </value>
        public Language Language { get; set; }

        /// <summary>
        /// Gets or sets the maximum time(in milliseconds) of program execution.
        /// </summary>
        /// 
        /// <value>
        /// The maximum time(in milliseconds) of program execution.
        /// </value>
        public int TimeLimit { get; set; }

        /// <summary>
        /// Gets or sets the maximum amount of memory(in kilobytes) that program can use during execution.
        /// </summary>
        /// 
        /// <value>
        /// The maximum amount of memory(in kilobytes) that program can use during execution.
        /// </value>
        public int MemoryLimit { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="Program"/> class.
        /// </summary>
        public Program()
        {

        }
    }
}
