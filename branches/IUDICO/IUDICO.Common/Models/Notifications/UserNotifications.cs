using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Notifications
{
    /// <summary>
    /// Holds User/Group - related notification's name constants with description.
    /// </summary>
    public static class UserNotifications
    {
        /// <summary>
        /// 
        /// </summary>
        public const string UserLogin = "user/login";

        /// <summary>
        /// 
        /// </summary>
        public const string UserLogout = "user/logout";

        /// <summary>
        /// User Create Multiple notification is sent when multiple users has been created(added).
        /// <param name="user">User value represents created user object.</param>
        /// </summary>
        public const string UserCreateMultiple = "user/createmultiple";

        /// <summary>
        /// User Create notification is sent when user has been created(added).
        /// <param name="user">User value represents created user object.</param>
        /// </summary>
        public const string UserCreate = "user/create";

        /// <summary>
        /// User Edit notification is sent when user details has been modified (updated).
        /// <param name="user">User value represents edited user object.</param>
        /// </summary>
        public const string UserEdit = "user/edit";

        /// <summary>
        /// User Delete notification is sent when user has been deleted.
        /// <param name="user">User value represents deleted user object.</param>
        /// </summary>
        public const string UserDelete = "user/delete";

        /// <summary>
        /// Group Create notification is sent when group has been created(added).
        /// <param name="group">Group value represents created group object.</param>
        /// </summary>
        public const string GroupCreate = "group/create";

        /// <summary>
        /// Group Edit notification is sent when group details has been modified (updated).
        /// <param name="group">Group value represents edited group object.</param>
        /// </summary>
        public const string GroupEdit = "group/edit";

        /// <summary>
        /// Group Delete notification is sent when group has been deleted.
        /// <param name="group">Group value represents deleted group object.</param>
        /// </summary>
        public const string GroupDelete = "group/delete";

        /// <summary>
        /// Course Delete notification is sent when course has been deleted.
        /// <param name="course">Group value represents deleted group object.</param>
        /// </summary>
        public const string CourseDelete = "course/delete";
    }
}
