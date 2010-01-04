CREATE PROCEDURE GetUserRoles
	@UserLogin nvarchar(max)
AS
BEGIN
	select rs.Name from relUserRoles us
	LEFT JOIN fxRoles rs ON us.RoleID = rs.ID
	WHERE UserID = (SELECT ID FROM tblUsers where [Login] = @UserLogin)
END
