using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.Common.Models.TemplateMetadata
{
    public class FieldTemplateMetadata : DataAnnotationsModelMetadata
    {
        public FieldTemplateMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute, IEnumerable<Attribute> attributes)
            : base(provider, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute)
        {
            Attributes = new List<Attribute>(attributes);
        }

        public IList<Attribute> Attributes
        {
            get;
            private set;
        }
    }
}