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

DELETE fxRoles
INSERT INTO fxRoles (Name)
	select 'STUDENT' union all
	select 'LECTOR' union all
	select 'TRAINER' union all
	select 'ADMIN' union all
	select 'SUPER_ADMIN'
	
INSERT INTO tblUsers 
	(FirstName, LastName, [Login], PasswordHash, Email)
	VALUES ('Volodymyr', 'Shtenovych', 'lex', 'B067B3D3054D8868C950E1946300A3F4', 'ShVolodya@gmail.com')

DECLARE @id int
SET @id = SCOPE_IDENTITY()

INSERT INTO relUserRoles (UserID, RoleID)
	SELECT UserID, rs.ID 
	FROM (SELECT @id UserID) UserID FULL OUTER JOIN fxRoles rs 
	ON 1 <> 2