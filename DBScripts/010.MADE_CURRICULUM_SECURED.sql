EXEC sp_rename 
	@objname = 'tblPermissions.CurriculumsOperationRef',
	@newname = 'CurriculumOperationRef',
	@objtype = 'COLUMN'
GO