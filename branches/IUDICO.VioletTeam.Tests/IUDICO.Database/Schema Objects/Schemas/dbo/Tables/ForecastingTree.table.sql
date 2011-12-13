CREATE TABLE dbo.ForecastingTree
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(50) NOT NULL,
	UserRef uniqueidentifier NOT NULL,
	Created datetime NOT NULL,
	Updated datetime NOT NULL,
	IsDeleted bit NOT NULL
	)  ON [PRIMARY]


