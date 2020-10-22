﻿CREATE PROCEDURE [dbo].[UserExist]
	@Name varchar(16)
AS
IF EXISTS (SELECT * FROM [User] WHERE Username = @Name)
	BEGIN
		SELECT 1
	END
ELSE
	BEGIN
		SELECT 0
	END
