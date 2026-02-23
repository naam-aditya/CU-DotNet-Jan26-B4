-- -- Identify Order IDs where the total order value is higher than the average order value of the entire database.

SELECT
    od.OrderID,
    SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)) AS TotalOrderValue
FROM [Order Details] od
GROUP BY od.OrderID
HAVING
    SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)) > 
    (
        SELECT AVG(OrderTotal)
        FROM 
        (
            SELECT 
                SUM(UnitPrice * Quantity * (1 - Discount)) AS OrderTotal
            FROM [Order Details]
            GROUP BY OrderID
        ) AS OrderTotalAvg
    );