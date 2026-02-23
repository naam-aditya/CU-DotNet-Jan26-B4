SELECT
	c.CategoryName,
	SUM(p.UnitsInStock) AS TotalUnitsInStock
FROM Products p 
JOIN Categories c 
	ON c.CategoryID = p.CategoryID
GROUP BY c.CategoryName;
