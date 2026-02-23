-- list top 5 Product Names based on total
-- quantity sold.

SELECT TOP 5
	p.ProductName,
	SUM(od.Quantity) AS TotalQuantity
FROM [Order Details] od
JOIN Products p 
	ON od.ProductID = p.ProductID
GROUP BY p.ProductName
ORDER BY TotalQuantity DESC;