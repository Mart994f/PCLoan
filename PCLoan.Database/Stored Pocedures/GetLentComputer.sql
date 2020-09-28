CREATE PROCEDURE [dbo].[GetLentComputer]
	@username VARCHAR(16)
AS
	SELECT TOP(1) Loan.Id as Id, Computer.Name as Name FROM Loan
	INNER JOIN Computer ON Loan.ComputerId = Computer.ID
	WHERE Loan.UserId = (SELECT [User].ID FROM [User] WHERE [User].Username = @username) AND Computer.StateID = 2
	ORDER BY Loan.LoanDate DESC;
RETURN 0
