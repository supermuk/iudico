using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.QualityTest
{
    public class SelectGroupsModel
    {
        private IEnumerable<Group> _AllowedGroups;
        private String _TeacheUserName;
        private String _DisciplineName;
        private String _TopicName;

        public SelectGroupsModel(ILmsService iLmsService, int selectTopicId, String teacherUserName, String disciplineName)
        {
            IEnumerable<Group> allowedGroups;
            Topic selectTopic;
            selectTopic = iLmsService.FindService<IDisciplineService>().GetTopic(selectTopicId);
            _TopicName = selectTopic.Name;
            _TeacheUserName = teacherUserName;
            _DisciplineName = disciplineName;
            allowedGroups = iLmsService.FindService<IDisciplineService>().GetGroupsAssignedToTopic(selectTopicId);
            //
            if (allowedGroups != null & allowedGroups.Count() != 0)
                _AllowedGroups = allowedGroups;
            else
                _AllowedGroups = null;            
        }
        public String GetDisciplineName()
        {
            return this._DisciplineName;
        }
        public String GetTeacherUserName()
        {
            return this._TeacheUserName;
        }
        public String GetTopicName()
        {
            return this._TopicName;
        }
        public bool NoData()
        {
            return _AllowedGroups == null;
        }
        public IEnumerable<Group> GetAllowedGroups()
        {
            return this._AllowedGroups;
        }
    }
}