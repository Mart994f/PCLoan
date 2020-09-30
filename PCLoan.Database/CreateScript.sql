USE PCLoan
GO

CREATE TABLE [dbo].[Computer] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (32) NOT NULL,
    [StateId]     INT          NOT NULL,
    [Deactivated] BIT          NOT NULL,
    CONSTRAINT [PK_Computer] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Loan] (
    [Id]           INT  IDENTITY (1, 1) NOT NULL,
    [UserId]       INT  NOT NULL,
    [ComputerId]   INT  NOT NULL,
    [LoanDate]     DATE NOT NULL,
    [ReturnedDate] DATE NULL,
    CONSTRAINT [PK_Loan] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Log] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [TimeStamp]   DATETIME2 (7) NOT NULL,
    [UserId]      INT           NOT NULL,
    [ComputerId]  INT           NULL,
    [Description] VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[State] (
    [Id]    INT          IDENTITY (1, 1) NOT NULL,
    [State] VARCHAR (32) NOT NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[User] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [UserName] VARCHAR (16) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

ALTER TABLE [dbo].[Loan]
    ADD DEFAULT GETDATE() FOR [LoanDate];
GO

ALTER TABLE [dbo].[Computer]
    ADD CONSTRAINT [FK_StateComputer] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id]);
GO

ALTER TABLE [dbo].[Loan]
    ADD CONSTRAINT [FK_UserLoanTable] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]);
GO

ALTER TABLE [dbo].[Loan]
    ADD CONSTRAINT [FK_ComputerLoanTable] FOREIGN KEY ([ComputerId]) REFERENCES [dbo].[Computer] ([Id]);
GO

ALTER TABLE [dbo].[Log]
    ADD CONSTRAINT [FK_UserLog] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]);
GO

ALTER TABLE [dbo].[Log]
    ADD CONSTRAINT [FK_ComputerLog] FOREIGN KEY ([ComputerId]) REFERENCES [dbo].[Computer] ([Id]);
GO

CREATE PROCEDURE [dbo].[GetAllComputersWithCurrentLoan]
	
AS
	SELECT Computer.Id, Computer.Name, Computer.StateId, U.UserName, L.LoanDate, L.ReturnedDate
	FROM Computer
	FULL JOIN [Loan] L on Computer.Id = L.ComputerId
    FULL JOIN [User] U on U.Id = L.UserId
    INNER JOIN State S on S.Id = Computer.StateId
	WHERE Computer.Deactivated = 0;
GO

CREATE PROCEDURE [dbo].[GetAvailableComputers]

AS
	SELECT * FROM Computer WHERE Computer.StateID = 1;
RETURN 0
GO

CREATE PROCEDURE [dbo].[GetLentComputer]
	@username VARCHAR(16)
AS
	SELECT TOP(1) Loan.Id as Id, Computer.Name as Name FROM Loan
	INNER JOIN Computer ON Loan.ComputerId = Computer.ID
	WHERE Loan.UserId = (SELECT [User].ID FROM [User] WHERE [User].Username = @username) AND Computer.StateID = 2
	ORDER BY Loan.LoanDate DESC;
RETURN 0
GO

CREATE PROCEDURE [dbo].[GetUserIdByName]
	@name varchar(16)
AS
	SELECT Id FROM [User] WHERE Username = @name;
GO

CREATE PROCEDURE [dbo].[GetUsernameById]
	@Id int
AS
	SELECT [User].UserName FROM [User] WHERE [User].Id = @Id;
GO

CREATE PROCEDURE [dbo].[UserExist]
	@Name varchar(16)
AS
IF EXISTS (SELECT * FROM [User] WHERE Username = @name)
	BEGIN
		SELECT 1
	END
ELSE
	BEGIN
		SELECT 0
	END
GO

INSERT INTO PCLoan.dbo.State (State) VALUES ('Klar til udlån');
INSERT INTO PCLoan.dbo.State (State) VALUES ('Udlånt');
INSERT INTO PCLoan.dbo.State (State) VALUES ('Defekt');
INSERT INTO PCLoan.dbo.State (State) VALUES ('Til reparation');
GO