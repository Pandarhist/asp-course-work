USE "MusicShopASP"

GO

CREATE TRIGGER Instead_Delete_Product
ON [Products]
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE [Products]
           SET [p_is_deleted] = 1
         WHERE [p_number] = (SELECT [p_number] FROM deleted)
	END;
