using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingSystem
{
    public class Settings
    {
        /// <summary>
        /// Represents the directory path, where all work is done.
        /// </summary>
        private string testingDirectory;

        /// <summary>
        /// Represents the list of compilers used on testing.
        /// </summary>
        private List<Compile.Compiler> compilers;

        /// <summary>
        /// Gets or sets the directory path, where all work is done.
        /// </summary>
        /// 
        /// <value>
        /// The directory path, where all work is done.
        /// </value>
        public string TestingDirectory
        {
            get
            {
                return testingDirectory;
            }
            set
            {
                testingDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of compilers used on testing.
        /// </summary>
        /// 
        /// <value>
        /// The list of compilers used on testing.
        /// </value>
        public List<Compile.Compiler> Compilers
        {
            get
            {
                return compilers;
            }
            set
            {
                compilers = value;
            }
        }
    }
}
