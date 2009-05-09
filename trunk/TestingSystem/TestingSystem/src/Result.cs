namespace TestingSystem
{
    public class Result
    {
        /// <summary>
        /// Represents the status of tested program. 
        /// </summary>
        private Status programStatus;

        /// <summary>
        /// Represents the output received from the program.
        /// </summary>
        private string output;

        /// <summary>
        /// Represents the error stream received from the program.
        /// </summary>
        private string error;

        /// <summary>
        /// Represents the amount of memory(in kilobytes) that program used during execution.
        /// </summary>
        private int memoryUsed;

        /// <summary>
        /// Represents the time(in milliseconds) of program execution.
        /// </summary>
        private int timeUsed;

        /// <summary>
        /// Gets or sets the status of testing program.
        /// </summary>
        /// 
        /// <value>
        /// The status of testing program.
        /// </value>
        public Status ProgramStatus
        {
            get
            {
                return programStatus;
            }
            set
            {
                programStatus = value;
            }
        }

        /// <summary>
        /// Gets or sets the content of output stream of testing program.
        /// </summary>
        /// 
        /// <value>
        /// The content of output stream of testing program.
        /// </value>
        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
            }
        }

        /// <summary>
        /// Gets or sets the content of error stream of testing program.
        /// </summary>
        /// 
        /// <value>
        /// The content of error stream of testing program.
        /// </value>
        public string Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of memory(in kilobytes) that program used during execution.
        /// </summary>
        /// 
        /// <value>
        /// The amount of memory(in kilobytes) that program used during execution.
        /// </value>
        public int MemoryUsed
        {
            get
            {
                return memoryUsed;
            }
            set
            {
                memoryUsed = value;
            }
        }

        /// <summary>
        /// Gets or sets the time(in milliseconds) of program execution.
        /// </summary>
        /// 
        /// <value>
        /// The time(in milliseconds) of program execution.
        /// </value>
        public int TimeUsed
        {
            get
            {
                return timeUsed;
            }
            set
            {
                timeUsed = value;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="Result"/> class.
        /// </summary>
        public Result()
        {

        }
    }
}
