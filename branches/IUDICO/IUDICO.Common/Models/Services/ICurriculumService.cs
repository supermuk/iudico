using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.Common.Models.Services
{
    public interface ICurriculumService : IService
    {
        IList<Curriculum> GetCurriculumsByGroupId(int groupId);

        /// <summary>
        /// Gets the topic descriptions owned by user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IList<TopicDescription> GetTopicDescriptions(User user);
    }
}
