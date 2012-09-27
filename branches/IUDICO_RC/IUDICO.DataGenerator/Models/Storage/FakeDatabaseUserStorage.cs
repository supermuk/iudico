using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.UserManagement.Models.Storage;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.DataGenerator.Models.Storage
{
	public class FakeDatabaseUserStorage : DatabaseUserStorage
	{
		private string _username;

		public FakeDatabaseUserStorage(ILmsService lmsService, LinqLogger logger, string username): base(lmsService, logger)
		{
			this._username = username;
		}

		public FakeDatabaseUserStorage(ILmsService lmsService, string username) : this(lmsService, null, username)
		{
		}

		public override User GetCurrentUser()
		{
			return base.GetUser(this._username);
		}
	}
}