CREATE TABLE fxGroupOperations
(
	ID int IDENTITY(1, 1) NOT NULL,
	Name nvarchar(50) NOT NULL,
	[Description] nvarchar(max) NULL,
	CanBeDelegated bit NOT NULL
	
	CONSTRAINT PK_fxGroupOperations PRIMARY KEY(ID),
	CONSTRAINT UK_fxGroupOperations_Name UNIQUE(Name)
)
GO

INSERT INTO fxGroupOperations (Name, CanBeDelegated) 
	SELECT 'View', 1 UNION ALL
	SELECT 'Rename', 1 UNION ALL
	SELECT 'ChangeMembers', 1
GO

EXEC sp_rename 
	@objname = 'tblPermissions.UserRef',
	@newname = 'OwnerUserRef',
	@objtype = 'COLUMN'

EXEC sp_rename 
	@objname = 'tblPermissions.GroupRef',
	@newname = 'OwnerGroupRef',
	@objtype = 'COLUMN'
	
EXEC sp_rename
	'FK_Permissions_Groups',
	'FK_Permissions_OwnerGroup'
	
EXEC sp_rename
	@objname = 'FK_Permissions_Users',
	@newname = 'FK_Permissions_OwnerUser'	
	
GO

ALTER TABLE tblPermissions
	ADD GroupRef int 
		CONSTRAINT FK_Permissions_Groups 
		FOREIGN KEY (GroupRef) 
		REFERENCES tblGroups(ID)
	
ALTER TABLE tblPermissions
	ADD GroupOperationRef int
		CONSTRAINT FK_Permissions_GroupOperations
		FOREIGN KEY (GroupOperationRef)
		REFERENCES fxGroupOperations(ID)
GO