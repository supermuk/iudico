USE [IUDICO]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/10/2010 21:52:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[OpenID] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stages]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[CurriculumRef] [int] NOT NULL,
 CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Curriculums]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curriculums](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
 CONSTRAINT [PK_Curriculums] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Owner] [nvarchar](50) NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Cources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Themes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[StageRef] [int] NOT NULL,
	[CourseRef] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nodes]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CourseId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[IsFolder] [bit] NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_Nodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupUsers]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupUsers](
	[GroupID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GroupUsers] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurriculumAssignments]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurriculumAssignments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserGroupRef] [int] NULL,
	[CurriculumRef] [int] NULL,
 CONSTRAINT [PK_CurriculumAssignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleUsers]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUsers](
	[RoleID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RoleUsers] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timeline]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timeline](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[CurriculumAssignmentRef] [int] NULL,
	[OperationRef] [int] NULL,
	[StageRef] [int] NULL,
 CONSTRAINT [PK_Timeline] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 12/10/2010 21:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_User_ID]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_ID]  DEFAULT (newsequentialid()) FOR [ID]
GO
/****** Object:  Default [DF_Courses_Deleted]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
/****** Object:  ForeignKey [FK_Themes_Stages]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[Themes]  WITH CHECK ADD  CONSTRAINT [FK_Themes_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[Stages] ([Id])
GO
ALTER TABLE [dbo].[Themes] CHECK CONSTRAINT [FK_Themes_Stages]
GO
/****** Object:  ForeignKey [FK_Nodes_Cources]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[Nodes]  WITH CHECK ADD  CONSTRAINT [FK_Nodes_Cources] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[Nodes] CHECK CONSTRAINT [FK_Nodes_Cources]
GO
/****** Object:  ForeignKey [FK_Nodes_Nodes]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[Nodes]  WITH CHECK ADD  CONSTRAINT [FK_Nodes_Nodes] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Nodes] ([Id])
GO
ALTER TABLE [dbo].[Nodes] CHECK CONSTRAINT [FK_Nodes_Nodes]
GO
/****** Object:  ForeignKey [FK_GroupUsers_Group]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[GroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupUsers_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
GO
ALTER TABLE [dbo].[GroupUsers] CHECK CONSTRAINT [FK_GroupUsers_Group]
GO
/****** Object:  ForeignKey [FK_GroupUsers_User]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[GroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupUsers_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[GroupUsers] CHECK CONSTRAINT [FK_GroupUsers_User]
GO
/****** Object:  ForeignKey [FK_CurriculumAssignments_Curriculums]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[CurriculumAssignments]  WITH CHECK ADD  CONSTRAINT [FK_CurriculumAssignments_Curriculums] FOREIGN KEY([Id])
REFERENCES [dbo].[Curriculums] ([Id])
GO
ALTER TABLE [dbo].[CurriculumAssignments] CHECK CONSTRAINT [FK_CurriculumAssignments_Curriculums]
GO
/****** Object:  ForeignKey [FK_RoleUsers_Role]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoleUsers_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_RoleUsers_Role]
GO
/****** Object:  ForeignKey [FK_RoleUsers_User]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoleUsers_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_RoleUsers_User]
GO
/****** Object:  ForeignKey [FK_Timeline_CurriculumAssignments1]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[Timeline]  WITH CHECK ADD  CONSTRAINT [FK_Timeline_CurriculumAssignments1] FOREIGN KEY([Id])
REFERENCES [dbo].[CurriculumAssignments] ([Id])
GO
ALTER TABLE [dbo].[Timeline] CHECK CONSTRAINT [FK_Timeline_CurriculumAssignments1]
GO
/****** Object:  ForeignKey [FK_Operation_Timeline]    Script Date: 12/10/2010 21:52:27 ******/
ALTER TABLE [dbo].[Operation]  WITH CHECK ADD  CONSTRAINT [FK_Operation_Timeline] FOREIGN KEY([Id])
REFERENCES [dbo].[Timeline] ([Id])
GO
ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [FK_Operation_Timeline]
GO
