CREATE PROCEDURE [dbo].[AddComputer]
	@name NCHAR(20)
AS
	INSERT INTO Computer (Name, StateID, Deactivated) VALUES (@name, 1, 0);
RETURN 0
