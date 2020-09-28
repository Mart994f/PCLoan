CREATE PROCEDURE [dbo].[GetAllComputersWithCurrentLoan]
	
AS
	SELECT Computer.Id, Computer.Name, Computer.StateId, U.UserName, L.LoanDate, L.ReturnedDate
	FROM Computer
	FULL JOIN [Loan] L on Computer.Id = L.ComputerId
    FULL JOIN [User] U on U.Id = L.UserId
    INNER JOIN State S on S.Id = Computer.StateId
	WHERE Computer.Deactivated = 0;