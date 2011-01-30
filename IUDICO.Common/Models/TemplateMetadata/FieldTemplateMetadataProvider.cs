using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            foreach (FieldTemplateMetadata metadataForProperty in metadataForProperties)
            {
                var renderModeAttribute = metadataForProperty.Attributes.OfType<RenderModeAttribute>();

                if (renderModeAttribute.Any())
                {
                    var renderMode = renderModeAttribute.First().RenderMode;
                    
                    switch (renderMode)
                    {
                        case RenderMode.DisplayModeOnly:
                            metadataForProperty.ShowForDisplay = true;
                            metadataForProperty.ShowForEdit = false;
                            break;
                        case RenderMode.EditModeOnly:
                            metadataForProperty.ShowForDisplay = false;
                            metadataForProperty.ShowForEdit = true;
                            break;
                    }

                }

                var dropdownAttribute = metadataForProperty.Attributes.OfType<DropDownListAttribute>();

                if (dropdownAttribute.Any())
                {
                    var targetProperty = dropdownAttribute.First().TargetProperty;
                    var targetValue = containerType.InvokeMember(targetProperty, BindingFlags.GetProperty, null, container, null);
                    ((IEnumerable<SelectListItem>) metadataForProperty.Model).Where(s => s.Value == targetValue.ToString()).First()
                        .Selected = true;
                }

                var orderAttribute = metadataForProperty.Attributes.OfType<OrderAttribute>();

                returnValue.Add(orderAttribute.Any() ? orderAttribute.First().Order : key++, metadataForProperty);
            }

            return returnValue.Values.AsEnumerable();
        } 
    }
}
