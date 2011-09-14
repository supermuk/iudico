using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.DB
{
    [ManyToManyRelationship(typeof(TblUser), typeof(FxRole))]
    public partial class RelUserRole : RelTable, IRelationshipTable { }

    public partial class FxRole : FxDataObject, IFxDataObject { }

    public partial class TblUser : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblNews : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblCategory : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblComment : IntKeyedDataObject, IIntKeyedDataObject {}
}
