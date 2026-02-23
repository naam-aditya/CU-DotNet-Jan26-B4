SELECT
	o.OrderID,
	s.CompanyName 
FROM Orders o 
JOIN Shippers s 
	ON s.ShipperID = o.ShipVia
WHERE o.ShipCountry = 'FRANCE';
