﻿CREATE TABLE [dbo].[State]
(
    [Id] INT IDENTITY(1,1) NOT NULL, 
    [State] VARCHAR(32) NOT NULL,
    CONSTRAINT PK_State
    PRIMARY KEY (Id)
)