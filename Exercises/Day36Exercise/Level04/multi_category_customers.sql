-- List Customers who have purchased products from more than 3 different categories.

SELECT
    c.CustomerID,
    c.CompanyName,
    c.ContactName
FROM [Order Details] od
JOIN Orders o
    ON od.OrderID = o.OrderID
JOIN Products p
    ON od.ProductID = p.ProductID
JOIN Customers c
    ON o.CustomerID = c.CustomerID
JOIN Categories ct
    ON p.CategoryID = ct.CategoryID
GROUP BY 
    c.CustomerID,
    c.CompanyName,
    c.ContactName
HAVING COUNT(*) > 3;
