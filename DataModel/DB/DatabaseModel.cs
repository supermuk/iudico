using System.Data.Common;
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

    public partial class TblPermissions : IntKeyedDataObject, IIntKeyedDataObject
    {
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

    public partial class TblCourses : IntKeyedDataObject, IIntKeyedDataObject, INamedDataObject
    {
    }

    public partial class TblCurriculums : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblFiles : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblGroups : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblPages : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblQuestions : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblSampleBusinesObject : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblStages : IntKeyedDataObject, IIntKeyedDataObject, INamedDataObject
    {
    }

    public partial class TblThemes : IntKeyedDataObject, IIntKeyedDataObject, INamedDataObject
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
