using System;
using System.Collections.Generic;

namespace IUDICO.Common.Models.Services
{
    public interface ICourseService : IService
    {
        #region Course methods

        IEnumerable<Course> GetCourses();
        Course GetCourse(int id);
        string Export(int id);

        #endregion

        #region Nodes methods

        IEnumerable<Node> GetNodes(int courseId);
        IEnumerable<Node> GetNodes(int courseId, int? parentId);
        Node GetNode(int id);
        String GetNodeContents(int id);

        #endregion
    }
}
