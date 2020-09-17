CREATE PROCEDURE [dbo].[GetLentComputer]
	@username NCHAR(15)
AS
	SELECT TOP(1) LoanTable.ID as Id, Computer.Name as Name FROM LoanTable
	INNER JOIN Computer ON LoanTable.PCID = Computer.ID
	WHERE LoanTable.UserID = (SELECT [User].ID FROM [User] WHERE [User].Username = 'mort286f') AND Computer.StateID = 2
	ORDER BY LoanTable.Loan DESC;
RETURN 0
