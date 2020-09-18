CREATE PROCEDURE [dbo].[AddUser]
	@name NCHAR(15)
AS
	INSERT INTO [User] ([Username]) VALUES (@name);
RETURN 0
