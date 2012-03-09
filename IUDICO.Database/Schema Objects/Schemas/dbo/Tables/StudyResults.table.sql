CREATE TABLE dbo.StudyResults
	(
	Id int NOT NULL IDENTITY (1, 1),
	StudentRef uniqueidentifier NOT NULL,
	CourseRef int NOT NULL,
	Point int NOT NULL
	)  ON [PRIMARY]


