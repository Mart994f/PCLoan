CREATE PROCEDURE [dbo].[UpdateState]
	@computerId int,
	@stateId int
AS
	UPDATE Computer
	SET Computer.StateId = @stateId
	WHERE Computer.Id = @computerId