-- List all Product Names whose UnitPrice is greater than the average price of all products.

SELECT
	p.ProductName,
	p.UnitPrice 
FROM Products p
WHERE p.UnitPrice > (
	SELECT AVG (UnitPrice)
	FROM Products
);