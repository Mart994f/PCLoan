CREATE PROCEDURE [dbo].[GetAvailableComputers]

AS
	SELECT * FROM Computer WHERE Computer.StateID = 1;
RETURN 0
