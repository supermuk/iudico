CREATE TABLE [tblUserNotes](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[UserRef] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[SysState] [smallint] NOT NULL,
 CONSTRAINT [PK_UserNotes] PRIMARY KEY  
(
	[ID] ASC
) 
) 
GO