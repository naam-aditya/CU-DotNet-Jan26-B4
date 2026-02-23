SELECT
	o.OrderID,
	o.OrderDate,
	e.FirstName,
	e.LastName 
FROM Orders o 
JOIN Employees e 
	ON o.EmployeeID = e.EmployeeID;
