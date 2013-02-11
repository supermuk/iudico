using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.DataGenerator.Models.Storage
{
	public class DemoStorage: IDemoStorage
	{
		private List<User> students = new List<User>
		{
			new User 
				{
					Username = "IvanOpenko", 
					Password="ivan", 
					Email="ivan.openko@mail.com", 
					OpenId="ivan.openko@mail.com", 
					Name="Іван Опенько", 
				},
			new User 
				{
					Username = "MariyaLukovych", 
					Password="mariya", 
					Email="mariya.lukovych@mail.com", 
					OpenId="mariya.lukovych@mail.com", 
					Name="Марія Ликович", 
				},
			new User 
				{
					Username = "PetroIvanciv", 
					Password="petro", 
					Email="petro.ivanciv@mail.com", 
					OpenId="petro.ivanciv@mail.com", 
					Name="Петро Іванців", 
				},
			new User 
				{
					Username = "AndriyHrabarenko", 
					Password="andriy", 
					Email="andriy.hrabarenko@mail.com", 
					OpenId="andriy.hrabarenko@mail.com", 
					Name="Андрій Грабаренко", 
				},
			new User 
				{
					Username = "LilyaNevmurenko", 
					Password="lilya", 
					Email="lilya.nevmurenko@mail.com", 
					OpenId="lilya.nevmurenko@mail.com", 
					Name="Ліля Невмиренко", 
				},
			};


		private List<User> teachers = new List<User>
		{
			new User 
				{
					Username = "OlehVukladachenko", 
					Password="oleh", 
					Email="oleh.vukladachenko@mail.com", 
					OpenId="oleh.vukladachenko@mail.com", 
					Name="Олег Викладаченко", 
				},
			new User 
				{
					Username = "prof", 
					Password="prof", 
					Email="prof@mail.com", 
					OpenId="prof@mail.com", 
					Name="prof", 
				},
			new User 
				{
					Username = "prof2", 
					Password="prof2", 
					Email="prof2@mail.com", 
					OpenId="prof2@mail.com", 
					Name="prof2", 
				},
         new User
            {
               Username = "prof3", 
					Password="prof3", 
					Email="prof3@mail.com", 
					OpenId="prof3@mail.com", 
					Name="prof3",
            }
			};

		private List<User> courseCreators = new List<User>
		{
			new User 
				{
					Username = "TarasKursostvorenko", 
					Password="taras", 
					Email="taras.kursostvorenko@mail.com", 
					OpenId="taras.kursostvorenko@mail.com", 
					Name="Тарас Курсостворенко", 
				},
		};


		private List<User> administrators = new List<User>
		{
			new User 
				{
					Username = "NazarAdminenko", 
					Password="nazar", 
					Email="nazar.adminenko@mail.com", 
					OpenId="nazar.adminenko@mail.com", 
					Name="Назар Адміненко", 
				},
		};


		
		#region IDemoStorage Members

		public IEnumerable<User> GetStudents()
		{
			return this.students.AsEnumerable();
		}

		public IEnumerable<User> GetTeachers()
		{
			return this.teachers.AsEnumerable();
		}

		public IEnumerable<User> GetCourseCreators()
		{
			return this.courseCreators.AsEnumerable();
		}

		public IEnumerable<User> GetAdministrators()
		{
			return this.administrators.AsEnumerable();
		}

		#endregion
	}
}