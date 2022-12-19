USE "MusicShopASP"

GO

CREATE TRIGGER After_Cancelling_Order
ON [Orders]
AFTER UPDATE
AS
	BEGIN
	    DECLARE @Order           AS INT,
              @CancelledStatus AS INT,
              @CurrentStatus   AS INT
        SET @Order = (SELECT [o_id] FROM inserted)
        SET @CancelledStatus = (SELECT [os_id] 
                                  FROM [OrderStatuses]
                                 WHERE [os_name] = 'Отменён')
        SET @CurrentStatus = (SELECT [o_status]
                                FROM [Orders]
                               WHERE [o_id] = @Order)
        IF (@CancelledStatus = @CurrentStatus)
           BEGIN
             DELETE
               FROM [ShoppingCarts]
              WHERE [sc_order] = @Order
           END
    END;
