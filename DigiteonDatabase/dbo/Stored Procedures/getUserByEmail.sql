
CREATE PROCEDURE getUserByEmail( 
	@stUserEmail NVARCHAR(200) 
) 
AS 
BEGIN
	SELECT U.inUserId,U.unUserId,U.inStatus,U.inRole,U.stEmail,U.stMobile,U.stPassword,U.stUsername
	FROM tblUser U
	WHERE U.stEmail=@stUserEmail 
END 