using System;
using System.Web.UI;

namespace IUDICO.DataModel
{
    public class PageParserFilter : System.Web.UI.PageParserFilter
    {
        public override bool AllowBaseType(Type baseType)
        {
            return true;
//            return baseType == null ||
//                baseType.IsSubclassOf(typeof (ControlledPage<>)) || 
//                baseType == typeof(MasterPageBase) ||
//                baseType == typeof(HttpApplication);
        }

        public override CompilationMode GetCompilationMode(CompilationMode current)
        {
            return CompilationMode.Auto;
        }

        public override bool AllowVirtualReference(string referenceVirtualPath, VirtualReferenceType referenceType)
        {
            return true;
        }

        public override bool AllowCode
        {
            get
            {
                return true;
            }
        }

        public override int NumberOfControlsAllowed
        {
            get
            {
                return -1;
            }
        }

        public override bool AllowControl(Type controlType, ControlBuilder builder)
        {
            return true;
        }


        public override int NumberOfDirectDependenciesAllowed
        {
            get
            {
                return -1;
            }
        }

        public override bool AllowServerSideInclude(string includeVirtualPath)
        {
            return false;
        }

        public override int TotalNumberOfDependenciesAllowed
        {
            get
            {
                return -1;
            }
        }
    }
}
