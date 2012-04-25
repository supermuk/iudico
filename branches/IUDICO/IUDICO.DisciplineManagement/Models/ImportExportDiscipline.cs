using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using IUDICO.Common.Models.Shared;
using IUDICO.DisciplineManagement.Models.Storage;
using System.IO;
using System.Web;

namespace IUDICO.DisciplineManagement.Models
{
    public class ImportExportDiscipline
    {
        private readonly IDisciplineStorage storage;
        private readonly string basePath;

        public ImportExportDiscipline(IDisciplineStorage storage, string basePath)
        {
            this.storage = storage;
            this.basePath = basePath;
        }

        #region Private helpers

        private string GetFolderPath()
        {
            return Path.Combine(this.basePath, @"Data\Disciplines");
        }

        private static string GetTempFileName(int disciplineId)
        {
            return string.Format("{0}-{1}.disc", disciplineId, Guid.NewGuid());
        }

        private static string GetImportFileName(string fileName)
        {
            return string.Format("{0}-{1}.disc", fileName, Guid.NewGuid());
        }

        private void Serialize(string path, int disciplineId)
        {
            var discipline = this.storage.GetDiscipline(disciplineId);
            var disciplineDto = new DisciplineDto
            {
                Name = discipline.Name,
                Chapters = this.storage.GetChapters(item => item.DisciplineRef == discipline.Id)
                    .Select(chapter => new ChapterDto
                    {
                        Name = chapter.Name,
                        Topics = this.storage.GetTopics(item => item.ChapterRef == chapter.Id)
                            .OrderBy(item => item.SortOrder)
                            .Select(topic => new TopicDto
                                {
                                    Name = topic.Name,
                                    TestCourseRef = topic.TestCourseRef,
                                    TestTopicTypeRef = topic.TestTopicTypeRef,
                                    TheoryCourseRef = topic.TheoryCourseRef,
                                    TheoryTopicTypeRef = topic.TheoryTopicTypeRef
                                }).ToList()
                    }).ToList()
            };

            Stream writer = new FileStream(path, FileMode.Create);
            var serializer = new XmlSerializer(disciplineDto.GetType());
            serializer.Serialize(writer, disciplineDto);
            writer.Close();
        }

        private void Deserialize(string path)
        {
            Stream reader = new FileStream(path, FileMode.Open);
            var serializer = new XmlSerializer(typeof(DisciplineDto));
            var disciplineDto = (DisciplineDto)serializer.Deserialize(reader);

            var disciplineId = this.storage.AddDiscipline(
                new Discipline
                {
                    Name = disciplineDto.Name,
                    Owner = this.storage.GetCurrentUser().Username
                });

            foreach (var chapterDto in disciplineDto.Chapters)
            {
                var chapterId = this.storage.AddChapter(
                    new Chapter
                    {
                        Name = chapterDto.Name,
                        DisciplineRef = disciplineId
                    });

                foreach (var topicDto in chapterDto.Topics)
                {
                    this.storage.AddTopic(
                        new Topic
                        {
                            Name = topicDto.Name,
                            ChapterRef = chapterId,
                            TestCourseRef = topicDto.TestCourseRef,
                            TestTopicTypeRef = topicDto.TestTopicTypeRef,
                            TheoryCourseRef = topicDto.TheoryCourseRef,
                            TheoryTopicTypeRef = topicDto.TheoryTopicTypeRef
                        });
                }
            }
        }

        #endregion

        #region Public methods

        public string GetFileName(int disciplineId)
        {
            return string.Format("{0}.disc", this.storage.GetDiscipline(disciplineId).Name);
        }

        public string Export(int disciplineId)
        {
            var path = Path.Combine(this.GetFolderPath(), GetTempFileName(disciplineId));
            Directory.CreateDirectory(this.GetFolderPath());
            this.Serialize(path, disciplineId);
            return path;
        }

        public void Import(HttpPostedFileBase file)
        {
            var fileName = GetImportFileName(Path.GetFileNameWithoutExtension(file.FileName));
            var path = Path.Combine(this.GetFolderPath(), fileName);
            Directory.CreateDirectory(this.GetFolderPath());
            file.SaveAs(path);
            this.Deserialize(path);
        }

        #endregion
    }

    [Serializable]
    public class DisciplineDto
    {
        public string Name { get; set; }
        public List<ChapterDto> Chapters { get; set; }
    }

    [Serializable]
    public class ChapterDto
    {
        public string Name { get; set; }
        public List<TopicDto> Topics { get; set; }
    }

    [Serializable]
    public class TopicDto
    {
        public string Name { get; set; }
        public int? TestCourseRef { get; set; }
        public int? TestTopicTypeRef { get; set; }
        public int? TheoryCourseRef { get; set; }
        public int? TheoryTopicTypeRef { get; set; }
    }
}