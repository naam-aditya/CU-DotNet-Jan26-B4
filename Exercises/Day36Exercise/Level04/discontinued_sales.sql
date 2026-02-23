-- Calculate the total revenue generated only by products that are currently Discontinued.

SELECT 
    SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)) 
    AS TotalRevenueFromDiscontinuedProducts
FROM [Order Details] od
JOIN
    (
        SELECT * 
        FROM Products
        WHERE Discontinued = 1
    ) p
    ON od.ProductID = p.ProductID;