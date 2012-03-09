ALTER TABLE dbo.StudyResults ADD CONSTRAINT
	FK_StudyResults_Courses FOREIGN KEY
	(
	CourseRef
	) REFERENCES dbo.Courses
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION


