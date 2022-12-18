USE "MusicShopASP"

GO

CREATE TRIGGER Instead_Delete_Employee
ON [Staff]
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE [Staff]
           SET [s_is_fired] = 1
         WHERE [s_personnel_number] = (SELECT [s_personnel_number] FROM deleted)
	  END;