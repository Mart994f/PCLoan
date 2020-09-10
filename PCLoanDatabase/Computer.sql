CREATE TABLE [dbo].[Computer]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(20) NOT NULL, 
    [StateID] INT NOT NULL,
        PRIMARY KEY (ID),
        CONSTRAINT FK_StateComputer
    FOREIGN KEY (StateID)
    REFERENCES [State](ID)
)
