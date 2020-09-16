CREATE PROCEDURE [dbo].[CreateComputer]
	@name NCHAR(20),
	@state int
AS
	INSERT INTO Computer (Name, StateID, Deactivated) VALUES (@name, @state, 0);
RETURN 0
