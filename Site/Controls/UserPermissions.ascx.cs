using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;
using IUDICO.DataModel;
using System.Linq;

namespace IUDICO.Web.Controls
{
    public partial class UserPermissions : UserControl
    {
        public DB_OBJECT_TYPE? ObjectType { get; set; }

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

//        protected override object SaveViewState()
//        {
//            return new Pair(base.SaveViewState(), _View != null ? _View.SaveViewStateData() : null);
//        }
//
//        protected override void LoadControlState(object savedState)
//        {
//            var p = (Pair)savedState;
//            base.LoadViewState(p.First);
//            if (p.Second != null)
//            {
//                (_View = new DynamicClassView()).LoadViewStateData(p.Second);
//            }
//        }

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
                var ids = new List<int>(permissions.Select(p => (int) refProp.GetValue(p, null)).Distinct());
                var objs =
                    new DataObjectDictionary(
                        (IEnumerable)
                        DatabaseModel.LOAD_LIST_METHOD.MakeGenericMethod(new[] {ot.GetSecurityAtr().RuntimeClass}).
                            Invoke(ServerModel.DB, new[] {ids}));

                var ops = PermissionsManager.GetPossibleOperations(ot);

                var view = new DynamicClassView(ops.Count);
                view.DefineProperty("Name", typeof (string));
                view.DefineProperty("Date From", typeof (DateTime), false);
                view.DefineProperty("Date To", typeof (DateTime), false);
                foreach (var op in ops)
                {
                    view.DefineProperty(op.Name, typeof (bool));
                }

                foreach (var p in permissions)
                {
                    IIntKeyedDataObject obj = objs[(int) refProp.GetValue(p, null)];
                    var operations = PermissionsManager.GetOperationsForObject(ot, ui, obj.ID, now);

                    var d = view.Add();

                    d.AddProperty("Name", ((INamedDataObject) obj).Name);
                    d.AddProperty("Date From", p.DateSince);
                    d.AddProperty("Date To", p.DateTill);
                    foreach (var op in ops)
                    {
                        d.AddProperty(op.Name, operations.Contains(op.ID));
                    }
                    view.Add(d);
                }

                return view;
            }
            else
            {
                return null;
            }
        }

        private DynamicClassView _View;
    }
}