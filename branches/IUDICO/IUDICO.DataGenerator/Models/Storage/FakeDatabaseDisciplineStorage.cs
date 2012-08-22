using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.DisciplineManagement.Models.Storage;

namespace IUDICO.DataGenerator.Models.Storage
{
	public class FakeDatabaseDisciplineStorage : DatabaseDisciplineStorage
	{
		protected string _username;
      protected ILmsService lmsService;

		public FakeDatabaseDisciplineStorage(ILmsService lmsService, LinqLogger logger, string username): base(lmsService, logger)
		{
         this.lmsService = lmsService;
			this._username = username;
		}

		public FakeDatabaseDisciplineStorage(ILmsService lmsService, string username): this(lmsService, null, username)
		{
		}

		public override User GetCurrentUser()
		{
         return this.lmsService.FindService<IUserService>().GetUsers().SingleOrDefault(u => u.Username == this._username);
		}
	}
}