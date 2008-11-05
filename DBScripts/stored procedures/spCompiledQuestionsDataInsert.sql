ALTER PROC [dbo].[spCompiledQuestionsDataInsert] 
    @ID int,
    @CompiledQuestionRef int,
    @Input nvarchar(MAX),
    @Output nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblCompiledQuestionsData] ([ID], [CompiledQuestionRef], [Input], [Output])
	SELECT @ID, @CompiledQuestionRef, @Input, @Output
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CompiledQuestionRef], [Input], [Output]
	FROM   [dbo].[tblCompiledQuestionsData]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT