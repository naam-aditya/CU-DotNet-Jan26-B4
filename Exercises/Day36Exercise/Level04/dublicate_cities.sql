-- Find Customers and Suppliers who are located in the same city.

SELECT
    c.CompanyName,
    c.ContactName,
    s.CompanyName,
    s.ContactName
FROM Customers c
INNER JOIN Suppliers s
    ON c.City = s.City;