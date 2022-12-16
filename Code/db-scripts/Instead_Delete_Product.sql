USE "MusicShopASP"

GO

CREATE TRIGGER Instead_Delete_Product
ON [Products]
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE [Product]
           SET [p_is_deleted] = 1
         WHERE [p_product_number] = (SELECT [p_product_number]
                                       FROM deleted)
	END;
