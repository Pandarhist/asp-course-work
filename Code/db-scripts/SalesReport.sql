-- SQL-представление отчёта по продажам из контроллера OrdersController

SELECT P.[p_number] AS ProductNumber,
	   P.[p_name] AS ProductName,
	   SUM(SC.[sc_count]) AS SalesCount,
	   P.[p_price] AS Price,
	   SUM([sc_count]) * P.[p_price] AS Gain
  FROM [Products] P
	   INNER JOIN [ShoppingCarts] SC
	   ON SC.[sc_product] = P.[p_number]
 GROUP BY P.[p_number], P.[p_name], [p_price]