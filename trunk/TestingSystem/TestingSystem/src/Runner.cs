using System.Diagnostics;
using System.IO;
using System.Security;
using System.Xml.Serialization;
using System;
using System.ComponentModel;

namespace TestingSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class Runner
    {
        /// <summary>
        /// Represents filename of ExeWrapper used to wrap *.exe files for correct exception workout.
        /// </summary>
        private readonly string executerPath;
        /// <summary>
        /// Represents the username used to launch new process.
        /// </summary>
        private readonly string userName;

        /// <summary>
        /// Represents the password of user to launch new process.
        /// </summary>
        private readonly string password;

        /// <summary>
        /// Gets the username used to execute process.
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
        }

        /// <summary>
        /// Executes provided exe file and returns the result of program using.
        /// </summary>
        /// 
        /// <param name="exePath">
        /// Path of exe file to run.
        /// </param>
        /// 
        /// <param name="program">
        /// Execunitg constraints.(like memory limit, time limit)
        /// </param>
        /// 
        /// <returns>
        /// Detailed result of program executing.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If any argument is null.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// If provided path is invalid.
        /// </exception>
        /// <exception cref="">
        /// 
        /// </exception>
        public Result Execute(string exePath, Program program)
        {
            //validate arguments
            ProjectHelper.ValidateNotNull(program, "program");
            ProjectHelper.ValidateFileExists(exePath, "exePath");
            ProjectHelper.ValidateFileExists(executerPath, "exePath");

            XmlSerializer serializer = new XmlSerializer(typeof(Program));

            //create new process
            using (Process exeProcess = new Process())
            {
                exeProcess.StartInfo.FileName = executerPath;

                exeProcess.StartInfo.UserName = userName;
                exeProcess.StartInfo.Password = GetSecureString(password);

                exeProcess.StartInfo.RedirectStandardError = true;
                exeProcess.StartInfo.RedirectStandardInput = true;
                exeProcess.StartInfo.RedirectStandardOutput = true;
                exeProcess.StartInfo.UseShellExecute = false;
                exeProcess.StartInfo.CreateNoWindow = true;

                //serialize problem
                using (MemoryStream inputStream = new MemoryStream())
                {
                    serializer.Serialize(inputStream, program);

                    //start process
                    exeProcess.Start();

                    exeProcess.StandardInput.WriteLine(exePath);
                    inputStream.WriteTo(exeProcess.StandardInput.BaseStream);
                    exeProcess.StandardInput.Close();

                    XmlSerializer deserializer = new XmlSerializer(typeof(Result));

                    //deserialize output
                    string output = exeProcess.StandardOutput.ReadToEnd();
                    //terminate process
                    if (!exeProcess.HasExited)
                    {
                        exeProcess.Kill();
                    }
                    byte[] buffer = new byte[output.Length];
                    for (int i = 0; i < output.Length; i++)
                    {
                        buffer[i] = (byte)output[i];
                    }
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        outputStream.Write(buffer, 0, buffer.Length);
                        outputStream.Position = 0;
                        Result result = deserializer.Deserialize(outputStream) as Result;
                        return result;
                    }
                }
            }

        }

        /// <summary>
        /// Create new instance of <see cref="Runner"/> class.
        /// </summary>
        /// 
        /// <remarks>
        /// Empty arguments mean current user logged in.
        /// </remarks>
        /// 
        /// <param name="userName">
        /// Username used to launch new process.
        /// </param>
        /// <param name="password">
        /// Password of user to launch new process.
        /// </param>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If any argument is null.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// If pair username/passford is not valid.
        /// </exception>
        public Runner(string userName, string password)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            executerPath = baseDirectory + @"Bin\ExeWrapper.exe";

            //validate arguments
            ProjectHelper.ValidateNotNull(userName, "userName");
            ProjectHelper.ValidateNotNull(password, "password");
            //create some dummy process to validate username/password pair
            try
            {
                using (Process dummyProcess = new Process())
                {
                    dummyProcess.StartInfo.UserName = userName;
                    dummyProcess.StartInfo.Password = GetSecureString(password);
                    dummyProcess.StartInfo.FileName = "cmd";
                    dummyProcess.StartInfo.UseShellExecute = false;
                    dummyProcess.Start();
                    dummyProcess.Kill();
                }
            }
            catch
            {
                //re throw any exception
                throw;
            }

            //set arguments
            this.userName = userName;
            this.password = password;
        }

        /// <summary>
        /// Converts string into SecureString.
        /// </summary>
        /// 
        /// <param name="str">
        /// String to convert.
        /// </param>
        /// 
        /// <returns>
        /// Converted SecureString.
        /// </returns>
        private static SecureString GetSecureString(string str)
        {
            SecureString secureString = new SecureString();
            for (int i = 0; i < str.Length; i++)
            {
                secureString.AppendChar(str[i]);
            }

            return secureString;
        }
    }


}
