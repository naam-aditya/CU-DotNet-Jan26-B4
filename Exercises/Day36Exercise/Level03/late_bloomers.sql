-- Select Product Names that have never been ordered after the year 1997.

SELECT p.ProductName
FROM Products p
WHERE NOT EXISTS
(
    SELECT 1
    FROM [Order Details] od
    INNER JOIN Orders o
        ON od.OrderID = o.OrderID
    WHERE od.ProductID = p.ProductID
        AND o.OrderDate > '1997-12-31'
);
