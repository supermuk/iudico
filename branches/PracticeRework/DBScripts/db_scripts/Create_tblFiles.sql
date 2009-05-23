USE [IUDICO]
GO
/****** Object:  Table [dbo].[tblFiles]    Script Date: 11/05/2008 14:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblFiles](
	[ID] [int] NOT NULL,
	[PID] [int] NULL,
	[PageRef] [int] NULL,
	[File] [varbinary](max) NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[IsDirectory] [bit] NULL,
 CONSTRAINT [PK_tblFiles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tblFiles]  WITH CHECK ADD  CONSTRAINT [FK_tblFiles_tblFiles] FOREIGN KEY([PID])
REFERENCES [dbo].[tblFiles] ([ID])
GO
ALTER TABLE [dbo].[tblFiles] CHECK CONSTRAINT [FK_tblFiles_tblFiles]
GO
ALTER TABLE [dbo].[tblFiles]  WITH CHECK ADD  CONSTRAINT [FK_tblFiles_tblPages] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblFiles] CHECK CONSTRAINT [FK_tblFiles_tblPages]