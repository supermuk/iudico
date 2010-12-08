using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.LearningComponents;

namespace IUDICO.CourseManagment.Helpers
{
    public static class PackageValidator
    {
        public static List<string> Validate(string path)
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
            List<string> messages = new List<string>();
            try
            {
                results = Microsoft.LearningComponents.PackageValidator.Validate(reader, settings);
            }
            catch (InvalidPackageException ex)
            {
                messages.Add(String.Format("Package is invalid.{0}", ex.Message));
                return messages;
            }
            foreach (ValidationResult result in results.Results)
            {
                if (result.IsError)
                {
                    messages.Add(String.Format("MLC Error: {0}", result.Message));
                }
                else
                {
                    messages.Add(String.Format("SCORM Warning: {0}", result.Message));
                }
            }
            if (messages.Count == 0)
            {
                messages.Add("Package is valid.");
            }
            return messages;
        }
    }
}