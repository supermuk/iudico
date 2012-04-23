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
        private readonly IDisciplineStorage _Storage;
        private readonly string _BasePath;

        public ImportExportDiscipline(IDisciplineStorage storage, string basePath)
        {
            _Storage = storage;
            _BasePath = basePath;
        }

        #region Private helpers

        private string GetFolderPath()
        {
            return Path.Combine(_BasePath, @"Data\Disciplines");
        }

        private static string GetTempFileName(int disciplineId)
        {
            return String.Format("{0}-{1}.disc", disciplineId, Guid.NewGuid());
        }

        private static string GetImportFileName(string fileName)
        {
            return String.Format("{0}-{1}.disc", fileName, Guid.NewGuid());
        }

        private void Serialize(string path, int disciplineId)
        {
            var discipline = _Storage.GetDiscipline(disciplineId);
            var disciplineDto = new DisciplineDTO
            {
                Name = discipline.Name,
                Chapters = _Storage.GetChapters(item => item.DisciplineRef == discipline.Id)
                    .Select(chapter => new ChapterDTO
                    {
                        Name = chapter.Name,
                        Topics = _Storage.GetTopics(item => item.ChapterRef == chapter.Id)
                            .OrderBy(item => item.SortOrder)
                            .Select(topic => new TopicDTO
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
            var serializer = new XmlSerializer(typeof(DisciplineDTO));
            var disciplineDto = (DisciplineDTO)serializer.Deserialize(reader);

            var disciplineId = _Storage.AddDiscipline(
                new Discipline
                {
                    Name = disciplineDto.Name,
                    Owner = _Storage.GetCurrentUser().Username
                });

            foreach (var chapterDto in disciplineDto.Chapters)
            {
                var chapterId = _Storage.AddChapter(
                    new Chapter
                    {
                        Name = chapterDto.Name,
                        DisciplineRef = disciplineId
                    });

                foreach (var topicDto in chapterDto.Topics)
                {
                    _Storage.AddTopic(
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
            return String.Format("{0}.disc", _Storage.GetDiscipline(disciplineId).Name);
        }

        public string Export(int disciplineId)
        {
            var path = Path.Combine(GetFolderPath(), GetTempFileName(disciplineId));
            Directory.CreateDirectory(GetFolderPath());
            Serialize(path, disciplineId);
            return path;
        }

        public void Import(HttpPostedFileBase file)
        {
            var fileName = GetImportFileName(Path.GetFileNameWithoutExtension(file.FileName));
            var path = Path.Combine(GetFolderPath(), fileName);
            Directory.CreateDirectory(GetFolderPath());
            file.SaveAs(path);
            Deserialize(path);
        }

        #endregion
    }

    [Serializable]
    public class DisciplineDTO
    {
        public string Name { get; set; }
        public List<ChapterDTO> Chapters { get; set; }
    }

    [Serializable]
    public class ChapterDTO
    {
        public string Name { get; set; }
        public List<TopicDTO> Topics { get; set; }
    }

    [Serializable]
    public class TopicDTO
    {
        public string Name { get; set; }
        public int? TestCourseRef { get; set; }
        public int? TestTopicTypeRef { get; set; }
        public int? TheoryCourseRef { get; set; }
        public int? TheoryTopicTypeRef { get; set; }
    }
}