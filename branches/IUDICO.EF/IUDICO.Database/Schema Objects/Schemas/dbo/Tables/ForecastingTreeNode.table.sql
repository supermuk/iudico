CREATE TABLE dbo.ForecastingTreeNode
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(50) NOT NULL,
	Created datetime NOT NULL,
	Updated datetime NOT NULL,
	TreeRef int NOT NULL,
	ParentRef int NULL,
	CourseRef int NULL,
	IsDeleted bit NOT NULL
	)  ON [PRIMARY]


