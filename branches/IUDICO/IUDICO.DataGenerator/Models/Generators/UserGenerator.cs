using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models.Storage;
using IUDICO.DataGenerator.Models.Storage;

namespace IUDICO.DataGenerator.Models.Generators
{
	public static class UserGenerator
	{
		public static void Generate( IWindsorContainer container )
		{
			var userStorage = container.Resolve<IUserStorage>();
			var demoStorage = container.Resolve<IDemoStorage>();

			Group demoGroup = new Group { Name = "Демонстраційна група"};

			if(!userStorage.GetGroups().Select(g=>g.Name).Contains(demoGroup.Name))
			{
				userStorage.CreateGroup(demoGroup);
			}

			var students = demoStorage.GetStudents().Select(s => s)
										.Where(s => !userStorage.GetUsers().Select(u => u.Username).Contains(s.Username));
			foreach (var stud in students)
			{
				userStorage.CreateUser(stud);
				userStorage.AddUserToRole(Role.Student, stud);
				userStorage.AddUserToGroup(demoGroup, stud);
			}

			var teachers = demoStorage.GetTeachers().Select(t => t)
										.Where(t => !userStorage.GetUsers().Select(u => u.Username).Contains(t.Username));
			foreach (var t in teachers)
			{
				userStorage.CreateUser(t);
				userStorage.AddUserToRole(Role.Teacher, t);
			}

			var courseCreators = demoStorage.GetCourseCreators().Select(cc => cc)
												.Where(cc => !userStorage.GetUsers().Select(u => u.Username).Contains(cc.Username));
			foreach (var cc in courseCreators)
			{
				userStorage.CreateUser(cc);
				userStorage.AddUserToRole(Role.CourseCreator, cc);
			}

			var admins = demoStorage.GetAdministrators().Select(a => a)
										.Where(a => !userStorage.GetUsers().Select(u => u.Username).Contains(a.Username));
			foreach (var admin in admins)
			{
				userStorage.CreateUser(admin);
				userStorage.AddUserToRole(Role.Admin, admin);
			}
		}
	}
}