ALTER PROC [dbo].[spPagesInsert] 
    @ID int,
    @PageFile varbinary(MAX),
    @PageName nvarchar(50),
    @PageRank int = NULL,
    @PageTypeRef int,
    @ThemeRef int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblPages] ([ID], [PageFile], [PageName], [PageRank], [PageTypeRef], [ThemeRef])
	SELECT @ID, @PageFile, @PageName, @PageRank, @PageTypeRef, @ThemeRef
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [PageFile], [PageName], [PageRank], [PageTypeRef], [ThemeRef]
	FROM   [dbo].[tblPages]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT