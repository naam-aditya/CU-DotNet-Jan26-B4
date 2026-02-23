-- List all Employees and the names of the Regions they cover 
-- (requires joining Employees, EmployeeTerritories, Territories, and Region).

SELECT DISTINCT
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS EmployeeName,
    r.RegionDescription AS RegionName
FROM Employees e
INNER JOIN EmployeeTerritories et
    ON e.EmployeeID = et.EmployeeID
INNER JOIN Territories t
    ON et.TerritoryID = t.TerritoryID
INNER JOIN Region r
    ON t.RegionID = r.RegionID;