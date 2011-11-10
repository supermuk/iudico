ALTER TABLE dbo.ForecastingResults ADD CONSTRAINT
	FK_ForecastingResults_ForecastingTree FOREIGN KEY
	(
	TreeRef
	) REFERENCES dbo.ForecastingTree
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION


