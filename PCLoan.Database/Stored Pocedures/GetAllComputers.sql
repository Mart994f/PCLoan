CREATE PROCEDURE [dbo].[GetAllComputers]
	
AS
	SELECT Computer.id as Id, name as Name, Computer.StateID as SelectedState, Username as LendBy, Loan as LoanDate, [Return] as ReturnDate
	FROM Computer
	FULL JOIN LoanTable LT on Computer.ID = LT.PCID
    FULL JOIN [User] U on U.ID = LT.UserID
    INNER JOIN State S on S.ID = Computer.StateID
	WHERE Computer.Deactivated = 0;
RETURN 0
