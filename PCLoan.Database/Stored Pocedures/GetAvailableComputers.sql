CREATE PROCEDURE [dbo].[GetAvailableComputers]

AS
	SELECT * FROM Computer WHERE Computer.StateId = 1 AND Computer.Deactivated = 0;
RETURN 0
