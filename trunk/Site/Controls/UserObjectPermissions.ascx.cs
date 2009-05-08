using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;

public partial class UserObjectPermissions : UserControl
{
    public SECURED_OBJECT_TYPE? ObjectType { get; set; }

    public int? UserID { get; set; }

    public bool IsEmpty
    {
        get { return _View == null; }
    }

    public override void DataBind()
    {
        if (_View == null)
        {
            _View = GenerateView();
        }

        PermissionsGrid.DataSource = _View;
        PermissionsGrid.DataBind();
    }

    public void Refresh()
    {
        _View = null;
        DataBind();
    }

    private DynamicClassView GenerateView()
    {
        if (ObjectType == null || UserID == null)
        {
            throw new InvalidOperationException("Both ObjectType and UserID property must be specified");
        }
        var ot = ObjectType.Value;
        var ui = UserID.Value;
        var now = DateTime.Now;

        var permissions = ServerModel.DB.Load<TblPermissions>(PermissionsManager.GetPermissions(ot, ui, now, null));
        if (permissions.Count > 0)
        {
            Type pType = typeof (TblPermissions);
            var refProp = pType.GetProperty(ot.GetSecurityAtr().Name + "Ref");
            var ids = new List<int>(permissions.Select(p => refProp.GetValue(p, null)).NonNull().Cast<int>().Distinct());
            var objs =
                new DataObjectDictionary(
                    (IEnumerable)
                    DatabaseModel.LOAD_LIST_METHOD.MakeGenericMethod(new[] {ot.GetSecurityAtr().RuntimeClass}).
                        Invoke(ServerModel.DB, new[] {ids}));

            var possibleOps = PermissionsManager.GetPossibleOperations(ot);

            var view = new DynamicClassView(possibleOps.Count);
            view.DefineProperty("Name", typeof (string));
            view.DefineProperty("Date From", typeof (DateTime), false);
            view.DefineProperty("Date To", typeof (DateTime), false);
            foreach (var op in possibleOps)
            {
                view.DefineProperty(op.Name, typeof (bool));
            }

            foreach (ISecuredDataObject o in objs.Values)
            {
                var operations = PermissionsManager.GetOperationsForObject(ot, ui, o.ID, now);

                foreach (var op in operations)
                {
                    var d = view.Add();
                    foreach (var i in possibleOps)
                    {
                        d.AddProperty(i.Name, i.ID == op);
                    }
                    d.AddProperty("Name", o.Name);
                    // TODO: Implement datefrom and dateto retrieving
                    d.AddProperty("Date From", null);
                    d.AddProperty("Date To", null);

                    view.Add(d);
                }
            }

            return view;
        }
        return null;
    }

    [PersistantField]
    private DynamicClassView _View;
}