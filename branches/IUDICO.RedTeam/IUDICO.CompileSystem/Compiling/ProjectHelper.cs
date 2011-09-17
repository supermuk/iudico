using System;
using System.IO;

namespace CompileSystem.Compiling
{
    /// <summary>
    /// Provides helper methods for commonly needed functionalities in this component.
    /// </summary>
    internal static class ProjectHelper
    {
        /// <summary>
        /// Validates an object for null.
        /// </summary>
        /// 
        /// <param name="value">
        /// Object reference to check.
        /// </param>
        /// <param name="paramName">
        /// Name of parameter.
        /// </param>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If value is null.
        /// </exception>
        internal static void ValidateNotNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, paramName + " can not be null.");
            }
        }

        /// <summary>
        /// Validates if provided file exists.
        /// </summary>
        /// 
        /// <param name="path">
        /// File path to check.
        /// </param>
        /// <param name="pathName">
        /// Name of path variable.
        /// </param>
        /// 
        /// <exception cref="FileNotFoundException">
        /// If file doesn't exists.
        /// </exception>
        internal static void ValidateFileExists(string path, string pathName)
        {
            ValidateNotNull(path, pathName);
            
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Can't find file", path);
            }
        }
    }
}
