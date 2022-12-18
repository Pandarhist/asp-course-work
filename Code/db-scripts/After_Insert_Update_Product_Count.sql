USE "MusicShopASP"

GO

--DROP TRIGGER After_Insert_Update_Product_Count

CREATE TRIGGER After_Insert_Update_Product_Count
ON [ShoppingCarts]
AFTER INSERT, UPDATE
AS
    BEGIN
        DECLARE @Order           AS INT,
                @TotalCost       AS MONEY,
                @CancelledStatus AS INT,
                @CurrentStatus   AS INT
        SET @Order = (SELECT TOP(1) [sc_order] FROM inserted)
        PRINT(@Order)
        SET @CancelledStatus = (SELECT [os_id] 
                                  FROM [OrderStatuses]
                                 WHERE [os_name] = 'Отменён')
        SET @CurrentStatus = (SELECT [o_status]
                                FROM [Orders]
                               WHERE [o_id] = @Order)
        IF (@CancelledStatus = @CurrentStatus)
		  BEGIN
			ROLLBACK
		  END
        SET @TotalCost = (SELECT SUM(P.[p_price])
                            FROM [Products] P
                                 INNER JOIN [ShoppingCarts] SC
                                 ON P.[p_number] = SC.[sc_product]
                           WHERE SC.[sc_order] = @Order)
        Print('TotalCost:' + CONVERT(VARCHAR, @TotalCost))
        UPDATE [Orders]
           SET [o_total_cost] = @TotalCost
         WHERE [o_id] = @Order
    END;