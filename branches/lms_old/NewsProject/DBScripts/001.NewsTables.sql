CREATE TABLE [tblCategory]
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(50) NOT NULL UNIQUE,
	Description nvarchar(max) NULL
)

CREATE TABLE [tblNews]
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	CategoryRef INT NOT NULL,
	Title nvarchar(max) NOT NULL,
	Contents nvarchar(max) NOT NULL,
	Files binary NULL
)
GO

ALTER TABLE tblNews ADD CONSTRAINT tlbNews_CategoryRef
	FOREIGN KEY (CategoryRef)
	REFERENCES tblCategory(ID)
	
CREATE TABLE [tblComment]
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	ParentCommentRef INT NULL,
	UserRef INT NOT NULL,
	NewsRef INT NOT NULL,
	Content nvarchar(max)
)
GO

ALTER TABLE tblComment ADD CONSTRAINT tblComment_ParentCommentRef
	FOREIGN KEY (ParentCommentRef)
	REFERENCES tblComment(ID)

ALTER TABLE tblComment ADD CONSTRAINT tblComment_UserRef
	FOREIGN KEY (UserRef)
	REFERENCES tblUser(ID)
	
ALTER TABLE tblComment ADD CONSTRAINT tblComment_NewsRef
	FOREIGN KEY (NewsRef)
	REFERENCES tblNews(ID)
	
CREATE TABLE fxNewsOperation
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(32) UNIQUE
)
GO

INSERT INTO fxNewsOperation (Name)
	select 'View' union all
	select 'Edit' union all
	select 'Comment' union all
	select 'Lock' union all
	select 'Delete'
	
	
CREATE TABLE fxCategoryOperation
(
	ID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(32) UNIQUE
)
GO

INSERT INTO fxCategoryOperation(Name)
	select 'View' union all
	select 'Edit' union all
	select 'Add_Topic' union all
	select 'Lock' union all
	select 'Delete' 

DECLARE @uID INT	
INSERT INTO tblUser (FirstName, SecondName, Email, PasswordHash) VALUES
	('Guest', 'Guest', 'Guest', 'Guest')
SET @uID = SCOPE_IDENTITY()

INSERT INTO relUserRole (UserRef, RoleRef) VALUES
	(@uID, (SELECT ID from fxRole where Name = 'Guest'))