SELECT
	s.CompanyName,
	SUM(o.Freight) AS TotalFreightCharges
FROM Orders o 
JOIN Shippers s 
	ON o.ShipVia = s.ShipperID
GROUP BY s.CompanyName;
