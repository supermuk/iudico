ALTER PROC [dbo].[spThemesInsert] 
    @ID int,
    @CourseRef int,
    @IsControl bit,
    @Name nvarchar(50),
    @PageOrderRef int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblThemes] ([ID], [CourseRef], [IsControl], [Name], [PageOrderRef])
	SELECT @ID, @CourseRef, @IsControl, @Name, @PageOrderRef
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [CourseRef], [IsControl], [Name], [PageOrderRef]
	FROM   [dbo].[tblThemes]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
