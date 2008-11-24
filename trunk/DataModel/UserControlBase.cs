using System.Web.UI;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;

namespace IUDICO.DataModel
{
    public abstract class ControlledUserControl<TController> : UserControl
        where TController : ControllerBase, new()
    {
        protected TController Controller = new TController();

        protected virtual void BindController(TController c)
        {
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            BindController(Controller);
        }

        public override bool EnableViewState
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        protected override void LoadViewState(object savedState)
        {
            var p = PersistantStateMetaData.Get(typeof(TController));
            if (p.IsEmpty)
            {
                base.LoadViewState(savedState);
            }
            else
            {
                var d = (Pair)savedState;
                base.LoadViewState(d.First);
                p.LoadStateFor(Controller, d.Second);
            }
        }

        protected override object SaveViewState()
        {
            var r = base.SaveViewState();
            var p = PersistantStateMetaData.Get(typeof(TController));
            if (!p.IsEmpty)
            {
                r = new Pair(r, p.SaveStateFor(Controller));
            }
            return r;
        }
    }

    public class ControlledUserControl : ControlledUserControl<DefaultController>
    {
        protected override void LoadViewState(object savedState)
        {
            var p = PersistantStateMetaData.Get(GetType().BaseType);
            if (p.IsEmpty)
            {
                base.LoadViewState(savedState);
            }
            else
            {
                var d = (Pair)savedState;
                base.LoadViewState(d.First);
                p.LoadStateFor(this, d.Second);
            }
        }

        protected override object SaveViewState()
        {
            var r = base.SaveViewState();
            var p = PersistantStateMetaData.Get(GetType().BaseType);
            if (!p.IsEmpty)
            {
                r = new Pair(r, p.SaveStateFor(this));
            }
            return r;
        }
    }
}
