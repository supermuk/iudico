using System.Collections.Generic;
using System.Linq;
using IUDICO.DataModel.Common;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class HomeController : ControllerBase
    {
        public void Test1()
        {
        }

        public void Test2()
        {
        }

        public void Test3()
        {
            __PersistedInt.Value++;
        }

        public void TestPersistedList()
        {
            if (PersistedList == null)
            {
                PersistedList = new List<int>();
            }
            PersistedList.Add(PersistedList.Count);
            __PersistedCollection.Value = string.Join(", ", new List<string>(PersistedList.Select(i => i.ToString())).ToArray());
        }

        public IValue<string> PersistedCollection { get { return __PersistedCollection; } }

        public IValue<int> PersistedInt { get { return __PersistedInt; } }

        [PersistantField]
        private readonly IVariable<int> __PersistedInt = 0.AsVariable();

        [PersistantField]
        private List<int> PersistedList;

        private readonly IVariable<string> __PersistedCollection = string.Empty.AsVariable();
    }
}
