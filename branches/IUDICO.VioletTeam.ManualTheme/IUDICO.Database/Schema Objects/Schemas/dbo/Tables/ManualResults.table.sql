CREATE TABLE [dbo].[ManualResults](
	[Id] [int] NOT NULL,
	[UserRef] [uniqueidentifier] NOT NULL,
	[ThemeRef] [int] NOT NULL,
	[Score] [float] NOT NULL,
 CONSTRAINT [PK_ManualResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


