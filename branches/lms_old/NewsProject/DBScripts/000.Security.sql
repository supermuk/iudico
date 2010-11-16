CREATE TABLE [fxRole]
(
	ID int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(32) NOT NULL UNIQUE,
	Description nvarchar(max) NULL
)
GO

INSERT INTO [fxRole] (Name) 
	select 'Guest' union all
	select 'User' union all
	select 'Editor' union all
	select 'Admin'
	
	
CREATE TABLE [tblUser]
(
	ID int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	FirstName nvarchar(32) NOT NULL,
	SecondName nvarchar(32) NOT NULL,
	Email nvarchar(32) NOT NULL UNIQUE,
	PasswordHash char(32) NOT NULL
)
GO

CREATE TABLE [relUserRole]
(
	UserRef int NOT NULL,
	RoleRef int NOT NULL
)
GO

ALTER TABLE relUserRole ADD CONSTRAINT relUserRole_UserRef 
	FOREIGN KEY (UserRef)
	REFERENCES tblUser(ID)
	
ALTER TABLE relUserRole ADD CONSTRAINT relUserRole_RoleRef
	FOREIGN KEY (RoleRef)
	REFERENCES fxRole(ID)
	
DECLARE @uID int

INSERT INTO tblUser (FirstName, SecondName, Email, PasswordHash) VALUES
('Volodymyr', 'Shtenovych', 'ShVolodya@gmail.com', 'B067B3D3054D8868C950E1946300A3F4')
SET @uID = SCOPE_IDENTITY()
	
INSERT INTO relUserRole (UserRef, RoleRef)
	SELECT UserID, rs.ID
	FROM (SELECT @uID UserID) UserID FULL OUTER JOIN fxRole rs ON 1 <> 2
	
