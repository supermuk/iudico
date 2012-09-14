using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.UserManagement.Models.Storage;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.Common.Models.Shared;

namespace IUDICO.DataGenerator.Models.Generators
{
   public static class CurriculumGenerator
   {
      public static void PascalCurriculum(ICurriculumStorage curriculumStorage, IDisciplineStorage disciplineStorage, IUserStorage userStorage)
      {
         var curriculum = new Curriculum
                                       {
                                          UserGroupRef = userStorage.GetGroups().FirstOrDefault(g => g.Name == "Демонстраційна група").Id,
                                          DisciplineRef = disciplineStorage.GetDisciplines().FirstOrDefault(d => d.Name == "Pascal" && d.Owner == "OlehVukladachenko").Id,
                                          StartDate = DateTime.Now,
                                          EndDate = DateTime.Now + new TimeSpan(365, 0, 0, 0, 0)
                                       };

         if (curriculumStorage.GetCurriculums().Where(c => c.DisciplineRef == curriculum.DisciplineRef && c.UserGroupRef == curriculum.UserGroupRef).Count() > 0)
         {
            return;
         }
         curriculumStorage.AddCurriculum(curriculum);

         foreach (var chapter in curriculumStorage.GetCurriculum(curriculum.Id).CurriculumChapters)
         {
            chapter.StartDate = DateTime.Now;
            chapter.EndDate = DateTime.Now + new TimeSpan(364, 0, 0, 0, 0);
            curriculumStorage.UpdateCurriculumChapter(chapter);

            foreach (var topic in chapter.CurriculumChapterTopics)
            {
               topic.TestStartDate = DateTime.Now;
               topic.TestEndDate = DateTime.Now + new TimeSpan(363, 0, 0, 0, 0);
               topic.TheoryStartDate = DateTime.Now;
               topic.TheoryEndDate = DateTime.Now + new TimeSpan(363, 0, 0, 0, 0);

               curriculumStorage.UpdateCurriculumChapterTopic(topic);
            }
         }
      }

      public static void CurriculumForSeleniumTestingSystem(ICurriculumStorage curriculumStorage, IDisciplineStorage disciplineStorage, IUserStorage userStorage)
      {
         var curriculums = new List<Curriculum>
                              {
                                 new Curriculum
                                    {
                                       UserGroupRef =
                                          userStorage.GetGroups().FirstOrDefault(
                                             g => g.Name == "Selenium testing system group").Id,
                                       DisciplineRef =
                                          disciplineStorage.GetDisciplines().FirstOrDefault(
                                             d => d.Name == "Testing discipline" && d.Owner == "SeleniumTeacher").Id,
                                       StartDate = DateTime.Now,
                                       EndDate = DateTime.Now + new TimeSpan(365, 0, 0, 0, 0)
                                    },
                                 //new Curriculum
                                 //   {
                                 //      UserGroupRef =
                                 //         userStorage.GetGroups().FirstOrDefault(
                                 //            g => g.Name == "Disc timeline").Id,
                                 //      DisciplineRef =
                                 //         disciplineStorage.GetDisciplines().FirstOrDefault(
                                 //            d => d.Name == "Testing discipline" && d.Owner == "SeleniumTeacher").Id,
                                 //      StartDate = DateTime.Now,
                                 //      EndDate = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1)
                                 //   },
                                 //new Curriculum
                                 //   {
                                 //      UserGroupRef =
                                 //         userStorage.GetGroups().FirstOrDefault(
                                 //            g => g.Name == "Chapter timeline").Id,
                                 //      DisciplineRef =
                                 //         disciplineStorage.GetDisciplines().FirstOrDefault(
                                 //            d => d.Name == "Testing discipline" && d.Owner == "SeleniumTeacher").Id,
                                 //      StartDate = DateTime.Now,
                                 //      EndDate = DateTime.Now + new TimeSpan(36500, 0, 0, 0, 0)
                                 //   },
                                 //new Curriculum
                                 //   {
                                 //      UserGroupRef =
                                 //         userStorage.GetGroups().FirstOrDefault(
                                 //            g => g.Name == "Topic timeline").Id,
                                 //      DisciplineRef =
                                 //         disciplineStorage.GetDisciplines().FirstOrDefault(
                                 //            d => d.Name == "Testing discipline" && d.Owner == "SeleniumTeacher").Id,
                                 //      StartDate = DateTime.Now,
                                 //      EndDate = DateTime.Now + new TimeSpan(36500, 0, 0, 0, 0)
                                 //   }
                              };

         foreach (var curriculum in curriculums)
         {

            if (
               curriculumStorage.GetCurriculums().Where(
                  c => c.DisciplineRef == curriculum.DisciplineRef && c.UserGroupRef == curriculum.UserGroupRef).Count() >
               0)
            {
               continue;
            }
            curriculumStorage.AddCurriculum(curriculum);

            foreach (var chapter in curriculumStorage.GetCurriculum(curriculum.Id).CurriculumChapters)
            {
               chapter.StartDate = DateTime.Now;
               chapter.EndDate = DateTime.Now + new TimeSpan(364, 0, 0, 0, 0);
               if (userStorage.GetGroup(curriculum.UserGroupRef).Name == "Chapter timeline")
               {
                  chapter.EndDate = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1);
               }
               curriculumStorage.UpdateCurriculumChapter(chapter);

               foreach (var topic in chapter.CurriculumChapterTopics)
               {
                  topic.TestStartDate = DateTime.Now;
                  topic.TestEndDate = DateTime.Now + new TimeSpan(363, 0, 0, 0, 0);
                  topic.TheoryStartDate = DateTime.Now;
                  topic.TheoryEndDate = DateTime.Now + new TimeSpan(363, 0, 0, 0, 0);
                  if (userStorage.GetGroup(curriculum.UserGroupRef).Name == "Topic timeline")
                  {
                     topic.TestEndDate = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1);
                     topic.TheoryEndDate = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1);
                  }
                  curriculumStorage.UpdateCurriculumChapterTopic(topic);
               }
            }
         }
      }
   }
}