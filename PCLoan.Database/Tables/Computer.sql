CREATE TABLE [dbo].[Computer]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] VARCHAR(32) NOT NULL, 
    [StateId] INT NOT NULL,
    [Deactivated] BIT NOT NULL, 
    CONSTRAINT FK_StateComputer
    FOREIGN KEY (StateId)
    REFERENCES [State]([Id])
)
