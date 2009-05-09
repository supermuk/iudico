using System;

namespace TestingSystem
{
    /// <summary>
    /// This class represents program to test.
    /// </summary>
    [Serializable]
    public class Program
    {
        /// <summary>
        /// Represents the source of provided program.
        /// </summary>
        private string source;

        /// <summary>
        /// Resresents the input test for provided program.
        /// </summary>
        private string inputTest;

        /// <summary>
        /// Resresents the output test for provided program.
        /// </summary>
        private string outputTest;

        /// <summary>
        /// Resresents the language of provided program.
        /// </summary>
        private string language;

        /// <summary>
        /// Represents the maximum time(in milliseconds) of program execution.
        /// </summary>
        private int timeLimit;

        /// <summary>
        /// Represents the maximum amount of memory(in kilobytes) that program can use during execution.
        /// </summary>
        private int memoryLimit;

        /// <summary>
        /// Gets or sets the source of provided program.
        /// </summary>
        /// 
        /// <value>
        /// The source of provided program.
        /// </value>
        public string Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
            }
        }

        /// <summary>
        /// Gets or sets the input test for provided program.
        /// </summary>
        /// 
        /// <value>
        /// The input test for provided program.
        /// </value>
        public string InputTest
        {
            get
            {
                return inputTest;
            }
            set
            {
                inputTest = value;
            }
        }

        /// <summary>
        /// Gets or sets the output test for provided program.
        /// </summary>
        /// 
        /// <value>
        /// The output test for provided program.
        /// </value>
        public string OutputTest
        {
            get
            {
                return outputTest;
            }
            set
            {
                outputTest = value;
            }
        }

        /// <summary>
        /// Gets or sets the language of provided program.
        /// </summary>
        /// 
        /// <value>
        /// The language of provided program.
        /// </value>
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum time(in milliseconds) of program execution.
        /// </summary>
        /// 
        /// <value>
        /// The maximum time(in milliseconds) of program execution.
        /// </value>
        public int TimeLimit
        {
            get
            {
                return timeLimit;
            }
            set
            {
                timeLimit = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum amount of memory(in kilobytes) that program can use during execution.
        /// </summary>
        /// 
        /// <value>
        /// The maximum amount of memory(in kilobytes) that program can use during execution.
        /// </value>
        public int MemoryLimit
        {
            get
            {
                return memoryLimit;
            }
            set
            {
                memoryLimit = value;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="Program"/> class.
        /// </summary>
        public Program()
        {

        }
    }
}
