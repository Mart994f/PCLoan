﻿CREATE TABLE [dbo].[Log]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[TimeStamp] DATETIME2 NOT NULL,
	[UserId] INT NOT NULL,
	[ComputerId] INT NULL,
	[Description] VARCHAR(128) NOT NULL,
	CONSTRAINT PK_Log
	PRIMARY KEY (Id),
	CONSTRAINT FK_UserLog
	FOREIGN KEY (UserId)
    REFERENCES [User]([Id]),
	CONSTRAINT FK_ComputerLog
	FOREIGN KEY (ComputerId)
    REFERENCES [Computer]([Id])
)
