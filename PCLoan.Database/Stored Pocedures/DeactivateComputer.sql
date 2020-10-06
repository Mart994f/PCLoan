CREATE PROCEDURE [dbo].[DeactivateComputer]
	@id int
AS
	UPDATE Computer
	SET Deactivated = 1
	WHERE Id = @id;
RETURN 0
