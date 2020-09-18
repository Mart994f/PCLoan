CREATE TABLE [dbo].[Computer]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NCHAR(20) NOT NULL, 
    [StateID] INT NOT NULL,
    [Deactivated] BIT NOT NULL, 
    CONSTRAINT FK_StateComputer
    FOREIGN KEY (StateID)
    REFERENCES [State]([ID])
)
