-- For each Category, list the most expensive product name and its price.

SELECT
    c.CategoryID,
    p.ProductName,
    p.UnitPrice
FROM Products p
JOIN Categories c
    ON p.CategoryID = c.CategoryID
WHERE p.UnitPrice = 
(
    SELECT MAX(p2.UnitPrice)
    FROM Products p2
    WHERE p2.CategoryID = p.CategoryID
)
ORDER BY 1;