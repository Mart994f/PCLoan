CREATE PROCEDURE [dbo].[DeavtivateComputer]
	@Id INT
AS
	UPDATE Computer SET Deactivated = 1 WHERE ID = @id;
RETURN 0
