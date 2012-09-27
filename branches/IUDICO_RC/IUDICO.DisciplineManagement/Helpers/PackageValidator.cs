using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.LearningComponents;

namespace IUDICO.DisciplineManagement.Helpers
{
    public static class PackageValidator
    {
        public static List<string> Validate(string path)
        {
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var reader = PackageReader.Create(stream);

            var settings = new PackageValidatorSettings(
                ValidationBehavior.LogWarning,
                ValidationBehavior.LogWarning,
                ValidationBehavior.LogError,
                ValidationBehavior.LogWarning);

            ValidationResults results;

            var messages = new List<string>();

            try
            {
                results = Microsoft.LearningComponents.PackageValidator.Validate(reader, settings);
            }
            catch (InvalidPackageException ex)
            {
                messages.Add(string.Format("Package is invalid.{0}", ex.Message));

                return messages;
            }

            foreach (var result in results.Results)
            {
                if (result.IsError)
                {
                    messages.Add(string.Format("MLC Error: {0}", result.Message));
                }
                else
                {
                    messages.Add(string.Format("SCORM Warning: {0}", result.Message));
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