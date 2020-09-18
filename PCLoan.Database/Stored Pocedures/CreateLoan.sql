CREATE PROCEDURE [dbo].[CreateLoan]
	@pcId int,
	@user NCHAR(15)
AS
	INSERT INTO LoanTable (PCID, UserID, Loan) VALUES (@pcId, (SELECT [User].ID FROM [User] WHERE [User].Username = @user), GETDATE())
RETURN 0
