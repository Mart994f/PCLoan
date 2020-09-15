CREATE PROCEDURE [dbo].[DeavtivateComputer]
	@id NCHAR(20)
AS
	UPDATE Computer SET Deactivated = 1 WHERE ID = @id;
RETURN 0
