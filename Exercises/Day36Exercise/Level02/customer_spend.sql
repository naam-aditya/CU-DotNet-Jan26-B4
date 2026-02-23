SELECT
	c.CompanyName,
	SUM(od.UnitPrice * od.Quantity) AS TotalMoneySpent
FROM Orders o 
JOIN Customers c 
	ON c.CustomerID = o.CustomerID
JOIN [Order Details] od
	ON od.OrderID = o.OrderID
GROUP BY 
	c.CompanyName;
