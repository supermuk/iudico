ALTER PROC [dbo].[spCoursesInsert] 
    @ID int,
    @Description nvarchar(MAX),
    @Name nvarchar(50),
    @UploadDate datetime,
    @Version int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[tblCourses] ([ID], [Description], [Name], [UploadDate], [Version])
	SELECT @ID, @Description, @Name, @UploadDate, @Version
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [Description], [Name], [UploadDate], [Version]
	FROM   [dbo].[tblCourses]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
