CREATE TABLE dbo.ForecastingResults
	(
	Id int NOT NULL IDENTITY (1, 1),
	StudentRef uniqueidentifier NOT NULL,
	TreeRef int NOT NULL,
	ForecastingPoint int NULL,
	ForecastingPercent float(53) NULL
	)  ON [PRIMARY]


