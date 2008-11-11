USE [IUDICO]
GO
ALTER PROC [dbo].[spThemesInsert] 
    @CourseRef int,
    @IsControl bit,
    @Name nvarchar(50),
    @PageOrderRef int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblThemes] ([CourseRef], [IsControl], [Name], [PageOrderRef])
	SELECT @CourseRef, @IsControl, @Name, @PageOrderRef
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CourseRef], [IsControl], [Name], [PageOrderRef]
	FROM   [dbo].[tblThemes]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
ALTER PROC [dbo].[spQuestionsInsert] 
    @CompiledQuestionRef int = NULL,
    @CorrectAnswer nvarchar(MAX) = NULL,
    @IsCompiled bit,
    @PageRef int,
    @Rank int,
    @TestName nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblQuestions] ([CompiledQuestionRef], [CorrectAnswer], [IsCompiled], [PageRef], [Rank], [TestName])
	SELECT @CompiledQuestionRef, @CorrectAnswer, @IsCompiled, @PageRef, @Rank, @TestName
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CompiledQuestionRef], [CorrectAnswer], [IsCompiled], [PageRef], [Rank], [TestName]
	FROM   [dbo].[tblQuestions]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
ALTER PROC [dbo].[spPagesInsert] 
    @PageFile varbinary(MAX),
    @PageName nvarchar(50),
    @PageRank int = NULL,
    @PageTypeRef int,
    @ThemeRef int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblPages] ([PageFile], [PageName], [PageRank], [PageTypeRef], [ThemeRef])
	SELECT @PageFile, @PageName, @PageRank, @PageTypeRef, @ThemeRef
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [PageFile], [PageName], [PageRank], [PageTypeRef], [ThemeRef]
	FROM   [dbo].[tblPages]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
ALTER PROC [dbo].[spFilesInsert] 
    @File varbinary(MAX) = NULL,
    @IsDirectory bit,
    @Name nvarchar(50),
    @PageRef int,
    @PID int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblFiles] ([File], [IsDirectory], [Name], [PageRef], [PID])
	SELECT @File, @IsDirectory, @Name, @PageRef, @PID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [File], [IsDirectory], [Name], [PageRef], [PID]
	FROM   [dbo].[tblFiles]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
ALTER PROC [dbo].[spCoursesInsert] 
    @Description nvarchar(MAX),
    @Name nvarchar(50),
    @UploadDate datetime,
    @Version int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblCourses] ([Description], [Name], [UploadDate], [Version])
	SELECT @Description, @Name, @UploadDate, @Version
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [Description], [Name], [UploadDate], [Version]
	FROM   [dbo].[tblCourses]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
ALTER PROC [dbo].[spCompiledQuestionsDataInsert] 
    @CompiledQuestionRef int,
    @Input nvarchar(MAX),
    @Output nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblCompiledQuestionsData] ([CompiledQuestionRef], [Input], [Output])
	SELECT @CompiledQuestionRef, @Input, @Output
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CompiledQuestionRef], [Input], [Output]
	FROM   [dbo].[tblCompiledQuestionsData]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT

GO
ALTER PROC [dbo].[spCompiledQuestionsInsert] 
    @LanguageRef int,
    @MemoryLimit int,
    @OutputLimit int,
    @TimeLimit int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblCompiledQuestions] ([LanguageRef], [MemoryLimit], [OutputLimit], [TimeLimit])
	SELECT @LanguageRef, @MemoryLimit, @OutputLimit, @TimeLimit
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [LanguageRef], [MemoryLimit], [OutputLimit], [TimeLimit]
	FROM   [dbo].[tblCompiledQuestions]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
	
GO
CREATE PROC [dbo].[spFilesSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [File], [IsDirectory], [Name], [PageRef], [PID] 
	FROM   [dbo].[tblFiles] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
	
GO
CREATE PROC [dbo].[spFilesSelectId] 
    @PageRef INT,
    @Name nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID] 
	FROM   [dbo].[tblFiles] 
	WHERE  ([PageRef] = @PageRef AND [Name] = @Name) 

	COMMIT
	
	
GO
CREATE PROC [dbo].[spPagesSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [PageFile], [PageName], [PageRank], [PageTypeRef], [ThemeRef] 
	FROM   [dbo].[tblPages] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT