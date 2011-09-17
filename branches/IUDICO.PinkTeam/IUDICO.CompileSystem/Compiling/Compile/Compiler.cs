using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CompileSystem.Compiling.Compile
{
    /// <summary>
    /// Compiler class that is used to create *.exe file from provided source.
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// Represents the "$CompilerDirectory$" constant string.
        /// </summary>
        /// 
        /// <remarks>
        /// This string is used in compilator arguments and must be replaced with actual value.
        /// </remarks>
        public const string CompilerDirectory = "$CompilerDirectory$";

        /// <summary>
        /// Represents the "$SourceFilePath$" constant string.
        /// </summary>
        /// 
        /// <remarks>
        /// This string is used in compilator arguments and must be replaced with actual value.
        /// </remarks>
        public const string SourceFilePath = "$SourceFilePath$";

        /// <summary>
        /// Gets the name of compilator.
        /// </summary>
        /// 
        /// <value>
        /// The name of compilator.
        /// </value>
        public Language Name { get; private set; }

        /// <summary>
        /// Gets the location of compilator in file system.
        /// </summary>
        /// 
        /// <value>
        /// The name location of compilator in file system.
        /// </value>
        public string Location { get; private set; }

        /// <summary>
        /// Gets the extension of source file.
        /// </summary>
        /// 
        /// <value>
        /// The extension of source file.
        /// </value>
        public string Extension { get; private set; }

        /// <summary>
        /// Gets the arguments of compilator to be run with.
        /// </summary>
        /// 
        /// <value>
        /// The arguments of compilator to be run with.
        /// </value>
        public string Arguments { get; private set; }

        /// <summary>
        /// Compiles provided source file into *.exe
        /// </summary>
        /// 
        /// <param name="sourceFilePath">
        /// Path of source to compile.
        /// </param>
        /// 
        /// <returns>
        /// <see cref="CompileResult"/> class that represents the compilation result.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="sourceFilePath"/> is null.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// If <paramref name="sourceFilePath"/> is incorrect.
        /// </exception>
        public CompileResult Compile(string sourceFilePath)
        {
            var compilingJava = (Name == Language.Java6);
            
            ProjectHelper.ValidateFileExists(sourceFilePath, "sourceFilePath");
            
            sourceFilePath = Path.GetFullPath(sourceFilePath);

            try
            {
                using (var process = new Process())
                {
                    //get shot pathes for compilers (some of them dont' work with long names)
                    var shortLocation = ToShortPathName(Path.GetDirectoryName(Location));
                    var shortSourceFilePath = compilingJava ? sourceFilePath : ToShortPathName(sourceFilePath);

                    //set compilation arguments
                    process.StartInfo.FileName = Location;
                    process.StartInfo.Arguments =
                        Arguments.Replace(CompilerDirectory, shortLocation).Replace(SourceFilePath, shortSourceFilePath);
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(sourceFilePath);

                    //set up process info nad start
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();

                    //get output and error info
                    var standardOutput = process.StandardOutput.ReadToEnd();
                    var standardError = process.StandardError.ReadToEnd();
                    
                    process.WaitForExit();

                    var compiled = compilingJava
                                       ? File.Exists(Path.ChangeExtension(sourceFilePath, "class"))
                                       : File.Exists(Path.ChangeExtension(sourceFilePath, "exe"));


                    //create and return the result of compilation
                    var result = new CompileResult(compiled, standardOutput, standardError);
                    
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new CompileException("Error occurred during compilation", e);
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="Compiler"/> class.
        /// </summary>
        /// 
        /// <param name="name">
        /// Name of compiler.
        /// </param>
        /// <param name="location">
        /// Location of compiler in file system.
        /// </param>
        /// <param name="arguments">
        /// Arguments of compilator to be run with.
        /// </param>
        /// <param name="extension">
        /// Extension of source file.
        /// </param>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If any argument is null.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// If <paramref name="location"/> is incorrect.
        /// </exception>
        public Compiler(Language name, string location, string arguments, string extension)
        {
            ProjectHelper.ValidateNotNull(name, "name");
            ProjectHelper.ValidateFileExists(location, "location");
            ProjectHelper.ValidateNotNull(arguments, "arguments");
            ProjectHelper.ValidateNotNull(extension, "extension");

            Name = name;
            Location = Path.GetFullPath(location);
            Arguments = arguments;
            Extension = extension;
        }

        /// <summary>
        /// Retrieves the short path form of the specified path.
        /// </summary>
        /// 
        /// <param name="lpszLongPath">
        /// The path string.
        /// </param>
        /// <param name="lpszShortPath">
        /// A pointer to a buffer to receive the null-terminated short form of the path that lpszLongPath specifies.
        /// </param>
        /// <param name="cchBuffer">
        /// The size of the buffer that lpszShortPath points to, in TCHARs.
        /// </param>
        /// 
        /// <returns>
        /// If the function succeeds, the return value is the length, in TCHARs, of the string that is copied to
        /// lpszShortPath, not including the terminating null character.
        /// If the lpszShortPath buffer is too small to contain the path, the return value is the size of the
        /// buffer, in TCHARs, that is required to hold the path and the terminating null character. 
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszLongPath,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszShortPath,
            uint cchBuffer);

        /// <summary>
        /// Wraps GetShortPathName API function
        /// </summary>
        /// <param name="longName">
        /// The long path string.
        /// </param>
        /// 
        /// <returns>
        /// The short path string.
        /// </returns>
        public static string ToShortPathName(string longName)
        {
            var shortNameBuffer = new StringBuilder(256);
            var bufferSize = (uint)shortNameBuffer.Capacity;
            var result = GetShortPathName(longName, shortNameBuffer, bufferSize);

            return shortNameBuffer.ToString();
        }

        public static Compiler VC6Compiler
        {
            get
            {

                return new Compiler(Language.VC6,
                                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\VC6\CL.EXE"),
                                    "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"",
                                    "cpp");
            }
        }

        public static Compiler VC8Compiler
        {
            get
            {
                return new Compiler(Language.VC8,
                                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\VC8\CL.EXE"),
                                    "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"",
                                    "cpp");
            }
        }

        public static Compiler DotNet2Compiler
        {
            get
            {
                var reference = "/reference:";
                var referenceList = new List<string> {"system.dll"};
                var allReferences = referenceList.Aggregate("", (current, systemReference) => current + (reference + systemReference + " "));

                return new Compiler(Language.CSharp2,
                                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CSharp2\csc.exe"),
                                    @"/t:exe " + allReferences + "$SourceFilePath$", "cs");
            }
        }

        public static Compiler DotNet3Compiler
        {
            get
            {
                var reference = "/reference:";
                var referenceList = new List<string>
                                                 {
                                                     "system.dll",
                                                     "System.Core.dll",
                                                     "System.Xml.Linq.dll",
                                                     "System.WorkflowServices.dll",
                                                     "System.Net.dll",
                                                     "System.Data.Linq.dll",
                                                     "System.Data.Entity.dll",
                                                     "System.AddIn.dll"
                                                 };
                var allReferences = referenceList.Aggregate("", (current, systemReference) => current + (reference + systemReference + " "));

                return new Compiler(Language.CSharp3,
                                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CSharp3\csc.exe"),
                                    @"/t:exe " + allReferences + "\"$SourceFilePath$\"", "cs");
            }
        }

        public static Compiler Java6Compiler
        {
            get
            {
                return new Compiler(Language.Java6,
                                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\Java6\bin\javac.exe"),
                                    "\"$SourceFilePath$\"", "java");
            }
        }

        public static Compiler Delphi7Compiler
        {
            get
            {
                return new Compiler(Language.Delphi7,
                                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\Delphi7\Dcc32.exe"),
                                    "-U\"$CompilerDirectory$\" $SourceFilePath$", "pas");
            }
        }
    }
}