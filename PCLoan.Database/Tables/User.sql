﻿CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [UserName] VARCHAR(16) NOT NULL,
	CONSTRAINT PK_User
	PRIMARY KEY (Id)
)
