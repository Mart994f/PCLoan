CREATE TABLE [dbo].[LoanTable]
(
	[ID] INT NOT NULL, 
    [UserID] INT NOT NULL, 
    [PCID] INT NOT NULL, 
    [Loan] DATETIME NULL, 
    [Return] DATETIME NULL,
        PRIMARY KEY (ID),
    	CONSTRAINT FK_UserLoanTable
	FOREIGN KEY (UserID)
	REFERENCES [User] (ID),
        CONSTRAINT FK_ComputerLoanTable
    FOREIGN KEY (PCID)
    REFERENCES Computer(ID)
    
)
