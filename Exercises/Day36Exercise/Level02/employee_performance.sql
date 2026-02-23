SELECT
	e.LastName,
	COUNT(*) AS TotalOrders
FROM Orders o 
JOIN Employees e 
	ON o.EmployeeID = e.EmployeeID
GROUP BY e.LastName;
