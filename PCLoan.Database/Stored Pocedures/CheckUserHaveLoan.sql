CREATE PROCEDURE [dbo].[CheckUserHaveLoan]
	@userId int
AS
	IF EXISTS(SELECT * FROM Loan WHERE Loan.UserId = @userId AND ReturnedDate IS NULL)
	BEGIN
		SELECT 1
	END
ELSE
	BEGIN
		SELECT 0
	END
		
