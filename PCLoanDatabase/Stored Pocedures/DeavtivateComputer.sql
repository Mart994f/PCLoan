CREATE PROCEDURE [dbo].[DeavtivateComputer]
	@name NCHAR(20)
AS
	UPDATE Computer SET Deactivated = 1 WHERE Name = @name;
RETURN 0
