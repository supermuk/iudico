using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

namespace IUDICO.DisciplineManagement.Models.ViewDataClasses {
	public class ViewDisciplineModel {
		public Discipline Discipline { get; set; }
		public string Error { get; set; }
	}
}