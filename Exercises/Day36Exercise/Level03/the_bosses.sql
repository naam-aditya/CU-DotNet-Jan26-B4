-- Use a Self-Join on the Employees table to show each employee's name and their manager's name.

SELECT
	e1.FirstName + ' ' + e1.LastName AS EmployeeName,
	e2.FirstName + ' ' + e2.LastName AS ReportsTo 
FROM Employees e1
LEFT JOIN Employees e2
	ON e1.ReportsTo = e2.EmployeeID;