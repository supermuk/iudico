EXEC sp_rename 
	@objname = 'tblUsers.Name',
	@newname = 'FirstName',
	@objtype = 'COLUMN'
	
DELETE tblUsers -- Has no any other option

ALTER TABLE tblUsers ADD LastName nvarchar(50) NOT NULL
ALTER TABLE tblUsers ADD [Login] nvarchar(32) NOT NULL
ALTER TABLE tblUsers ADD PasswordHash char(32) NOT NULL
ALTER TABLE tblUsers ADD [Email] char(50) NOT NULL
GO

ALTER TABLE tblUsers ADD CONSTRAINT UK_EMAIL UNIQUE ([Email])
ALTER TABLE tblUsers ADD CONSTRAINT UK_Login UNIQUE ([Login])
GO

CREATE TABLE relUserRoles
(
	UserID INT NOT NULL,
	RoleID INT NOT NULL
)
GO

ALTER TABLE relUserRoles ADD CONSTRAINT FK_USER_ID FOREIGN KEY (UserID)
	REFERENCES tblUsers(ID)
ALTER TABLE relUserRoles ADD CONSTRAINT FK_ROLE_ID FOREIGN KEY (RoleID)
	REFERENCES fxRoles(ID)
ALTER TABLE relUserRoles ADD CONSTRAINT PK_USER_ROLE PRIMARY KEY (UserID, RoleID)
