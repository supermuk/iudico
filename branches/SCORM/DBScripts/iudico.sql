USE [IUDICO]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSecurityID]    Script Date: 01/28/2010 01:26:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[GetSecurityID]()
RETURNS uniqueidentifier
AS
BEGIN
	RETURN 'd9826e47-1ed4-4250-8e00-1ab88ea373e7';
END
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxThemeOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_ThemeOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxThemeOperations] ON
INSERT [dbo].[fxThemeOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'View', N'View the theme', 0, 0)
INSERT [dbo].[fxThemeOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Pass', N'Pass the theme', 0, 0)
SET IDENTITY_INSERT [dbo].[fxThemeOperations] OFF
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxStageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_StageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxStageOperations] ON
INSERT [dbo].[fxStageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'View', N'View the stage', 0, 0)
INSERT [dbo].[fxStageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Pass', N'Pass the stage', 0, 0)
SET IDENTITY_INSERT [dbo].[fxStageOperations] OFF
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxSampleBusinesObjectOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [UQ__fxSampleBusinesO__023D5A04] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fxRoles]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxRoles] ON
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (1, N'STUDENT', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (2, N'LECTOR', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (3, N'TRAINER', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (4, N'ADMIN', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (5, N'SUPER_ADMIN', NULL, 0)
SET IDENTITY_INSERT [dbo].[fxRoles] OFF
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxPageTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](10) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_PageType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxPageTypes] ON
INSERT [dbo].[fxPageTypes] ([ID], [Type], [sysState]) VALUES (1, N'Theory', 0)
INSERT [dbo].[fxPageTypes] ([ID], [Type], [sysState]) VALUES (2, N'Practice', 0)
SET IDENTITY_INSERT [dbo].[fxPageTypes] OFF
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxPageOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdPageOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxPageOrders] ON
INSERT [dbo].[fxPageOrders] ([ID], [Name], [sysState]) VALUES (1, N'Straight', 0)
INSERT [dbo].[fxPageOrders] ([ID], [Name], [sysState]) VALUES (2, N'Random', 0)
SET IDENTITY_INSERT [dbo].[fxPageOrders] OFF
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxPageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_PageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxPageOperations] ON
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (1, N'Add', N'Add new Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (2, N'Edit', N'Edit Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (3, N'View', N'View Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (4, N'Delete', N'Delete Page', 1, 0)
SET IDENTITY_INSERT [dbo].[fxPageOperations] OFF
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxLanguages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdLanguages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxLanguages] ON
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (17, N'Vs6CPlusPlus', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (18, N'Vs8CPlusPlus', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (19, N'DotNet2', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (20, N'DotNet3', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (21, N'Java6', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (22, N'Delphi7', 0)
SET IDENTITY_INSERT [dbo].[fxLanguages] OFF
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxGroupOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxGroupOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_fxGroupOperations_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxGroupOperations] ON
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (1, N'View', NULL, 1, 0)
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (2, N'Rename', NULL, 1, 0)
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (3, N'ChangeMembers', NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[fxGroupOperations] OFF
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxCurriculumOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CurriculumOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxCurriculumOperations] ON
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'Modify', N'Modify curriculum by teacher', 1, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Use', N'Use curriculum by teacher', 1, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (7, N'View', N'View the curriculum', 0, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (8, N'Pass', N'Pass the curriculum', 0, 0)
SET IDENTITY_INSERT [dbo].[fxCurriculumOperations] OFF
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxCourseOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CourseOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxCourseOperations] ON
INSERT [dbo].[fxCourseOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'Modify', N'Modify course by teacher', 1, 0)
INSERT [dbo].[fxCourseOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Use', N'Use course by teacher', 1, 0)
SET IDENTITY_INSERT [dbo].[fxCourseOperations] OFF
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxCompiledStatuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdCompiledStatuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxCompiledStatuses] ON
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (1, N'WrongAnswer', N'Program was compiled, it passed time and memory limits,but it returns wrong output', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (2, N'Accepted', N'Program was compiled, it passed time and memory limits, and it returns correct output.', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (3, N'TimeLimit', N'Program was compiled, but it takes too much time to run.', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (4, N'MemoryLimit', N'Program was compiled, but it takes too much memory during run', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (5, N'CompilationError', N'Program wasnt compiled succesfully', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (6, N'Running', N'Program was compiled, and it is running right now', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (7, N'Enqueued', N'Program was received, and it is waiting too proceed', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (8, N'Crashed', N'Program was compiled, but it crashed during execution', 0)
SET IDENTITY_INSERT [dbo].[fxCompiledStatuses] OFF
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fxAnswerType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxAnswerType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[fxAnswerType] ON
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (1, N'UserAnswer', 0)
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (2, N'EmptyAnswer', 0)
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (3, N'NotIncludedAnswer', 0)
SET IDENTITY_INSERT [dbo].[fxAnswerType] OFF
/****** Object:  Table [dbo].[tblBob]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBob](
	[ID] [int] NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Pattern] [varchar](max) NOT NULL,
	[sysState] [smallint] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblGroups]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblGroups] ON
INSERT [dbo].[tblGroups] ([ID], [Name], [sysState]) VALUES (1, N'123', 0)
SET IDENTITY_INSERT [dbo].[tblGroups] OFF
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCurriculums](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_SdudyCourses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblCurriculums] ON
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (1, N'test', N'123', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (2, N'qwe', N'qwe', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (3, N'DB_test', N'', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (4, N'DB_test', N'DB_test_desc', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (5, N'C++', N'C++', 0)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (6, N'C++', N'', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (7, N'C++', N'', 0)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (8, N'DB Test', N'', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (9, N'bob', N'', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (10, N'lox', N'', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (11, N'Bob', N'', 0)
SET IDENTITY_INSERT [dbo].[tblCurriculums] OFF
/****** Object:  Table [dbo].[tblCourses]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCourses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UploadDate] [datetime] NULL,
	[Version] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblCourses] ON
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (69, N'C++_first', N'', CAST(0x00009CF501522DA3 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (70, N'C++_first', N'', CAST(0x00009CF501569FD2 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (71, N'C++_first', N'', CAST(0x00009CF50169FA8A AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (72, N'C++_first', N'', CAST(0x00009CF600CAEE20 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (73, N'C++_first', N'', CAST(0x00009CF600CD3AB9 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (74, N'C++_first', N'', CAST(0x00009CF600CE10CC AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (75, N'C++_first', N'', CAST(0x00009CF600CEC489 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (76, N'C++_first', N'', CAST(0x00009CF600D1A405 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (77, N'DB_test3', N'', CAST(0x00009CF6015B0F5B AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (78, N'DB_test3', N'', CAST(0x00009CF6015BB1F7 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (79, N'DB_test3', N'', CAST(0x00009CF70019D425 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (80, N'DB_test3', N'', CAST(0x00009CF7001A57E2 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (81, N'DB_test3', N'', CAST(0x00009CF7001D5525 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (82, N'DB_test3', N'', CAST(0x00009CF7001D9FD5 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (83, N'DB_test3', N'', CAST(0x00009CF7001EF8E4 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (84, N'DB_test3', N'', CAST(0x00009CF8016D7F30 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (85, N'DB_test3', N'', CAST(0x00009CF8016DC38E AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (86, N'DB_test3', N'', CAST(0x00009CF8016E7D6F AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (87, N'DB_test3', N'', CAST(0x00009CF8016F5778 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (88, N'DB_test3', N'', CAST(0x00009CF8017286C8 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (89, N'DB_test3', N'', CAST(0x00009CF801778E0B AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (90, N'DB_test_new', N'', CAST(0x00009CF9001BDC14 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (91, N'NewCourse', N'', CAST(0x00009D0B0183DD89 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[tblCourses] OFF
/****** Object:  Table [dbo].[tblLearnerAttempts]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLearnerAttempts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NOT NULL,
	[UserRef] [int] NOT NULL,
	[Started] [datetime] NULL,
	[Finished] [datetime] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblLearnerAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblLearnerAttempts] ON
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (11, 12, 1, CAST(0x00009CF80172D6A5 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (12, 13, 1, CAST(0x00009CF80177BD12 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (13, 13, 1, CAST(0x00009CF8017B335B AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (14, 14, 1, CAST(0x00009CF9001BFDFF AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (15, 14, 1, CAST(0x00009CF90026D83A AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (16, 14, 1, CAST(0x00009CF90027E26C AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (17, 14, 1, CAST(0x00009CF9002862D7 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (18, 14, 1, CAST(0x00009CF9002B0E92 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (19, 14, 1, CAST(0x00009D0A016C7260 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (20, 15, 1, CAST(0x00009D0B0186C298 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (21, 15, 1, CAST(0x00009D0B01870146 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (22, 15, 1, CAST(0x00009D0B01875D93 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (23, 15, 1, CAST(0x00009D0B01879EB4 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (24, 15, 1, CAST(0x00009D0B01881C28 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (25, 17, 1, CAST(0x00009D0B0188B1EE AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (26, 17, 1, CAST(0x00009D0B01892BA5 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (27, 17, 1, CAST(0x00009D0C00010AAF AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (28, 17, 1, CAST(0x00009D0C00014832 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (29, 17, 1, CAST(0x00009D0C0002534A AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (30, 17, 1, CAST(0x00009D0C00026E6C AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (31, 17, 1, CAST(0x00009D0C0004DE0E AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (32, 17, 1, CAST(0x00009D0C0008A851 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (33, 17, 1, CAST(0x00009D0C00098178 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (34, 17, 1, CAST(0x00009D0C000A0292 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (35, 17, 1, CAST(0x00009D0C000B5D6D AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (36, 17, 1, CAST(0x00009D0C000C0B18 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (37, 17, 1, CAST(0x00009D0C000C7EF5 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (38, 17, 1, CAST(0x00009D0C00101693 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (39, 17, 1, CAST(0x00009D0C00107E90 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (40, 17, 1, CAST(0x00009D0C0012584E AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (41, 17, 1, CAST(0x00009D0C00138C27 AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (42, 17, 1, CAST(0x00009D0C0014102A AS DateTime), NULL, 0)
INSERT [dbo].[tblLearnerAttempts] ([ID], [ThemeRef], [UserRef], [Started], [Finished], [sysState]) VALUES (43, 17, 1, CAST(0x00009D0C00173368 AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[tblLearnerAttempts] OFF
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSampleBusinesObject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [UQ__tblSampleBusines__7E6CC920] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVarsScore]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVarsScore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsScore] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVarsInteractions]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVarsInteractions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsInteractions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblVarsInteractions] ON
INSERT [dbo].[tblVarsInteractions] ([ID], [LearnerSessionRef], [Name], [Value], [Number], [sysState]) VALUES (1, 59, N'type', N'choice', 0, 0)
INSERT [dbo].[tblVarsInteractions] ([ID], [LearnerSessionRef], [Name], [Value], [Number], [sysState]) VALUES (2, 60, N'type', N'choice', 0, 0)
INSERT [dbo].[tblVarsInteractions] ([ID], [LearnerSessionRef], [Name], [Value], [Number], [sysState]) VALUES (3, 61, N'type', N'choice', 0, 0)
INSERT [dbo].[tblVarsInteractions] ([ID], [LearnerSessionRef], [Name], [Value], [Number], [sysState]) VALUES (4, 62, N'type', N'choice', 0, 0)
INSERT [dbo].[tblVarsInteractions] ([ID], [LearnerSessionRef], [Name], [Value], [Number], [sysState]) VALUES (5, 63, N'type', N'choice', 0, 0)
INSERT [dbo].[tblVarsInteractions] ([ID], [LearnerSessionRef], [Name], [Value], [Number], [sysState]) VALUES (6, 64, N'type', N'choice', 0, 0)
SET IDENTITY_INSERT [dbo].[tblVarsInteractions] OFF
/****** Object:  Table [dbo].[tblVarsInteractionObjectives]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVarsInteractionObjectives](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVarsInteractionCorrectResponses]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVarsInteractionCorrectResponses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblVarsInteractionCorrectResponses] ON
INSERT [dbo].[tblVarsInteractionCorrectResponses] ([ID], [LearnerSessionRef], [InteractionRef], [Name], [Value], [Number], [sysState]) VALUES (1, 59, 0, N'pattern', N'1', 0, 0)
INSERT [dbo].[tblVarsInteractionCorrectResponses] ([ID], [LearnerSessionRef], [InteractionRef], [Name], [Value], [Number], [sysState]) VALUES (2, 60, 0, N'pattern', N'1', 0, 0)
INSERT [dbo].[tblVarsInteractionCorrectResponses] ([ID], [LearnerSessionRef], [InteractionRef], [Name], [Value], [Number], [sysState]) VALUES (3, 61, 0, N'pattern', N'1', 0, 0)
INSERT [dbo].[tblVarsInteractionCorrectResponses] ([ID], [LearnerSessionRef], [InteractionRef], [Name], [Value], [Number], [sysState]) VALUES (4, 62, 0, N'pattern', N'1', 0, 0)
INSERT [dbo].[tblVarsInteractionCorrectResponses] ([ID], [LearnerSessionRef], [InteractionRef], [Name], [Value], [Number], [sysState]) VALUES (5, 63, 0, N'pattern', N'1', 0, 0)
INSERT [dbo].[tblVarsInteractionCorrectResponses] ([ID], [LearnerSessionRef], [InteractionRef], [Name], [Value], [Number], [sysState]) VALUES (6, 64, 0, N'pattern', N'1', 0, 0)
SET IDENTITY_INSERT [dbo].[tblVarsInteractionCorrectResponses] OFF
/****** Object:  Table [dbo].[tblVars]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblAttemptsVars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblVars] ON
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (112, 52, N'entry', N'ab-initio', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (113, 52, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (114, 53, N'entry', N'ab-initio', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (115, 53, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (116, 54, N'entry', N'ab-initio', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (117, 54, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (118, 55, N'entry', N'ab-initio', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (119, 55, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (120, 56, N'entry', N'ab-initio', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (121, 56, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (122, 57, N'entry', N'ab-initio', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (123, 57, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (124, 58, N'entry', N'ab-initio', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (125, 58, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (126, 59, N'entry', N'resume', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (127, 59, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (128, 60, N'entry', N'resume', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (129, 60, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (130, 61, N'entry', N'resume', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (131, 61, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (132, 62, N'entry', N'resume', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (133, 62, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (134, 63, N'entry', N'ab-initio', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (135, 63, N'credit', N'credit', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (136, 64, N'entry', N'resume', 0)
INSERT [dbo].[tblVars] ([ID], [LearnerSessionRef], [Name], [Value], [sysState]) VALUES (137, 64, N'credit', N'credit', 0)
SET IDENTITY_INSERT [dbo].[tblVars] OFF
/****** Object:  Table [dbo].[tblUsers]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](32) NOT NULL,
	[PasswordHash] [char](32) NOT NULL,
	[Email] [char](50) NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_EMAIL] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblUsers] ON
INSERT [dbo].[tblUsers] ([ID], [FirstName], [LastName], [Login], [PasswordHash], [Email], [sysState]) VALUES (1, N'Volodymyr', N'Shtenovych', N'lex', N'B067B3D3054D8868C950E1946300A3F4', N'ShVolodya@gmail.com                               ', 0)
INSERT [dbo].[tblUsers] ([ID], [FirstName], [LastName], [Login], [PasswordHash], [Email], [sysState]) VALUES (3, N'V', N'P', N'vladykx', N'123                             ', N'1                                                 ', 0)
SET IDENTITY_INSERT [dbo].[tblUsers] OFF
/****** Object:  Table [dbo].[tblStages]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CurriculumRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblStages] ON
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (1, N'', N'', 1, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (2, N'', N'', 1, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (3, N'123', N'a', 1, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (4, N'DB_test', N'', 3, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (5, N'DB_test2', N'', 3, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (6, N'DB_test_stage', N'DB_test_stage_desc', 4, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (7, N'Semester 1', N'Semester 1', 5, 0)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (8, N'C++', N'', 6, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (9, N'asdf', N'', 6, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (10, N'sdfg', N'', 6, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (11, N'Semester 1', N'', 7, 0)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (12, N'DB Test', N'', 8, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (13, N'', N'', 9, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (14, N'lox', N'', 10, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (15, N'lox', N'', 10, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (16, N'lox', N'', 9, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (17, N'Bob', N'', 11, 0)
SET IDENTITY_INSERT [dbo].[tblStages] OFF
/****** Object:  Table [dbo].[tblResources]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblResources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Identifier] [nvarchar](300) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Href] [nvarchar](300) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblResources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblResources] ON
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (214, 70, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (215, 70, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (216, 70, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (217, 70, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (218, 70, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (219, 70, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (220, 70, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (221, 70, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (222, 70, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (223, 71, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (224, 71, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (225, 71, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (226, 71, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (227, 71, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (228, 71, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (229, 71, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (230, 71, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (231, 72, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (232, 72, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (233, 72, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (234, 72, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (235, 72, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (236, 72, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (237, 72, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (238, 72, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (239, 73, N'New_Theory', N'asset', N'New_Theory.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (240, 73, N'New_Theory_1', N'asset', N'New_Theory_1.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (241, 73, N'New_Theory_2', N'asset', N'New_Theory_2.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (242, 73, N'New_Theory_3', N'asset', N'New_Theory_3.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (243, 73, N'New_Theory_4', N'asset', N'New_Theory_4.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (244, 73, N'New_Theory_5', N'asset', N'New_Theory_5.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (245, 73, N'New_Theory_6', N'asset', N'New_Theory_6.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (246, 73, N'New_Theory_7', N'asset', N'New_Theory_7.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (247, 74, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (248, 74, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (249, 74, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (250, 74, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (251, 74, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (252, 74, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (253, 74, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (254, 74, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (255, 75, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (256, 75, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (257, 75, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (258, 75, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (259, 75, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (260, 75, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (261, 75, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (262, 75, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (263, 76, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (264, 76, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (265, 76, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (266, 76, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (267, 76, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (268, 76, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (269, 76, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (270, 77, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (271, 77, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (272, 77, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (273, 77, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (274, 77, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (275, 77, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (276, 77, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (277, 77, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (278, 77, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (279, 77, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (280, 77, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (281, 77, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (282, 77, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (283, 77, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (284, 77, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (285, 77, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (286, 77, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (287, 77, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (288, 77, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (289, 77, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (290, 77, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (291, 77, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (292, 77, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (293, 77, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (294, 78, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (295, 78, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (296, 78, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (297, 78, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (298, 78, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (299, 78, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (300, 78, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (301, 78, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (302, 78, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (303, 78, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (304, 78, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (305, 78, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (306, 78, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (307, 78, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (308, 78, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (309, 78, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (310, 78, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (311, 78, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (312, 78, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (313, 78, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
GO
print 'Processed 100 total records'
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (314, 78, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (315, 78, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (316, 78, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (317, 78, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (318, 79, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (319, 80, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (320, 81, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (321, 82, N'New_Examination_1', N'sco', N'New_Examination_1.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (322, 83, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (323, 83, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (324, 83, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (325, 83, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (326, 83, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (327, 83, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (328, 83, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (329, 83, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (330, 83, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (331, 83, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (332, 83, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (333, 83, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (334, 83, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (335, 83, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (336, 83, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (337, 83, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (338, 83, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (339, 83, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (340, 83, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (341, 83, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (342, 83, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (343, 83, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (344, 83, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (345, 83, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (346, 84, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (347, 84, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (348, 84, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (349, 84, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (350, 84, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (351, 84, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (352, 84, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (353, 84, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (354, 84, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (355, 84, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (356, 84, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (357, 84, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (358, 84, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (359, 84, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (360, 84, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (361, 84, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (362, 84, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (363, 84, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (364, 84, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (365, 84, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (366, 84, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (367, 84, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (368, 84, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (369, 84, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (370, 84, N'Results', N'sco', N'Results.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (371, 85, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (372, 85, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (373, 85, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (374, 85, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (375, 85, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (376, 85, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (377, 85, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (378, 85, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (379, 85, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (380, 85, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (381, 85, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (382, 85, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (383, 85, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (384, 85, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (385, 85, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (386, 85, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (387, 85, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (388, 85, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (389, 85, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (390, 85, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (391, 85, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (392, 85, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (393, 85, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (394, 85, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (395, 85, N'Results', N'sco', N'Results.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (396, 86, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (397, 87, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (398, 88, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (399, 88, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (400, 88, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (401, 88, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (402, 88, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (403, 88, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (404, 88, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (405, 88, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (406, 88, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (407, 88, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (408, 88, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (409, 88, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (410, 88, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (411, 88, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (412, 88, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (413, 88, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (414, 88, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
GO
print 'Processed 200 total records'
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (415, 88, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (416, 88, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (417, 88, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (418, 88, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (419, 88, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (420, 88, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (421, 88, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (422, 88, N'Results', N'sco', N'Results.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (423, 89, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (424, 89, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (425, 89, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (426, 89, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (427, 89, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (428, 89, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (429, 89, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (430, 89, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (431, 89, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (432, 89, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (433, 89, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (434, 89, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (435, 89, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (436, 89, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (437, 89, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (438, 89, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (439, 89, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (440, 89, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (441, 89, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (442, 89, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (443, 89, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (444, 89, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (445, 89, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (446, 89, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (447, 89, N'Results', N'sco', N'Results.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (448, 90, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (449, 90, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (450, 90, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (451, 90, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (452, 90, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (453, 90, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (454, 90, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (455, 90, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (456, 90, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (457, 90, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (458, 90, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (459, 90, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (460, 90, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (461, 90, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (462, 90, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (463, 90, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (464, 90, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (465, 90, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (466, 90, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (467, 90, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (468, 90, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (469, 90, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (470, 90, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (471, 90, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (472, 90, N'Results', N'sco', N'Results.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (473, 91, N'New_Examination', N'sco', N'New_Examination.html', 0)
SET IDENTITY_INSERT [dbo].[tblResources] OFF
/****** Object:  Table [dbo].[tblPages]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NULL,
	[PageTypeRef] [int] NULL,
	[PageRank] [int] NULL,
	[PageName] [nvarchar](50) NULL,
	[PageFile] [varchar](250) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrganizations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblOrganizations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblOrganizations] ON
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (17, 69, N'C++_first', 0)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (18, 70, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (19, 71, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (20, 72, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (21, 74, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (22, 75, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (23, 76, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (24, 77, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (25, 78, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (26, 83, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (27, 84, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (28, 85, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (29, 88, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (30, 89, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (31, 90, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (32, 91, N'NewCourse', 0)
SET IDENTITY_INSERT [dbo].[tblOrganizations] OFF
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompiledQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LanguageRef] [int] NOT NULL,
	[TimeLimit] [int] NULL,
	[MemoryLimit] [int] NULL,
	[OutputLimit] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblCompiledQuestions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[relUserRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_USER_ROLE] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 1, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 2, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 3, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 4, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 5, 0)
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[relUserGroups](
	[UserRef] [int] NOT NULL,
	[GroupRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_relUserGroups_KEY] PRIMARY KEY CLUSTERED 
(
	[UserRef] ASC,
	[GroupRef] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[relUserGroups] ([UserRef], [GroupRef], [sysState]) VALUES (1, 1, 0)
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompiledQuestionsData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompiledQuestionRef] [int] NOT NULL,
	[Input] [nvarchar](max) NULL,
	[Output] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblCompiledQuestionsData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblItems]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NULL,
	[OrganizationRef] [int] NOT NULL,
	[ResourceRef] [int] NULL,
	[Title] [nvarchar](200) NULL,
	[IsLeaf] [bit] NOT NULL,
	[sysState] [int] NOT NULL,
 CONSTRAINT [PK_tblItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblItems] ON
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (176, NULL, 18, 214, N'C++_first', 0, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (177, 176, 18, 214, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (178, 176, 18, 215, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (179, 176, 18, 219, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (180, 176, 18, 218, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (181, NULL, 19, 223, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (182, NULL, 19, 224, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (183, NULL, 19, 228, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (184, NULL, 19, 229, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (185, NULL, 19, 230, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (186, NULL, 20, 231, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (187, NULL, 20, 232, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (188, NULL, 20, 236, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (189, NULL, 20, 237, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (190, NULL, 20, 238, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (191, NULL, 21, 247, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (192, NULL, 21, 248, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (193, NULL, 21, 252, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (194, NULL, 21, 253, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (195, NULL, 21, 254, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (196, NULL, 22, 255, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (197, NULL, 22, 256, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (198, NULL, 22, 260, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (199, NULL, 22, 261, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (200, NULL, 22, 262, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (201, NULL, 23, 263, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (202, NULL, 23, 264, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (203, NULL, 23, 268, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (204, NULL, 23, 269, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (205, NULL, 24, NULL, N'DBColoquium1General+SQL', 0, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (206, 205, 24, 270, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (207, 205, 24, 271, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (208, 205, 24, 272, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (209, 205, 24, 273, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (210, 205, 24, 274, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (211, 205, 24, 275, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (212, 205, 24, 276, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (213, 205, 24, 277, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (214, 205, 24, 278, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (215, 205, 24, 279, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (216, 205, 24, 280, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (217, 205, 24, 281, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (218, 205, 24, 282, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (219, 205, 24, 283, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (220, 205, 24, 284, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (221, 205, 24, 285, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (222, 205, 24, 286, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (223, 205, 24, 287, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (224, 205, 24, 288, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (225, 205, 24, 289, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (226, 205, 24, 290, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (227, 205, 24, 291, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (228, 205, 24, 292, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (229, 205, 24, 293, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (230, NULL, 25, 294, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (231, NULL, 25, 295, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (232, NULL, 25, 296, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (233, NULL, 25, 297, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (234, NULL, 25, 298, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (235, NULL, 25, 299, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (236, NULL, 25, 300, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (237, NULL, 25, 301, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (238, NULL, 25, 302, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (239, NULL, 25, 303, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (240, NULL, 25, 304, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (241, NULL, 25, 305, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (242, NULL, 25, 306, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (243, NULL, 25, 307, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (244, NULL, 25, 308, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (245, NULL, 25, 309, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (246, NULL, 25, 310, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (247, NULL, 25, 311, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (248, NULL, 25, 312, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (249, NULL, 25, 313, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (250, NULL, 25, 314, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (251, NULL, 25, 315, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (252, NULL, 25, 316, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (253, NULL, 25, 317, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (254, NULL, 26, 322, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (255, NULL, 26, 323, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (256, NULL, 26, 324, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (257, NULL, 26, 325, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (258, NULL, 26, 326, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (259, NULL, 26, 327, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (260, NULL, 26, 328, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (261, NULL, 26, 329, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (262, NULL, 26, 330, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (263, NULL, 26, 331, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (264, NULL, 26, 332, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (265, NULL, 26, 333, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (266, NULL, 26, 334, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (267, NULL, 26, 335, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (268, NULL, 26, 336, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (269, NULL, 26, 337, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (270, NULL, 26, 338, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (271, NULL, 26, 339, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (272, NULL, 26, 340, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (273, NULL, 26, 341, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (274, NULL, 26, 342, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (275, NULL, 26, 343, N'test 22', 1, 1)
GO
print 'Processed 100 total records'
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (276, NULL, 26, 344, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (277, NULL, 26, 345, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (278, NULL, 27, 346, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (279, NULL, 27, 347, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (280, NULL, 27, 348, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (281, NULL, 27, 349, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (282, NULL, 27, 350, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (283, NULL, 27, 351, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (284, NULL, 27, 352, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (285, NULL, 27, 353, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (286, NULL, 27, 354, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (287, NULL, 27, 355, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (288, NULL, 27, 356, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (289, NULL, 27, 357, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (290, NULL, 27, 358, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (291, NULL, 27, 359, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (292, NULL, 27, 360, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (293, NULL, 27, 361, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (294, NULL, 27, 362, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (295, NULL, 27, 363, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (296, NULL, 27, 364, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (297, NULL, 27, 365, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (298, NULL, 27, 366, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (299, NULL, 27, 367, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (300, NULL, 27, 368, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (301, NULL, 27, 369, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (302, NULL, 27, 370, N'Results', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (303, NULL, 28, 371, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (304, NULL, 28, 372, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (305, NULL, 28, 373, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (306, NULL, 28, 374, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (307, NULL, 28, 375, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (308, NULL, 28, 376, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (309, NULL, 28, 377, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (310, NULL, 28, 378, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (311, NULL, 28, 379, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (312, NULL, 28, 380, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (313, NULL, 28, 381, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (314, NULL, 28, 382, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (315, NULL, 28, 383, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (316, NULL, 28, 384, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (317, NULL, 28, 385, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (318, NULL, 28, 386, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (319, NULL, 28, 387, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (320, NULL, 28, 388, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (321, NULL, 28, 389, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (322, NULL, 28, 390, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (323, NULL, 28, 391, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (324, NULL, 28, 392, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (325, NULL, 28, 393, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (326, NULL, 28, 394, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (327, NULL, 28, 395, N'Results', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (328, NULL, 29, 398, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (329, NULL, 29, 399, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (330, NULL, 29, 400, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (331, NULL, 29, 401, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (332, NULL, 29, 402, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (333, NULL, 29, 403, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (334, NULL, 29, 404, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (335, NULL, 29, 405, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (336, NULL, 29, 406, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (337, NULL, 29, 407, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (338, NULL, 29, 408, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (339, NULL, 29, 409, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (340, NULL, 29, 410, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (341, NULL, 29, 411, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (342, NULL, 29, 412, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (343, NULL, 29, 413, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (344, NULL, 29, 414, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (345, NULL, 29, 415, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (346, NULL, 29, 416, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (347, NULL, 29, 417, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (348, NULL, 29, 418, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (349, NULL, 29, 419, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (350, NULL, 29, 420, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (351, NULL, 29, 421, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (352, NULL, 29, 422, N'Results', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (353, NULL, 30, 423, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (354, NULL, 30, 424, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (355, NULL, 30, 425, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (356, NULL, 30, 426, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (357, NULL, 30, 427, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (358, NULL, 30, 428, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (359, NULL, 30, 429, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (360, NULL, 30, 430, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (361, NULL, 30, 431, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (362, NULL, 30, 432, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (363, NULL, 30, 433, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (364, NULL, 30, 434, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (365, NULL, 30, 435, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (366, NULL, 30, 436, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (367, NULL, 30, 437, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (368, NULL, 30, 438, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (369, NULL, 30, 439, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (370, NULL, 30, 440, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (371, NULL, 30, 441, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (372, NULL, 30, 442, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (373, NULL, 30, 443, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (374, NULL, 30, 444, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (375, NULL, 30, 445, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (376, NULL, 30, 446, N'test 24', 1, 1)
GO
print 'Processed 200 total records'
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (377, NULL, 30, 447, N'Results', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (378, NULL, 31, 448, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (379, NULL, 31, 449, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (380, NULL, 31, 450, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (381, NULL, 31, 451, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (382, NULL, 31, 452, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (383, NULL, 31, 453, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (384, NULL, 31, 454, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (385, NULL, 31, 455, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (386, NULL, 31, 456, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (387, NULL, 31, 457, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (388, NULL, 31, 458, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (389, NULL, 31, 459, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (390, NULL, 31, 460, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (391, NULL, 31, 461, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (392, NULL, 31, 462, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (393, NULL, 31, 463, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (394, NULL, 31, 464, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (395, NULL, 31, 465, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (396, NULL, 31, 466, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (397, NULL, 31, 467, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (398, NULL, 31, 468, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (399, NULL, 31, 469, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (400, NULL, 31, 470, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (401, NULL, 31, 471, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (402, NULL, 31, 472, N'Results', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (403, NULL, 32, 473, N'New Examination', 1, 0)
SET IDENTITY_INSERT [dbo].[tblItems] OFF
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageRef] [int] NULL,
	[TestName] [nvarchar](50) NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
	[Rank] [int] NULL,
	[IsCompiled] [bit] NOT NULL,
	[CompiledQuestionRef] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CorrectAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblThemes]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblThemes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CourseRef] [int] NOT NULL,
	[OrganizationRef] [int] NOT NULL,
	[StageRef] [int] NOT NULL,
	[IsControl] [bit] NOT NULL,
	[PageOrderRef] [int] NULL,
	[sysState] [smallint] NOT NULL,
	[PageCountToShow] [int] NULL,
	[MaxCountToSubmit] [int] NULL,
 CONSTRAINT [PK_Chapter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblThemes] ON
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (2, N'C++_first', 71, 19, 8, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (3, N'C++_first', 71, 19, 9, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (4, N'C++_first', 71, 19, 9, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (5, N'C++_first', 71, 19, 10, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (6, N'C++_first', 71, 19, 8, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (7, N'C++_first', 75, 22, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (8, N'C++_first', 76, 23, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (9, N'DB_test3', 77, 24, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (10, N'DB_test3', 78, 25, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (11, N'DB_test3', 83, 26, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (12, N'DB_test3', 88, 29, 12, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (13, N'DB_test3', 89, 30, 12, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (14, N'DB_test3', 90, 31, 12, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (15, N'NewCourse', 91, 32, 14, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (16, N'NewCourse', 91, 32, 16, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (17, N'NewCourse', 91, 32, 17, 0, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblThemes] OFF
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserAnswers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRef] [int] NULL,
	[QuestionRef] [int] NULL,
	[UserAnswer] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[IsCompiledAnswer] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
	[AnswerTypeRef] [int] NOT NULL,
 CONSTRAINT [PK_UserAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPermissions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentPermitionRef] [int] NULL,
	[DateSince] [datetime] NULL,
	[DateTill] [datetime] NULL,
	[OwnerUserRef] [int] NULL,
	[OwnerGroupRef] [int] NULL,
	[CanBeDelagated] [bit] NOT NULL,
	[CourseRef] [int] NULL,
	[CourseOperationRef] [int] NULL,
	[CurriculumRef] [int] NULL,
	[CurriculumOperationRef] [int] NULL,
	[StageRef] [int] NULL,
	[StageOperationRef] [int] NULL,
	[ThemeRef] [int] NULL,
	[ThemeOperationRef] [int] NULL,
	[PageRef] [int] NULL,
	[PageOperationRef] [int] NULL,
	[UserObjectRef] [int] NULL,
	[GroupObjectRef] [int] NULL,
	[GroupRef] [int] NULL,
	[GroupOperationRef] [int] NULL,
	[OrganizationRef] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblPermissions] ON
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (147, NULL, NULL, NULL, 1, NULL, 1, 88, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (148, NULL, NULL, NULL, 1, NULL, 1, 88, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (149, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 8, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (150, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 8, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (151, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 8, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (152, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 8, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (153, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 12, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (154, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 12, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (155, NULL, NULL, NULL, 1, NULL, 1, 89, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (156, NULL, NULL, NULL, 1, NULL, 1, 89, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (157, NULL, NULL, NULL, 1, NULL, 1, 90, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (158, NULL, NULL, NULL, 1, NULL, 1, 90, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (159, NULL, NULL, NULL, 1, NULL, 1, 91, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (160, NULL, NULL, NULL, 1, NULL, 1, 91, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (161, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 9, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (162, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 9, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (163, NULL, CAST(0x00009CF100000000 AS DateTime), CAST(0x00009D0F00000000 AS DateTime), NULL, 1, 1, NULL, NULL, 9, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (164, NULL, CAST(0x00009CF100000000 AS DateTime), CAST(0x00009D0F00000000 AS DateTime), NULL, 1, 1, NULL, NULL, 9, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (165, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 13, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (166, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 13, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (167, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 10, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (168, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (169, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 10, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (170, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 10, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (171, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 14, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (172, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 14, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (173, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 15, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (174, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 15, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (175, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 11, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (176, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 11, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (177, NULL, CAST(0x00009CF100000000 AS DateTime), CAST(0x00009D0F00000000 AS DateTime), NULL, 1, 1, NULL, NULL, 11, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (178, NULL, CAST(0x00009CF100000000 AS DateTime), CAST(0x00009D0F00000000 AS DateTime), NULL, 1, 1, NULL, NULL, 11, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (179, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 17, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (180, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 17, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (181, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 11, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[tblPermissions] OFF
/****** Object:  Table [dbo].[tblLearnerSessions]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLearnerSessions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerAttemptRef] [int] NOT NULL,
	[ItemRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblLearnerSessions] ON
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (1, 11, 328, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (2, 12, 353, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (3, 13, 353, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (4, 14, 378, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (5, 14, 379, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (6, 14, 380, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (7, 14, 381, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (8, 14, 382, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (9, 14, 383, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (10, 14, 384, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (11, 14, 385, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (12, 14, 386, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (13, 14, 387, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (14, 14, 388, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (15, 14, 389, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (16, 14, 390, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (17, 14, 391, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (18, 14, 392, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (19, 14, 393, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (20, 14, 394, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (21, 14, 395, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (22, 14, 396, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (23, 14, 397, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (24, 14, 398, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (25, 14, 399, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (26, 14, 400, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (27, 14, 401, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (28, 14, 402, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (29, 15, 378, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (30, 16, 378, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (31, 17, 378, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (32, 17, 378, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (33, 17, 378, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (34, 17, 379, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (35, 17, 379, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (36, 17, 380, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (37, 17, 380, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (38, 17, 381, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (39, 18, 378, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (40, 19, 378, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (41, 20, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (42, 21, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (43, 22, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (44, 23, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (45, 24, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (46, 25, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (47, 26, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (48, 27, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (49, 28, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (50, 29, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (51, 30, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (52, 31, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (53, 32, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (54, 33, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (55, 34, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (56, 35, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (57, 36, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (58, 37, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (59, 38, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (60, 39, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (61, 40, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (62, 41, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (63, 42, 403, 0)
INSERT [dbo].[tblLearnerSessions] ([ID], [LearnerAttemptRef], [ItemRef], [sysState]) VALUES (64, 43, 403, 0)
SET IDENTITY_INSERT [dbo].[tblLearnerSessions] OFF
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 01/28/2010 01:27:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompiledAnswers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TimeUsed] [int] NULL,
	[MemoryUsed] [int] NULL,
	[StatusRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
	[UserAnswerRef] [int] NOT NULL,
	[Output] [nvarchar](max) NULL,
	[CompiledQuestionsDataRef] [int] NOT NULL,
 CONSTRAINT [PK_tblCompiledAnswers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsTheme]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetPermissionsTheme]
	@UserID int,
	@ThemeOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@ThemeOperationID IS NULL) OR (@ThemeOperationID = ThemeOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[ThemeRef],parent_prms.[ThemeOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsStage]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetPermissionsStage]
	@UserID int,
	@StageOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@StageOperationID IS NULL) OR (@StageOperationID = StageOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[StageRef],parent_prms.[StageOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsGroup]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetPermissionsGroup]
	@UserID int,
	@GroupOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@GroupOperationID IS NULL) OR (@GroupOperationID = GroupOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[GroupRef],parent_prms.[GroupOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCurriculum]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetPermissionsCurriculum]
	@UserID int,
	@CurriculumOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@CurriculumOperationID IS NULL) OR (@CurriculumOperationID = CurriculumOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CurriculumRef],parent_prms.[CurriculumOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCourse]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetPermissionsCourse]
	@UserID int,
	@CourseOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@CourseOperationID IS NULL) OR (@CourseOperationID = CourseOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CourseRef],parent_prms.[CourseOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForTheme]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetOperationsForTheme]
	@UserID int,
	@ThemeID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[ThemeRef],parent_prms.[ThemeOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT ThemeOperationRef from FlatPermissionList		
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForStage]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetOperationsForStage]
	@UserID int,
	@StageID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[StageRef],parent_prms.[StageOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT StageOperationRef from FlatPermissionList		
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForGroup]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetOperationsForGroup]
	@UserID int,
	@GroupID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[GroupRef],parent_prms.[GroupOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT GroupOperationRef from FlatPermissionList		
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCurriculum]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetOperationsForCurriculum]
	@UserID int,
	@CurriculumID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CurriculumRef],parent_prms.[CurriculumOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT CurriculumOperationRef from FlatPermissionList		
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCourse]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetOperationsForCourse]
	@UserID int,
	@CourseID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CourseRef],parent_prms.[CourseOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT CourseOperationRef from FlatPermissionList		
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsTheme]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsTheme]
    @GroupID int,
    @ThemeOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@ThemeOperationID IS NULL) OR (@ThemeOperationID = ThemeOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[ThemeRef],parent_prms.[ThemeOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsStage]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsStage]
    @GroupID int,
    @StageOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@StageOperationID IS NULL) OR (@StageOperationID = StageOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[StageRef],parent_prms.[StageOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsGroup]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsGroup]
    @GroupID int,
    @GroupOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@GroupOperationID IS NULL) OR (@GroupOperationID = GroupOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[GroupRef],parent_prms.[GroupOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCurriculum]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsCurriculum]
    @GroupID int,
    @CurriculumOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@CurriculumOperationID IS NULL) OR (@CurriculumOperationID = CurriculumOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CurriculumRef],parent_prms.[CurriculumOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCourse]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsCourse]
    @GroupID int,
    @CourseOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@CourseOperationID IS NULL) OR (@CourseOperationID = CourseOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CourseRef],parent_prms.[CourseOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionTheme]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_CheckPermissionTheme]
	@UserID int,
	@ThemeOperationID int,   
    @ThemeID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@ThemeID = ThemeRef AND
		@ThemeOperationID = ThemeOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR ('Not enough permission to perform this operation', 16, 16);
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionStage]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_CheckPermissionStage]
	@UserID int,
	@StageOperationID int,   
    @StageID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@StageID = StageRef AND
		@StageOperationID = StageOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR ('Not enough permission to perform this operation', 16, 16);
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionGroup]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_CheckPermissionGroup]
	@UserID int,
	@GroupOperationID int,   
    @GroupID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@GroupID = GroupRef AND
		@GroupOperationID = GroupOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR ('Not enough permission to perform this operation', 16, 16);
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCurriculum]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_CheckPermissionCurriculum]
	@UserID int,
	@CurriculumOperationID int,   
    @CurriculumID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@CurriculumID = CurriculumRef AND
		@CurriculumOperationID = CurriculumOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR ('Not enough permission to perform this operation', 16, 16);
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCourse]    Script Date: 01/28/2010 01:27:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Security_CheckPermissionCourse]
	@UserID int,
	@CourseOperationID int,   
    @CourseID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@CourseID = CourseRef AND
		@CourseOperationID = CourseOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR ('Not enough permission to perform this operation', 16, 16);
END
GO
/****** Object:  Default [DF__fxThemeOp__sysSt__6B24EA82]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxThemeOperations] ADD  CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxStageOp__sysSt__6C190EBB]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxStageOperations] ADD  CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxSampleB__sysSt__6A30C649]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] ADD  CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxRoles__sysStat__6D0D32F4]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxRoles] ADD  CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxPageTyp__sysSt__6E01572D]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxPageTypes] ADD  CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxPageOrd__sysSt__6EF57B66]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxPageOrders] ADD  CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxPageOpe__sysSt__6FE99F9F]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxPageOperations] ADD  CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxLanguag__sysSt__70DDC3D8]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxLanguages] ADD  CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxGroupOp__sysSt__03F0984C]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxGroupOperations] ADD  CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxCurricu__sysSt__71D1E811]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxCurriculumOperations] ADD  CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxCourseO__sysSt__72C60C4A]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxCourseOperations] ADD  CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxCompile__sysSt__73BA3083]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxCompiledStatuses] ADD  CONSTRAINT [DF__fxCompile__sysSt__73BA3083]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__fxAnswerT__sysSt__0C85DE4D]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[fxAnswerType] ADD  CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblGroups__sysSt__693CA210]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF__tblGroups__sysSt__693CA210]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblCurric__sysSt__74AE54BC]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCurriculums] ADD  CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblCourse__sysSt__75A278F5]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCourses] ADD  CONSTRAINT [DF__tblCourse__sysSt__75A278F5]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF_tblLearnerAttempts_sysState]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblLearnerAttempts] ADD  CONSTRAINT [DF_tblLearnerAttempts_sysState]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblSample__sysSt__68487DD7]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblSampleBusinesObject] ADD  CONSTRAINT [DF__tblSample__sysSt__68487DD7]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF_tblAttemptsVars_sysState]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblVars] ADD  CONSTRAINT [DF_tblAttemptsVars_sysState]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblUsers__sysSta__6754599E]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF__tblUsers__sysSta__6754599E]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblStages__sysSt__787EE5A0]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblStages] ADD  CONSTRAINT [DF__tblStages__sysSt__787EE5A0]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF_tblResources_sysState]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblResources] ADD  CONSTRAINT [DF_tblResources_sysState]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblPages__sysSta__7C4F7684]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPages] ADD  CONSTRAINT [DF__tblPages__sysSta__7C4F7684]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF_tblOrganizations_sysState]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblOrganizations] ADD  CONSTRAINT [DF_tblOrganizations_sysState]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblCompil__sysSt__778AC167]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledQuestions] ADD  CONSTRAINT [DF__tblCompil__sysSt__778AC167]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__relUserRo__sysSt__02084FDA]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[relUserRoles] ADD  CONSTRAINT [DF__relUserRo__sysSt__02084FDA]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__relUserGr__sysSt__02FC7413]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[relUserGroups] ADD  CONSTRAINT [DF__relUserGr__sysSt__02FC7413]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblCompil__sysSt__7D439ABD]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledQuestionsData] ADD  CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF_tblItems_sysState]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblItems] ADD  CONSTRAINT [DF_tblItems_sysState]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblQuesti__sysSt__7E37BEF6]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblQuestions] ADD  CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblThemes__sysSt__797309D9]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__sysSt__797309D9]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblThemes__PageC__06CD04F7]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__PageC__06CD04F7]  DEFAULT (NULL) FOR [PageCountToShow]
GO
/****** Object:  Default [DF__tblThemes__MaxCo__07C12930]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__MaxCo__07C12930]  DEFAULT (NULL) FOR [MaxCountToSubmit]
GO
/****** Object:  Default [DF__tblUserAn__sysSt__01142BA1]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblUserAn__Answe__0D7A0286]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]  DEFAULT ((1)) FOR [AnswerTypeRef]
GO
/****** Object:  Default [DF__tblPermis__sysSt__7B5B524B]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions] ADD  CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF_tblAttempts_sysState]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblLearnerSessions] ADD  CONSTRAINT [DF_tblAttempts_sysState]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblCompil__sysSt__76969D2E]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__sysSt__76969D2E]  DEFAULT ((0)) FOR [sysState]
GO
/****** Object:  Default [DF__tblCompil__UserA__04E4BC85]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__UserA__04E4BC85]  DEFAULT ((0)) FOR [UserAnswerRef]
GO
/****** Object:  Default [DF__tblCompil__Compi__08B54D69]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__Compi__08B54D69]  DEFAULT ((0)) FOR [CompiledQuestionsDataRef]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblStages]  WITH CHECK ADD  CONSTRAINT [FK_Curriculums_Stages] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
ALTER TABLE [dbo].[tblStages] CHECK CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblResources_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblResources] CHECK CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPages]  WITH CHECK ADD  CONSTRAINT [FK_Page_PageType] FOREIGN KEY([PageTypeRef])
REFERENCES [dbo].[fxPageTypes] ([ID])
GO
ALTER TABLE [dbo].[tblPages] CHECK CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_tblOrganizations_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblOrganizations] CHECK CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages] FOREIGN KEY([LanguageRef])
REFERENCES [dbo].[fxLanguages] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledQuestions] CHECK CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_ID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[fxRoles] ([ID])
GO
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_GROUP]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_GROUP] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_USER] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledQuestionsData]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledQuestionsData] CHECK CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblItems] FOREIGN KEY([PID])
REFERENCES [dbo].[tblItems] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblOrganizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_CorrectAnswer_Page] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestions_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Themes] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Stages_Themes] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Themes_Course] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef] FOREIGN KEY([AnswerTypeRef])
REFERENCES [dbo].[fxAnswerType] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_CorrectAnswer] FOREIGN KEY([QuestionRef])
REFERENCES [dbo].[tblQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswers_Users] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswers_Users]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PARENT_PERMITION] FOREIGN KEY([ParentPermitionRef])
REFERENCES [dbo].[tblPermissions] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CourseOperations] FOREIGN KEY([CourseOperationRef])
REFERENCES [dbo].[fxCourseOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Courses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CurriculumOperations] FOREIGN KEY([CurriculumOperationRef])
REFERENCES [dbo].[fxCurriculumOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Curriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupObjects] FOREIGN KEY([GroupObjectRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupOperations] FOREIGN KEY([GroupOperationRef])
REFERENCES [dbo].[fxGroupOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Groups] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Organizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerGroup] FOREIGN KEY([OwnerGroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerUser] FOREIGN KEY([OwnerUserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PageOperations] FOREIGN KEY([PageOperationRef])
REFERENCES [dbo].[fxPageOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Pages] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_StageOperations] FOREIGN KEY([StageOperationRef])
REFERENCES [dbo].[fxStageOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_ThemeOperations] FOREIGN KEY([ThemeOperationRef])
REFERENCES [dbo].[fxThemeOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Themes] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_UserObjects] FOREIGN KEY([UserObjectRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_tblItems_tblLearnerSessions]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblLearnerSessions] FOREIGN KEY([ItemRef])
REFERENCES [dbo].[tblItems] ([ID])
GO
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblItems_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblLearnerAttempts_tblLearnerSessions]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions] FOREIGN KEY([LearnerAttemptRef])
REFERENCES [dbo].[tblLearnerAttempts] ([ID])
GO
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses] FOREIGN KEY([StatusRef])
REFERENCES [dbo].[fxCompiledStatuses] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData] FOREIGN KEY([CompiledQuestionsDataRef])
REFERENCES [dbo].[tblCompiledQuestionsData] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 01/28/2010 01:27:01 ******/
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers] FOREIGN KEY([UserAnswerRef])
REFERENCES [dbo].[tblUserAnswers] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
