ALTER PROC [dbo].[spCompiledQuestionsInsert] 
    @ID int,
    @LanguageRef int,
    @MemoryLimit int,
    @OutputLimit int,
    @TimeLimit int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblCompiledQuestions] ([ID], [LanguageRef], [MemoryLimit], [OutputLimit], [TimeLimit])
	SELECT @ID, @LanguageRef, @MemoryLimit, @OutputLimit, @TimeLimit
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [LanguageRef], [MemoryLimit], [OutputLimit], [TimeLimit]
	FROM   [dbo].[tblCompiledQuestions]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
