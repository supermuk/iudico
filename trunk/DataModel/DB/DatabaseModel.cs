using System.Data.Common;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.DB
{
    public partial class DatabaseModel
    {
        public DbConnection GetConnectionSafe()
        {
            return ServerModel.AcruireOpenedConnection();
        }
    }

    public partial class FxCourseOperations : FxDataObject, IFxDataObject { }

    public partial class FxThemeOperations : FxDataObject, IFxDataObject { }

    public partial class FxStageOperations : FxDataObject, IFxDataObject {}

    public partial class FxRoles : FxDataObject, IFxDataObject {}

    public partial class FxGroupOperations : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public readonly static FxGroupOperations View;
        [TableRecord]
        public readonly static FxGroupOperations Rename;
        [TableRecord]
        public readonly static FxGroupOperations ChangeMembers;
    }

    public partial class TblPermissions : IntKeyedDataObject, IIntKeyedDataObject
    {
        public DateTimeInterval WorkingInterval
        {
            get
            {
                return new DateTimeInterval(DateSince, DateTill);
            }
            set
            {
                DateSince = value.From;
                DateTill = value.To;
            }
        }

        public int? GetObjectID(SECURED_OBJECT_TYPE ot)
        {
            return (int?) GetType().GetProperty(ot.GetSecurityAtr().Name + "Ref").GetValue(this, null);
        }

        public void SetObjectID(SECURED_OBJECT_TYPE ot, int value)
        {
            GetType().GetProperty(ot.GetSecurityAtr().Name + "Ref").SetValue(this, value, null);
        }

        public int? GetOperationID(SECURED_OBJECT_TYPE ot)
        {
            return (int?)GetType().GetProperty(ot.GetSecurityAtr().Name + "OperationRef").GetValue(this, null);
        }

        public void SetOperationID(SECURED_OBJECT_TYPE ot, int value)
        {
            GetType().GetProperty(ot.GetSecurityAtr().Name + "OperationRef").SetValue(this, value, null);
        }
    }

    public partial class TblCompiledAnswers : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestions : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestionsData : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCourses : SecuredDataObject, ISecuredDataObject  
    {
    }

    public partial class TblCurriculums : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblFiles : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblGroups : SecuredDataObject, ISecuredDataObject { }

    public partial class TblPages : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblQuestions : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblSampleBusinesObject : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblStages : SecuredDataObject, ISecuredDataObject
    {
    }

    public partial class TblThemes : SecuredDataObject, ISecuredDataObject
    {
    }

    public partial class TblUserAnswers : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblUsers : IntKeyedDataObject, IIntKeyedDataObject { }

    [ManyToManyRelationship(typeof(TblUsers), typeof(FxRoles))]
    public partial class RelUserRoles : RelTable
    {   
    }

    [ManyToManyRelationship(typeof(TblUsers), typeof(TblGroups))]
    public partial class RelUserGroups : RelTable
    {
    }

    [DBEnum("fxRoles")]
    public enum FX_ROLE
    {
        STUDENT,
        LECTOR,
        TRAINER,
        ADMIN,
        SUPER_ADMIN
    }
}
