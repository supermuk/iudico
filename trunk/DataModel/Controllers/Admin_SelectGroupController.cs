using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Controllers
{
    public enum SELECT_GROUP_OPERATION
    {
        INCLUDE
    }

    public class Admin_SelectGroupController : ControllerBase
    {
        public override void Initialize()
        {
            base.Initialize();
            User = ServerModel.DB.Load<TblUsers>(UserID);
        }

        public IList<TblGroups> SelectGroups()
        {
            return ServerModel.DB.Query<TblGroups>(new InCondition<int>(
                DataObject.Schema.ID,
                new SubSelectCondition<RelUserGroups>("GroupRef",
                    new CompareCondition<int>(
                        DataObject.Schema.UserRef,
                        new ValueCondition<int>(User.ID),
                        COMPARE_KIND.EQUAL
                    )
                ),
                IN_CONDITION_KIND.NOT_IN
            ));
        }

        [PersistantField]
        public TblUsers User;

        [ControllerParameter]
        public int UserID;

        [ControllerParameter]
        public SELECT_GROUP_OPERATION Operation;
    }

}
