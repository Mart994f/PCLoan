CREATE PROCEDURE [dbo].[GetUserIdByName]
	@name varchar(16)
AS
	SELECT Id FROM [User] WHERE UserName = @name;