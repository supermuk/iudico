ALTER TABLE dbo.ForecastingTreeGroups ADD CONSTRAINT
	FK_ForecastingTreeGroups_ForecastingTree FOREIGN KEY
	(
	TreeRef
	) REFERENCES dbo.ForecastingTree
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION


