SELECT
	o.OrderID,
	c.CompanyName 
FROM Orders o 
JOIN Customers c 
	ON o.CustomerID = c.CustomerID;
