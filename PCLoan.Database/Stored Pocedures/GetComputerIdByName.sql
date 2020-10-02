CREATE PROCEDURE [dbo].[GetComputerIdByName]
	@name VARCHAR
AS
	SELECT Computer.Id FROM Computer WHERE Computer.Name = @name;