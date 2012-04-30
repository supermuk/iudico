ALTER TABLE dbo.StudyResults ADD CONSTRAINT
	FK_StudyResults_User FOREIGN KEY
	(
	StudentRef
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION


