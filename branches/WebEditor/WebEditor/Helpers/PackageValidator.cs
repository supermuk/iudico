using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.LearningComponents;

namespace WebEditor.Helpers
{
    public static class PackageValidator
    {
        public static string Validate(string path)
        {

            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            PackageReader reader = PackageReader.Create(stream);

            PackageValidatorSettings settings = new PackageValidatorSettings(
                ValidationBehavior.LogWarning,
                ValidationBehavior.LogWarning,
                ValidationBehavior.LogError,
                ValidationBehavior.LogWarning
                );
            ValidationResults results;
            try
            {
                results = Microsoft.LearningComponents.PackageValidator.Validate(reader, settings);
            }
            catch (InvalidPackageException ex)
            {
                return String.Format("Package is invalid.<br>{0}", ex.Message);
            }
            string message = "";
            foreach (ValidationResult result in results.Results)
            {
                if (result.IsError)
                {
                    message += String.Format("MLC Error: {0}<br>", result.Message);
                }
                else
                {
                    message += String.Format("SCORM Warning: {0}<br>", result.Message);
                }
            }
            if (message == "")
            {
                message = "Package is valid.";
            }
            return message;
        }
    }
}