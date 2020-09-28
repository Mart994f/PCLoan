CREATE PROCEDURE [dbo].[GetUsernameById]
	@Id int
AS
	SELECT [User].UserName FROM [User] WHERE [User].Id = @Id;
