CREATE PROCEDURE [dbo].[CheckUserExists]
	@name NCHAR(15)
AS
	SELECT * FROM [User] WHERE [User].Username = @name;
RETURN 0