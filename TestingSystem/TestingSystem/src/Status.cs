namespace TestingSystem
{
    /// <summary>
    /// Represents various statuses of program, while or after testing.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Program was compiled, it passed time and memory limits, but it returns wrong output.
        /// </summary>
        WrongAnswer = 1,

        /// <summary>
        /// Program was compiled, it passed time and memory limits, and it returns correct output.
        /// </summary>
        Accepted = 2,

        /// <summary>
        /// Program was compiled, but it takes too much time to run.
        /// </summary>
        TimeLimit = 3,

        /// <summary>
        /// Program was compiled, but it takes too much memory during run.
        /// </summary>
        MemoryLimit = 4,

        /// <summary>
        /// Program wasn't compiled succesfully.
        /// </summary>
        CompilationError = 5,

        /// <summary>
        /// Program was compiled, and it is running right now.
        /// </summary>
        Running = 6,

        /// <summary>
        /// Program was received, and it is waiting too proceed.
        /// </summary>
        Enqueued = 7,

        /// <summary>
        /// Program was compiled, but it crashed during execution.
        /// </summary>
        Crashed = 8,

    };
}
