CREATE PROCEDURE [dbo].[ReturnLoan]
	@loanId int,
	@returnDate date
AS
	UPDATE Loan
	SET ReturnedDate = @returnDate
	WHERE Id = @loanId
