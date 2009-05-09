using System.Collections.Generic;

namespace TestingSystem
{
    public class Settings
    {
        /// <summary>
        /// Represents the username used to launch new process.
        /// </summary>
        private string userName;

        /// <summary>
        /// Represents the password of user to launch new process.
        /// </summary>
        private string password;

        /// <summary>
        /// Represents the directory path, where all work is done.
        /// </summary>
        private string testingDirectory;

        /// <summary>
        /// Represents the list of compilers used on testing.
        /// </summary>
        private List<Compile.Compiler> compilers;

        /// <summary>
        /// Gets or sets the username used to execute process.
        /// </summary>
        /// 
        /// <value>
        /// The username used to execute process.
        /// </value>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        /// <summary>
        /// Gets or sets the password of user to launch new process.
        /// </summary>
        /// 
        /// <value>
        /// The password of user to launch new process.
        /// </value>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

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
