ALTER PROC [dbo].[spUserAnswersInsert] 
    @ID int,
    @CompiledAnswerRef int = NULL,
    @Date datetime,
    @IsCompiledAnswer bit,
    @QuestionRef int,
    @UserAnswer nvarchar(MAX),
    @UserRef int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblUserAnswers] ([ID], [CompiledAnswerRef], [Date], [IsCompiledAnswer], [QuestionRef], [UserAnswer], [UserRef])
	SELECT @ID, @CompiledAnswerRef, @Date, @IsCompiledAnswer, @QuestionRef, @UserAnswer, @UserRef
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CompiledAnswerRef], [Date], [IsCompiledAnswer], [QuestionRef], [UserAnswer], [UserRef]
	FROM   [dbo].[tblUserAnswers]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT