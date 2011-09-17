namespace CompileSystem.Compiling
{
    public class Result
    {
        /// <summary>
        /// Gets or sets the status of testing program.
        /// </summary>
        /// 
        /// <value>
        /// The status of testing program.
        /// </value>
        public Status ProgramStatus { get; set; }

        /// <summary>
        /// Gets or sets the content of output stream of testing program.
        /// </summary>
        /// 
        /// <value>
        /// The content of output stream of testing program.
        /// </value>
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets the content of error stream of testing program.
        /// </summary>
        /// 
        /// <value>
        /// The content of error stream of testing program.
        /// </value>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the amount of memory(in kilobytes) that program used during execution.
        /// </summary>
        /// 
        /// <value>
        /// The amount of memory(in kilobytes) that program used during execution.
        /// </value>
        public int MemoryUsed { get; set; }

        /// <summary>
        /// Gets or sets the time(in milliseconds) of program execution.
        /// </summary>
        /// 
        /// <value>
        /// The time(in milliseconds) of program execution.
        /// </value>
        public int TimeUsed { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="Result"/> class.
        /// </summary>
        public Result()
        {

        }
    }
}
