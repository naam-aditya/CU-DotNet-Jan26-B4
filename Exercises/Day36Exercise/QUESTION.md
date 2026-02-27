# SQL Practice: Multi-Level Query Challenges

# Level 1: The Join Foundation  
**Focus:** Inner Joins and basic multi-table connections  

1. **Basic Linking**  
   List all Product Names along with their Category Names.

2. **Order Details**  
   Display every Order ID alongside the Company Name of the customer who placed it.

3. **Supplier Tracking**  
   Show all Product Names and the Company Name of their respective suppliers.

4. **Employee Sales**  
   List all Orders (ID and Date) and the First/Last Name of the employee who processed them.

5. **International Logistics**  
   Find all Orders shipped to "France," showing the Order ID and the Company Name of the Shipper (from the Shippers table).

---

# Level 2: Aggregations with Joins  
**Focus:** Using GROUP BY across multiple tables  

6. **Category Stock**  
   Show the Category Name and the total number of units in stock for that category.

7. **Customer Spend**  
   List the Company Name and the total amount of money (Price × Quantity) they have spent across all orders.

8. **Employee Performance**  
   Display the Last Name of each employee and the total number of orders they have taken.

9. **Shipping Costs**  
   Find the total Freight charges paid to each Shipper company.

10. **Top Products**  
    List the top 5 Product Names based on total quantity sold.

---

# Level 3: Subqueries & Self-Joins  
**Focus:** Nested queries and tables referencing themselves  

11. **Above Average**  
    List all Product Names whose UnitPrice is greater than the average price of all products.

12. **The Bosses**  
    Use a Self-Join on the Employees table to show each employee's name and their manager's name.

13. **No Orders**  
    Find all Customers (Company Name) who have never placed an order (Use NOT IN or NOT EXISTS).

14. **High-Value Orders**  
    Identify Order IDs where the total order value is higher than the average order value of the entire database.

15. **Late Bloomers**  
    Select Product Names that have never been ordered after the year 1997.

---

# Level 4: Complex Logic & Advanced Joins  
**Focus:** Multiple joins, HAVING clauses, and correlated subqueries  

16. **Territory Coverage**  
    List all Employees and the names of the Regions they cover  
    (requires joining Employees, EmployeeTerritories, Territories, and Region).

17. **Duplicate Cities**  
    Find Customers and Suppliers who are located in the same city.

18. **Multi-Category Customers**  
    List Customers who have purchased products from more than 3 different categories.

19. **Discontinued Sales**  
    Calculate the total revenue generated only by products that are currently Discontinued.

20. **Correlated Subquery**  
    For each Category, list the most expensive product name and its price.

---

## Concepts Covered

- INNER JOIN  
- GROUP BY and Aggregations  
- HAVING clause  
- Subqueries  
- Self-Joins  
- Correlated Subqueries  
- NOT EXISTS / NOT IN  
- Multi-table joins  
- Real-world relational data analysis  