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
        protected static int startOrder = 20000;

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
            var returnValue = new SortedDictionary<int, ModelMetadata>();
            
            var metadataForProperties = base.GetMetadataForProperties(container, containerType);

            foreach (FieldTemplateMetadata metadataForProperty in metadataForProperties)
            {
                this.ProcessDropDownAttribute(metadataForProperty, container, containerType);
                returnValue.Add(this.GetOrder(metadataForProperty), metadataForProperty);
            }

            return returnValue.Values.AsEnumerable();
        }

        protected void ProcessDropDownAttribute(FieldTemplateMetadata metadataForProperty, object container, Type containerType)
        {
            var dropdownAttribute = metadataForProperty.Attributes.OfType<DropDownListAttribute>();

            if (!dropdownAttribute.Any())
            {
                return;
            }

            var sourceProperty = dropdownAttribute.First().SourceProperty;
            var sourceList = (IEnumerable<SelectListItem>)containerType.InvokeMember(sourceProperty, BindingFlags.GetProperty, null, container, null);

            if (sourceList != null)
            {
                var selectedItem = sourceList.FirstOrDefault(i => i.Value == metadataForProperty.Model.ToString());

                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                }
            }

            dropdownAttribute.First().List = sourceList;
        }

        protected int GetOrder(FieldTemplateMetadata metadataForProperty)
        {
            var orderAttribute = metadataForProperty.Attributes.OfType<OrderAttribute>();

            return orderAttribute.Any() ? orderAttribute.First().Order : startOrder++;
        }
    }
}
