ALTER TABLE dbo.ForecastingTreeNode ADD CONSTRAINT
	FK_ForecastingTreeNode_ForecastingTreeNode FOREIGN KEY
	(
	ParentRef
	) REFERENCES dbo.ForecastingTreeNode
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION


