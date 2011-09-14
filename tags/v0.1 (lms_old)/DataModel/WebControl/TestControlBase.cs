using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IUDICO.DataModel.WebControl
{
    public abstract class TestControlBase : UserControl
    {
        public int QuestionId;

        protected abstract void ApplyStyles();
    }

    public abstract class TextBoxTestControlBase : TestControlBase
    {
        public string InnerText;
    }

    public abstract class ItemListTestControlBase : TestControlBase
    {
        private readonly List<ListItem> _items = new List<ListItem>();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<ListItem> Items
        {
            get { return _items; }
        }
    }
}