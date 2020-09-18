CREATE PROCEDURE [dbo].[ReturnLoan]
	@loanID int
AS
	UPDATE LoanTable SET [Return] = GETDATE() WHERE ID = @loanID;
RETURN 0
