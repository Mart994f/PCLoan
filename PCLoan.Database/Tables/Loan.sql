﻿CREATE TABLE [dbo].[Loan]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [UserId] INT NOT NULL, 
    [ComputerId] INT NOT NULL, 
    [LoanDate] DATE NOT NULL DEFAULT GETDATE(), 
    [ReturnedDate] DATE NULL,
    CONSTRAINT PK_Loan
	PRIMARY KEY (Id),
    CONSTRAINT FK_UserLoanTable
	FOREIGN KEY (UserId)
	REFERENCES [User] (Id),
    CONSTRAINT FK_ComputerLoanTable
    FOREIGN KEY (ComputerId)
    REFERENCES Computer(Id)
    
)
