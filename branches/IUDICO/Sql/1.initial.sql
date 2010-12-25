USE [IUDICO]
GO
/****** Object:  ForeignKey [FK_CurriculumAssignments_Curriculums]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CurriculumAssignments_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[CurriculumAssignments]'))
ALTER TABLE [dbo].[CurriculumAssignments] DROP CONSTRAINT [FK_CurriculumAssignments_Curriculums]
GO
/****** Object:  ForeignKey [FK_GroupUsers_Group]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupUsers_Group]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupUsers]'))
ALTER TABLE [dbo].[GroupUsers] DROP CONSTRAINT [FK_GroupUsers_Group]
GO
/****** Object:  ForeignKey [FK_GroupUsers_User]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupUsers_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupUsers]'))
ALTER TABLE [dbo].[GroupUsers] DROP CONSTRAINT [FK_GroupUsers_User]
GO
/****** Object:  ForeignKey [FK_Nodes_Cources]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Cources]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Cources]
GO
/****** Object:  ForeignKey [FK_Nodes_Nodes]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Nodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] DROP CONSTRAINT [FK_Nodes_Nodes]
GO
/****** Object:  ForeignKey [FK_Operation_Timeline]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Operation_Timeline]') AND parent_object_id = OBJECT_ID(N'[dbo].[Operation]'))
ALTER TABLE [dbo].[Operation] DROP CONSTRAINT [FK_Operation_Timeline]
GO
/****** Object:  ForeignKey [FK_RoleUsers_Role]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleUsers_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] DROP CONSTRAINT [FK_RoleUsers_Role]
GO
/****** Object:  ForeignKey [FK_RoleUsers_User]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleUsers_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] DROP CONSTRAINT [FK_RoleUsers_User]
GO
/****** Object:  ForeignKey [FK_Stages_Curriculums]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stages]'))
ALTER TABLE [dbo].[Stages] DROP CONSTRAINT [FK_Stages_Curriculums]
GO
/****** Object:  ForeignKey [FK_Themes_Stages]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
ALTER TABLE [dbo].[Themes] DROP CONSTRAINT [FK_Themes_Stages]
GO
/****** Object:  ForeignKey [FK_Timeline_CurriculumAssignments1]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Timeline_CurriculumAssignments1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Timeline]'))
ALTER TABLE [dbo].[Timeline] DROP CONSTRAINT [FK_Timeline_CurriculumAssignments1]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operation]') AND type in (N'U'))
DROP TABLE [dbo].[Operation]
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Themes]') AND type in (N'U'))
DROP TABLE [dbo].[Themes]
GO
/****** Object:  Table [dbo].[Timeline]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Timeline]') AND type in (N'U'))
DROP TABLE [dbo].[Timeline]
GO
/****** Object:  Table [dbo].[RoleUsers]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND type in (N'U'))
DROP TABLE [dbo].[RoleUsers]
GO
/****** Object:  Table [dbo].[Stages]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stages]') AND type in (N'U'))
DROP TABLE [dbo].[Stages]
GO
/****** Object:  Table [dbo].[CurriculumAssignments]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurriculumAssignments]') AND type in (N'U'))
DROP TABLE [dbo].[CurriculumAssignments]
GO
/****** Object:  Table [dbo].[GroupUsers]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupUsers]') AND type in (N'U'))
DROP TABLE [dbo].[GroupUsers]
GO
/****** Object:  Table [dbo].[Nodes]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nodes]') AND type in (N'U'))
DROP TABLE [dbo].[Nodes]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Courses]') AND type in (N'U'))
DROP TABLE [dbo].[Courses]
GO
/****** Object:  Table [dbo].[Curriculums]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Curriculums]') AND type in (N'U'))
DROP TABLE [dbo].[Curriculums]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND type in (N'U'))
DROP TABLE [dbo].[Group]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Default [DF_Courses_Deleted]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Deleted]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Courses_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [DF_Courses_Deleted]
END


End
GO
/****** Object:  Default [DF_Curriculums_IsDeleted]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Curriculums_IsDeleted]') AND parent_object_id = OBJECT_ID(N'[dbo].[Curriculums]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Curriculums_IsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Curriculums] DROP CONSTRAINT [DF_Curriculums_IsDeleted]
END


End
GO
/****** Object:  Default [DF_User_ID]    Script Date: 12/21/2010 01:04:45 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF_User_ID]
END


End
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[ID] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](100) COLLATE Ukrainian_CI_AS NOT NULL,
	[Password] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Email] [nvarchar](100) COLLATE Ukrainian_CI_AS NOT NULL,
	[OpenID] [nvarchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Name] [nvarchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Group]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Curriculums]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Curriculums]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Curriculums](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Curriculums] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Courses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Courses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Owner] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Cources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Nodes]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Nodes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Nodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[CourseId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[IsFolder] [bit] NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_Nodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[GroupUsers]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GroupUsers](
	[GroupID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GroupUsers] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[CurriculumAssignments]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurriculumAssignments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CurriculumAssignments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserGroupRef] [int] NULL,
	[CurriculumRef] [int] NULL,
 CONSTRAINT [PK_CurriculumAssignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Stages]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Stages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[CurriculumRef] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[RoleUsers]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleUsers](
	[RoleID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RoleUsers] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Timeline]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Timeline]') AND type in (N'U'))
BEGIN
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Themes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Themes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[StageRef] [int] NOT NULL,
	[CourseRef] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 12/21/2010 01:04:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Operation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
 CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Default [DF_Courses_Deleted]    Script Date: 12/21/2010 01:04:45 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Courses_Deleted]') AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Courses_Deleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Deleted]  DEFAULT ((0)) FOR [Deleted]
END


End
GO
/****** Object:  Default [DF_Curriculums_IsDeleted]    Script Date: 12/21/2010 01:04:45 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Curriculums_IsDeleted]') AND parent_object_id = OBJECT_ID(N'[dbo].[Curriculums]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Curriculums_IsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Curriculums] ADD  CONSTRAINT [DF_Curriculums_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
END


End
GO
/****** Object:  Default [DF_User_ID]    Script Date: 12/21/2010 01:04:45 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_ID]  DEFAULT (newsequentialid()) FOR [ID]
END


End
GO
/****** Object:  ForeignKey [FK_CurriculumAssignments_Curriculums]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CurriculumAssignments_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[CurriculumAssignments]'))
ALTER TABLE [dbo].[CurriculumAssignments]  WITH CHECK ADD  CONSTRAINT [FK_CurriculumAssignments_Curriculums] FOREIGN KEY([Id])
REFERENCES [dbo].[Curriculums] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CurriculumAssignments_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[CurriculumAssignments]'))
ALTER TABLE [dbo].[CurriculumAssignments] CHECK CONSTRAINT [FK_CurriculumAssignments_Curriculums]
GO
/****** Object:  ForeignKey [FK_GroupUsers_Group]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupUsers_Group]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupUsers]'))
ALTER TABLE [dbo].[GroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupUsers_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupUsers_Group]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupUsers]'))
ALTER TABLE [dbo].[GroupUsers] CHECK CONSTRAINT [FK_GroupUsers_Group]
GO
/****** Object:  ForeignKey [FK_GroupUsers_User]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupUsers_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupUsers]'))
ALTER TABLE [dbo].[GroupUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupUsers_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupUsers_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupUsers]'))
ALTER TABLE [dbo].[GroupUsers] CHECK CONSTRAINT [FK_GroupUsers_User]
GO
/****** Object:  ForeignKey [FK_Nodes_Cources]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Cources]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes]  WITH CHECK ADD  CONSTRAINT [FK_Nodes_Cources] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Cources]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] CHECK CONSTRAINT [FK_Nodes_Cources]
GO
/****** Object:  ForeignKey [FK_Nodes_Nodes]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Nodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes]  WITH CHECK ADD  CONSTRAINT [FK_Nodes_Nodes] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Nodes] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nodes_Nodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Nodes]'))
ALTER TABLE [dbo].[Nodes] CHECK CONSTRAINT [FK_Nodes_Nodes]
GO
/****** Object:  ForeignKey [FK_Operation_Timeline]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Operation_Timeline]') AND parent_object_id = OBJECT_ID(N'[dbo].[Operation]'))
ALTER TABLE [dbo].[Operation]  WITH CHECK ADD  CONSTRAINT [FK_Operation_Timeline] FOREIGN KEY([Id])
REFERENCES [dbo].[Timeline] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Operation_Timeline]') AND parent_object_id = OBJECT_ID(N'[dbo].[Operation]'))
ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [FK_Operation_Timeline]
GO
/****** Object:  ForeignKey [FK_RoleUsers_Role]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleUsers_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoleUsers_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleUsers_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_RoleUsers_Role]
GO
/****** Object:  ForeignKey [FK_RoleUsers_User]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleUsers_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoleUsers_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleUsers_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_RoleUsers_User]
GO
/****** Object:  ForeignKey [FK_Stages_Curriculums]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stages]'))
ALTER TABLE [dbo].[Stages]  WITH CHECK ADD  CONSTRAINT [FK_Stages_Curriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[Curriculums] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stages]'))
ALTER TABLE [dbo].[Stages] CHECK CONSTRAINT [FK_Stages_Curriculums]
GO
/****** Object:  ForeignKey [FK_Themes_Stages]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
ALTER TABLE [dbo].[Themes]  WITH CHECK ADD  CONSTRAINT [FK_Themes_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[Stages] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Themes]'))
ALTER TABLE [dbo].[Themes] CHECK CONSTRAINT [FK_Themes_Stages]
GO
/****** Object:  ForeignKey [FK_Timeline_CurriculumAssignments1]    Script Date: 12/21/2010 01:04:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Timeline_CurriculumAssignments1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Timeline]'))
ALTER TABLE [dbo].[Timeline]  WITH CHECK ADD  CONSTRAINT [FK_Timeline_CurriculumAssignments1] FOREIGN KEY([Id])
REFERENCES [dbo].[CurriculumAssignments] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Timeline_CurriculumAssignments1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Timeline]'))
ALTER TABLE [dbo].[Timeline] CHECK CONSTRAINT [FK_Timeline_CurriculumAssignments1]
GO
