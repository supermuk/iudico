ALTER TABLE dbo.ForecastingTreeNode ADD CONSTRAINT
	FK_ForecastingTreeNode_ForecastingTree FOREIGN KEY
	(
	TreeRef
	) REFERENCES dbo.ForecastingTree
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION


