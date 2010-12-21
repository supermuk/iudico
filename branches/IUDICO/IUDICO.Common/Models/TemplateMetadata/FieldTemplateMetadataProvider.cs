using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.Common.Models.TemplateMetadata
{
    public class FieldTemplateMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var result = (DataAnnotationsModelMetadata)base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            return new FieldTemplateMetadata(this, containerType, modelAccessor, modelType, propertyName, attributes.OfType<DisplayColumnAttribute>().FirstOrDefault(), attributes)
            {
                TemplateHint = result.TemplateHint,
                HideSurroundingHtml = result.HideSurroundingHtml,
                DataTypeName = result.DataTypeName,
                IsReadOnly = result.IsReadOnly,
                NullDisplayText = result.NullDisplayText,
                DisplayFormatString = result.DisplayFormatString,
                ConvertEmptyStringToNull = result.ConvertEmptyStringToNull,
                EditFormatString = result.EditFormatString,
                ShowForDisplay = result.ShowForDisplay,
                ShowForEdit = result.ShowForEdit,
                DisplayName = result.DisplayName
            };
        }

        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            var key = 20000;
            var returnValue = new SortedDictionary<int, ModelMetadata>();
            
            var metadataForProperties = base.GetMetadataForProperties(container, containerType);

            foreach (var metadataForProperty in metadataForProperties)
            {
                var property = metadataForProperty.ContainerType.GetProperty(metadataForProperty.PropertyName);

                var propertyAttributes = property.GetCustomAttributes(typeof(OrderAttribute), true);

                if (propertyAttributes.Length > 0)
                {
                    var orderAttribute = propertyAttributes[0] as OrderAttribute;
                    returnValue.Add(orderAttribute.Order, metadataForProperty);
                }
                else
                {
                    returnValue.Add(key++, metadataForProperty);
                }
            }

            return returnValue.Values.AsEnumerable();
        } 
    }
}
