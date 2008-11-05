ALTER PROC [dbo].[spFilesInsert] 
    @ID int,
    @File varbinary(MAX) = NULL,
    @IsDirectory bit,
    @Name nvarchar(50),
    @PageRef int = NULL,
    @PID int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblFiles] ([ID], [File], [IsDirectory], [Name], [PageRef], [PID])
	SELECT @ID, @File, @IsDirectory, @Name, @PageRef, @PID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [File], [IsDirectory], [Name], [PageRef], [PID]
	FROM   [dbo].[tblFiles]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT