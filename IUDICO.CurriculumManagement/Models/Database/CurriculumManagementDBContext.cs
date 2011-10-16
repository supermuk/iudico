using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models.Database
{
    public class CurriculumManagementDBContext : DbContext
    {
        public CurriculumManagementDBContext() : base(IUDICO.CurriculumManagement.Properties.Settings.Default.ConnectionString) { }

        public DbSet<Curriculum> Curriculums { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<ThemeType> ThemeTypes { get; set; }
        public DbSet<CurriculumAssignment> CurriculumAssignments { get; set; }
        public DbSet<ThemeAssignment> ThemeAssignments { get; set; }
        public DbSet<Timeline> Timelines { get; set; }
    }
}