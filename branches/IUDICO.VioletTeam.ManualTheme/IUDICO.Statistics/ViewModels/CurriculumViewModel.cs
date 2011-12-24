using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Statistics.ViewModels
{
    public class CurriculumViewModel
    {
        #region Constructors

        public CurriculumViewModel(int id, string name, DateTime created)
        {
            Id = id;
            Name = name;
            Created = created;
        }

        #endregion

        #region Public Properties

        public readonly int Id;

        public readonly string Name;

        public readonly DateTime Created;

        #endregion

    }
}