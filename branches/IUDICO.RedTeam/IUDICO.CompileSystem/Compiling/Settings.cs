using System.Collections.Generic;
using CompileSystem.Compiling.Compile;

namespace CompileSystem.Compiling
{
    public class Settings
    {
        /// <summary>
        /// Gets or sets the directory path, where all work is done.
        /// </summary>
        /// 
        /// <value>
        /// The directory path, where all work is done.
        /// </value>
        public string TestingDirectory { get; set; }

        /// <summary>
        /// Gets or sets the list of compilers used on testing.
        /// </summary>
        /// 
        /// <value>
        /// The list of compilers used on testing.
        /// </value>
        public List<Compiler> Compilers { get; set; }
    }
}
