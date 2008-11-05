ALTER PROC [dbo].[spQuestionsInsert] 
    @ID int,
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
	
	INSERT INTO [dbo].[tblQuestions] ([ID], [CompiledQuestionRef], [CorrectAnswer], [IsCompiled], [PageRef], [Rank], [TestName])
	SELECT @ID, @CompiledQuestionRef, @CorrectAnswer, @IsCompiled, @PageRef, @Rank, @TestName
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CompiledQuestionRef], [CorrectAnswer], [IsCompiled], [PageRef], [Rank], [TestName]
	FROM   [dbo].[tblQuestions]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT