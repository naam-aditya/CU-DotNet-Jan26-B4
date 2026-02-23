-- Find all Customers (Company Name) who have never placed an order (Use NOT IN or NOT EXISTS).

SELECT
	c.CompanyName 
FROM Customers c 
WHERE NOT EXISTS (
	SELECT 1
	FROM Orders o 
	WHERE o.CustomerID = c.CustomerID 
);