USE [IUDICO]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 12/26/2010 13:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/26/2010 13:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/26/2010 13:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[OpenId] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[RoleRef] [int] NOT NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupUsers]    Script Date: 12/26/2010 13:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupUsers](
	[GroupRef] [int] NOT NULL,
	[UserRef] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GroupUsers] PRIMARY KEY CLUSTERED 
(
	[GroupRef] ASC,
	[UserRef] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__User__Id__47DBAE45]    Script Date: 12/26/2010 13:18:55 ******/
ALTER TABLE [dbo].[User] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
/****** Object:  ForeignKey [FK_GroupUsers_Group]    Script Date: 12/26/2010 13:18:55 ******/
ALTER TABLE [dbo].[GroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupUsers_Group] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupUsers] CHECK CONSTRAINT [FK_GroupUsers_Group]
GO
/****** Object:  ForeignKey [FK_GroupUsers_User]    Script Date: 12/26/2010 13:18:55 ******/
ALTER TABLE [dbo].[GroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupUsers_User] FOREIGN KEY([UserRef])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[GroupUsers] CHECK CONSTRAINT [FK_GroupUsers_User]
GO
/****** Object:  ForeignKey [FK_User_Role]    Script Date: 12/26/2010 13:18:55 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleRef])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
