ALTER TABLE dbo.ForecastingResults ADD CONSTRAINT
	FK_ForecastingResults_User FOREIGN KEY
	(
	StudentRef
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION


