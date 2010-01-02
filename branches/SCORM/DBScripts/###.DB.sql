USE [IUDICO]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_fxdRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](10) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_PageType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxAnswerType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxAnswerType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_fxAnswerType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOrders]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_fxdPageOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_PageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxLanguages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxLanguages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_fxdLanguages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCurriculumOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_CurriculumOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCourseOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_CourseOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCompiledStatuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_fxdCompiledStatuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCurriculums]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCurriculums](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_SdudyCourses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCourses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCourses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UploadDate] [datetime] NULL,
	[Version] [int] NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSecurityID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE function [dbo].[GetSecurityID]()
RETURNS uniqueidentifier
AS
BEGIN
	RETURN ''9f962487-c176-46b8-8f77-b390683b06cc'';
END' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxSampleBusinesObjectOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxThemeOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_ThemeOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxStageOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxStageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_StageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblFiles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblFiles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](300) NOT NULL,
	[File] [varbinary](max) NOT NULL,
	[sysState] [int] NOT NULL CONSTRAINT [DF_tblFiles_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblFiles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxGroupOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_fxGroupOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_fxGroupOperations_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](32) NOT NULL,
	[PasswordHash] [char](32) NOT NULL,
	[Email] [char](50) NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_EMAIL] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSampleBusinesObject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPermissions]') AND type in (N'U'))
BEGIN
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
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relUserRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_USER_ROLE] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblPages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NULL,
	[PageTypeRef] [int] NULL,
	[PageRank] [int] NULL,
	[PageName] [nvarchar](50) NULL,
	[PageFile] [varchar](250) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblPages__sysSta__7C4F7684]  DEFAULT ((0)),
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUserAnswers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRef] [int] NULL,
	[QuestionRef] [int] NULL,
	[UserAnswer] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[IsCompiledAnswer] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
	[AnswerTypeRef] [int] NOT NULL DEFAULT ((1)),
 CONSTRAINT [PK_UserAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThemes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblThemes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CourseRef] [int] NULL,
	[IsControl] [bit] NOT NULL,
	[PageOrderRef] [int] NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
	[PageCountToShow] [int] NULL DEFAULT (NULL),
	[MaxCountToSubmit] [int] NULL DEFAULT (NULL),
 CONSTRAINT [PK_Chapter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCompiledQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LanguageRef] [int] NOT NULL,
	[TimeLimit] [int] NULL,
	[MemoryLimit] [int] NULL,
	[OutputLimit] [int] NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_tblCompiledQuestions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCompiledAnswers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TimeUsed] [int] NULL,
	[MemoryUsed] [int] NULL,
	[StatusRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
	[UserAnswerRef] [int] NOT NULL DEFAULT ((0)),
	[Output] [nvarchar](max) NULL,
	[CompiledQuestionsDataRef] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_tblCompiledAnswers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblStages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CurriculumRef] [int] NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResources]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblResources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Identifier] [nvarchar](300) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_tblResources_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblResources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOrganizations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblOrganizations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_tblOrganizations_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblOrganizations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCompiledQuestionsData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompiledQuestionRef] [int] NOT NULL,
	[Input] [nvarchar](max) NULL,
	[Output] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_tblCompiledQuestionsData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuestions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageRef] [int] NULL,
	[TestName] [nvarchar](50) NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
	[Rank] [int] NULL,
	[IsCompiled] [bit] NOT NULL,
	[CompiledQuestionRef] [int] NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_CorrectAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relStagesThemes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relStagesThemes](
	[StageRef] [int] NOT NULL,
	[ThemeRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_KEY] PRIMARY KEY CLUSTERED 
(
	[StageRef] ASC,
	[ThemeRef] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relResourcesFiles](
	[ResourceRef] [int] NOT NULL,
	[FileRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_relResourcesFiles_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_relResourcesFiles] PRIMARY KEY CLUSTERED 
(
	[ResourceRef] ASC,
	[FileRef] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relResourcesDependency](
	[DependantRef] [int] NOT NULL,
	[DependencyRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_relResourcesDependency_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_relResourcesDependency] PRIMARY KEY CLUSTERED 
(
	[DependantRef] ASC,
	[DependencyRef] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NULL,
	[OrganizationRef] [int] NOT NULL,
	[ResourceRef] [int] NULL,
	[Title] [nvarchar](200) NULL,
	[IsLeaf] [bit] NOT NULL,
	[sysState] [int] NOT NULL CONSTRAINT [DF_tblItems_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relUserGroups](
	[UserRef] [int] NOT NULL,
	[GroupRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_relUserGroups_KEY] PRIMARY KEY CLUSTERED 
(
	[UserRef] ASC,
	[GroupRef] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionGroup]
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
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsGroup]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCurriculum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForCurriculum]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCurriculum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsCurriculum]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsStage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsStage]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsGroup]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCurriculum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionCurriculum]
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
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCurriculum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsCurriculum]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCourse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionCourse]
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
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCourse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsCourse]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCourse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForCourse]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionStage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionStage]
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
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsStage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsStage]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForGroup]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForStage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForStage]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionTheme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionTheme]
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
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsTheme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsTheme]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsTheme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsTheme]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForTheme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForTheme]
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCourse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsCourse]
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
END' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PARENT_PERMITION] FOREIGN KEY([ParentPermitionRef])
REFERENCES [dbo].[tblPermissions] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_PARENT_PERMITION]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CourseOperations] FOREIGN KEY([CourseOperationRef])
REFERENCES [dbo].[fxCourseOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CourseOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Courses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Courses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CurriculumOperations] FOREIGN KEY([CurriculumOperationRef])
REFERENCES [dbo].[fxCurriculumOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Curriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Curriculums]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupObjects] FOREIGN KEY([GroupObjectRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupObjects]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupOperations] FOREIGN KEY([GroupOperationRef])
REFERENCES [dbo].[fxGroupOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Groups] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Groups]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerGroup] FOREIGN KEY([OwnerGroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerGroup]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerUser] FOREIGN KEY([OwnerUserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerUser]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PageOperations] FOREIGN KEY([PageOperationRef])
REFERENCES [dbo].[fxPageOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_PageOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Pages] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Pages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_StageOperations] FOREIGN KEY([StageOperationRef])
REFERENCES [dbo].[fxStageOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_StageOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Stages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_ThemeOperations] FOREIGN KEY([ThemeOperationRef])
REFERENCES [dbo].[fxThemeOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_ThemeOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Themes] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Themes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_UserObjects] FOREIGN KEY([UserObjectRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_UserObjects]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_ID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[fxRoles] ([ID])
GO
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_ROLE_ID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_USER_ID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages]  WITH CHECK ADD  CONSTRAINT [FK_Page_PageType] FOREIGN KEY([PageTypeRef])
REFERENCES [dbo].[fxPageTypes] ([ID])
GO
ALTER TABLE [dbo].[tblPages] CHECK CONSTRAINT [FK_Page_PageType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_Theme]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages]  WITH CHECK ADD  CONSTRAINT [FK_Page_Theme] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
ALTER TABLE [dbo].[tblPages] CHECK CONSTRAINT [FK_Page_Theme]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef] FOREIGN KEY([AnswerTypeRef])
REFERENCES [dbo].[fxAnswerType] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_CorrectAnswer] FOREIGN KEY([QuestionRef])
REFERENCES [dbo].[tblQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswers_Users] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswers_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chapter_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Chapter_Course] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Chapter_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblThemes_fxdPageOrders]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_tblThemes_fxdPageOrders] FOREIGN KEY([PageOrderRef])
REFERENCES [dbo].[fxPageOrders] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_tblThemes_fxdPageOrders]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages] FOREIGN KEY([LanguageRef])
REFERENCES [dbo].[fxLanguages] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledQuestions] CHECK CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses] FOREIGN KEY([StatusRef])
REFERENCES [dbo].[fxCompiledStatuses] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData] FOREIGN KEY([CompiledQuestionsDataRef])
REFERENCES [dbo].[tblCompiledQuestionsData] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers] FOREIGN KEY([UserAnswerRef])
REFERENCES [dbo].[tblUserAnswers] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblStages_tblCurriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages]  WITH CHECK ADD  CONSTRAINT [FK_tblStages_tblCurriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
ALTER TABLE [dbo].[tblStages] CHECK CONSTRAINT [FK_tblStages_tblCurriculums]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblResources_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblResources] CHECK CONSTRAINT [FK_tblResources_tblCourses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_tblOrganizations_tblCourses] FOREIGN KEY([CourseID])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblOrganizations] CHECK CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledQuestionsData] CHECK CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_CorrectAnswer_Page] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_CorrectAnswer_Page]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestions_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stage]') AND parent_object_id = OBJECT_ID(N'[dbo].[relStagesThemes]'))
ALTER TABLE [dbo].[relStagesThemes]  WITH CHECK ADD  CONSTRAINT [FK_Stage] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
ALTER TABLE [dbo].[relStagesThemes] CHECK CONSTRAINT [FK_Stage]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Theme]') AND parent_object_id = OBJECT_ID(N'[dbo].[relStagesThemes]'))
ALTER TABLE [dbo].[relStagesThemes]  WITH CHECK ADD  CONSTRAINT [FK_Theme] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
ALTER TABLE [dbo].[relStagesThemes] CHECK CONSTRAINT [FK_Theme]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesFiles_tblFiles]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]'))
ALTER TABLE [dbo].[relResourcesFiles]  WITH CHECK ADD  CONSTRAINT [FK_relResourcesFiles_tblFiles] FOREIGN KEY([FileRef])
REFERENCES [dbo].[tblFiles] ([ID])
GO
ALTER TABLE [dbo].[relResourcesFiles] CHECK CONSTRAINT [FK_relResourcesFiles_tblFiles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesFiles_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]'))
ALTER TABLE [dbo].[relResourcesFiles]  WITH CHECK ADD  CONSTRAINT [FK_relResourcesFiles_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[relResourcesFiles] CHECK CONSTRAINT [FK_relResourcesFiles_tblResources]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesDependency_tblResources_Dependant]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]'))
ALTER TABLE [dbo].[relResourcesDependency]  WITH CHECK ADD  CONSTRAINT [FK_relResourcesDependency_tblResources_Dependant] FOREIGN KEY([DependantRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[relResourcesDependency] CHECK CONSTRAINT [FK_relResourcesDependency_tblResources_Dependant]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesDependency_tblResources_Dependency]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]'))
ALTER TABLE [dbo].[relResourcesDependency]  WITH CHECK ADD  CONSTRAINT [FK_relResourcesDependency_tblResources_Dependency] FOREIGN KEY([DependencyRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[relResourcesDependency] CHECK CONSTRAINT [FK_relResourcesDependency_tblResources_Dependency]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblItems] FOREIGN KEY([PID])
REFERENCES [dbo].[tblItems] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblItems]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblOrganizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblOrganizations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblResources]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_GROUP] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_GROUP]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_USER] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_USER]
