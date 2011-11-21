ALTER TABLE dbo.ForecastingTreeGroups ADD CONSTRAINT
	FK_ForecastingTreeGroups_Group FOREIGN KEY
	(
	GroupRef
	) REFERENCES dbo.[Group]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION


