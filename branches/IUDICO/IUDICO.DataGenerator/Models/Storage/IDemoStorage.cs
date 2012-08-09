using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

namespace IUDICO.DataGenerator.Models.Storage
{
	public interface IDemoStorage
	{


		#region DemoUsers 
		IEnumerable<User> GetStudents();
		IEnumerable<User> GetTeachers();
		IEnumerable<User> GetCourseCreators();
		IEnumerable<User> GetAdministrators();
		#endregion

	}
}