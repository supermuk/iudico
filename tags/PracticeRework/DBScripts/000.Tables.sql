CREATE TABLE [tblUsers](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY  
(
	[ID] ASC
) 
)

CREATE TABLE [tblSampleBusinesObject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
UNIQUE  
(
	[ID] ASC
) 
) 

CREATE TABLE [tblGroups](
	[ID] [int] IDENTITY(1, 1)  NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxSampleBusinesObjectOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
UNIQUE  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxThemeOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
 CONSTRAINT [PK_ThemeOperations] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxStageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
 CONSTRAINT [PK_StageOperations] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxRoles](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_fxdRoles] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxPageTypes](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Type] [nvarchar](10) NULL,
 CONSTRAINT [PK_PageType] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxPageOrders](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](20) NULL,
 CONSTRAINT [PK_fxdPageOrders] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxPageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
 CONSTRAINT [PK_PageOperations] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxLanguages](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](20) NULL,
 CONSTRAINT [PK_fxdLanguages] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxCurriculumOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
 CONSTRAINT [PK_CurriculumOperations] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxCourseOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
 CONSTRAINT [PK_CourseOperations] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [fxCompiledStatuses](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_fxdCompiledStatuses] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [tblCurriculums](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_SdudyCourses] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [tblCourses](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UploadDate] [datetime] NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY  
(
	[ID] ASC
) 
) 
GO

CREATE PROCEDURE [spCoursesInsert] 
    @ID int,
    @Description nvarchar(MAX),
    @Name nvarchar(50),
    @UploadDate datetime,
    @Version int
AS 
	
	  
	
	BEGIN TRAN
	
	INSERT INTO [tblCourses] ([ID], [Description], [Name], [UploadDate], [Version])
	SELECT @ID, @Description, @Name, @UploadDate, @Version
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [Description], [Name], [UploadDate], [Version]
	FROM   [tblCourses]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
GO

CREATE TABLE [tblCompiledAnswers](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[TimeUsed] [int] NULL,
	[MemoryUsed] [int] NULL,
	[StatusRef] [int] NOT NULL,
 CONSTRAINT [PK_tblCompiledAnswers] PRIMARY KEY  
(
	[ID] ASC
) 
) 
go

CREATE TABLE [tblCompiledQuestions](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[LanguageRef] [int] NOT NULL,
	[TimeLimit] [int] NULL,
	[MemoryLimit] [int] NULL,
	[OutputLimit] [int] NULL,
 CONSTRAINT [PK_tblCompiledQuestions] PRIMARY KEY  
(
	[ID] ASC
) 
) 
go

CREATE TABLE [tblStages](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CurriculumRef] [int] NULL,
 CONSTRAINT [PK_Stages] PRIMARY KEY  
(
	[ID] ASC
) 
) 
GO
 
CREATE TABLE [tblThemes](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CourseRef] [int] NULL,
	[IsControl] [bit] NOT NULL,
	[PageOrderRef] [int] NULL,
 CONSTRAINT [PK_Chapter] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [relStagesThemes](
	[StageRef] [int] NOT NULL,
	[ThemeRef] [int] NOT NULL
) 
GO

ALTER TABLE relStagesThemes ADD CONSTRAINT PK_KEY PRIMARY KEY (StageRef, ThemeRef)
ALTER TABLE relStagesThemes ADD CONSTRAINT FK_Stage FOREIGN KEY (StageRef)
	REFERENCES tblStages(ID)
ALTER TABLE relStagesThemes ADD CONSTRAINT FK_THEME FOREIGN KEY (ThemeRef)
	REFERENCES tblThemes(ID)
GO

CREATE TABLE [tblPermissions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentPermitionRef] [int] NULL,
	[DateSince] [datetime] NULL,
	[DateTill] [datetime] NULL,
	[UserRef] [int] NULL,
	[GroupRef] [int] NULL,
	[CanBeDelagated] [bit] NOT NULL,
	[CourseRef] [int] NULL,
	[CourseOperationRef] [int] NULL,
	[CurriculumRef] [int] NULL,
	[CurriculumsOperationRef] [int] NULL,
	[StageRef] [int] NULL,
	[StageOperationRef] [int] NULL,
	[ThemeRef] [int] NULL,
	[ThemeOperationRef] [int] NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY  
(
	[ID] ASC
) 
) 
go

CREATE TABLE [tblPages](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[ThemeRef] [int] NULL,
	[PageTypeRef] [int] NULL,
	[PageRank] [int] NULL,
	[PageName] [nvarchar](50) NULL,
	[PageFile] [varbinary](max) NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [tblCompiledQuestionsData](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[CompiledQuestionRef] [int] NOT NULL,
	[Input] [nvarchar](max) NULL,
	[Output] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblCompiledQuestionsData] PRIMARY KEY  
(
	[ID] ASC
) 
) 
GO

CREATE PROCEDURE [spThemesInsert] 
    @ID int,
    @CourseRef int,
    @IsControl bit,
    @Name nvarchar(50),
    @PageOrderRef int = NULL
AS 
	
	  
	
	BEGIN TRAN
	
	INSERT INTO [tblThemes] ([ID], [CourseRef], [IsControl], [Name], [PageOrderRef])
	SELECT @ID, @CourseRef, @IsControl, @Name, @PageOrderRef
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CourseRef], [IsControl], [Name], [PageOrderRef]
	FROM   [tblThemes]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
GO

CREATE PROCEDURE [spCompiledQuestionsInsert] 
    @ID int,
    @LanguageRef int,
    @MemoryLimit int,
    @OutputLimit int,
    @TimeLimit int
AS 
	
	  
	
	BEGIN TRAN
	
	INSERT INTO [tblCompiledQuestions] ([ID], [LanguageRef], [MemoryLimit], [OutputLimit], [TimeLimit])
	SELECT @ID, @LanguageRef, @MemoryLimit, @OutputLimit, @TimeLimit
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [LanguageRef], [MemoryLimit], [OutputLimit], [TimeLimit]
	FROM   [tblCompiledQuestions]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
GO

CREATE PROCEDURE [spCompiledQuestionsDataInsert] 
    @ID int,
    @CompiledQuestionRef int,
    @Input nvarchar(MAX),
    @Output nvarchar(MAX)
AS 
	
	  
	
	BEGIN TRAN
	
	INSERT INTO [tblCompiledQuestionsData] ([ID], [CompiledQuestionRef], [Input], [Output])
	SELECT @ID, @CompiledQuestionRef, @Input, @Output
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CompiledQuestionRef], [Input], [Output]
	FROM   [tblCompiledQuestionsData]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT

GO
CREATE PROCEDURE [spPagesInsert] 
    @ID int,
    @PageFile varbinary(MAX),
    @PageName nvarchar(50),
    @PageRank int = NULL,
    @PageTypeRef int,
    @ThemeRef int
AS 
	
	  
	
	BEGIN TRAN
	
	INSERT INTO [tblPages] ([ID], [PageFile], [PageName], [PageRank], [PageTypeRef], [ThemeRef])
	SELECT @ID, @PageFile, @PageName, @PageRank, @PageTypeRef, @ThemeRef
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [PageFile], [PageName], [PageRank], [PageTypeRef], [ThemeRef]
	FROM   [tblPages]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT

GO
CREATE TABLE [tblQuestions](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[PageRef] [int] NULL,
	[TestName] [nvarchar](50) NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
	[Rank] [int] NULL,
	[IsCompiled] [bit] NOT NULL,
	[CompiledQuestionRef] [int] NULL,
 CONSTRAINT [PK_CorrectAnswer] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [tblFiles](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[PID] [int] NULL,
	[PageRef] [int] NULL,
	[File] [varbinary](max) NULL,
	[Name] [nvarchar](50) NULL,
	[IsDirectory] [bit] NULL,
 CONSTRAINT [PK_tblFiles] PRIMARY KEY  
(
	[ID] ASC
) 
) 

CREATE TABLE [tblCompiledAnswersData](
	[UserRef] [int] IDENTITY(1, 1) NOT NULL,
	[CompiledQuestionsDataRef] [int] NOT NULL,
	[Output] [nvarchar](max) NULL
) 

CREATE TABLE [tblUserAnswers](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[UserRef] [int] NULL,
	[QuestionRef] [int] NULL,
	[UserAnswer] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[IsCompiledAnswer] [bit] NOT NULL,
	[CompiledAnswerRef] [int] NULL,
 CONSTRAINT [PK_UserAnswer] PRIMARY KEY  
(
	[ID] ASC
) 
) 
GO

CREATE PROCEDURE [spFilesInsert] 
    @ID int,
    @File varbinary(MAX) = NULL,
    @IsDirectory bit,
    @Name nvarchar(50),
    @PageRef int = NULL,
    @PID int = NULL
AS 
	
	  
	
	BEGIN TRAN
	
	INSERT INTO [tblFiles] ([ID], [File], [IsDirectory], [Name], [PageRef], [PID])
	SELECT @ID, @File, @IsDirectory, @Name, @PageRef, @PID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [File], [IsDirectory], [Name], [PageRef], [PID]
	FROM   [tblFiles]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
	
GO
CREATE PROCEDURE [spQuestionsInsert] 
    @ID int,
    @CompiledQuestionRef int = NULL,
    @CorrectAnswer nvarchar(MAX) = NULL,
    @IsCompiled bit,
    @PageRef int,
    @Rank int,
    @TestName nvarchar(50)
AS 
	
	  
	
	BEGIN TRAN
	
	INSERT INTO [tblQuestions] ([ID], [CompiledQuestionRef], [CorrectAnswer], [IsCompiled], [PageRef], [Rank], [TestName])
	SELECT @ID, @CompiledQuestionRef, @CorrectAnswer, @IsCompiled, @PageRef, @Rank, @TestName
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CompiledQuestionRef], [CorrectAnswer], [IsCompiled], [PageRef], [Rank], [TestName]
	FROM   [tblQuestions]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT

GO

CREATE PROCEDURE [spUserAnswersInsert] 
    @ID int,
    @CompiledAnswerRef int = NULL,
    @Date datetime,
    @IsCompiledAnswer bit,
    @QuestionRef int,
    @UserAnswer nvarchar(MAX),
    @UserRef int
AS 
	
	  
	
	BEGIN TRAN
	
	INSERT INTO [tblUserAnswers] ([ID], [CompiledAnswerRef], [Date], [IsCompiledAnswer], [QuestionRef], [UserAnswer], [UserRef])
	SELECT @ID, @CompiledAnswerRef, @Date, @IsCompiledAnswer, @QuestionRef, @UserAnswer, @UserRef
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CompiledAnswerRef], [Date], [IsCompiledAnswer], [QuestionRef], [UserAnswer], [UserRef]
	FROM   [tblUserAnswers]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
GO