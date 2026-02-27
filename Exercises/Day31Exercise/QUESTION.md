# LINQ Practice Assignments (LINQ to Objects)

# 1. Student Performance Analytics

## Problem Statement

Create a `Student` class with:

- Id  
- Name  
- Class  
- Marks  

### LINQ Tasks

- Get top 3 students by marks  
- Group students by Class and calculate average marks  
- List students who scored below class average  
- Order students by Class, then by Marks descending  

### Concepts

Where, OrderBy, GroupBy, Select, Aggregate  

### Data Model

```csharp
class Student
{
    public int Id;
    public string Name;
    public string Class;
    public int Marks;
}
```

---

# 2. Employee Salary Processing System

## Problem Statement

Create an Employee list with:

- Id  
- Name  
- Department  
- Salary  
- JoinDate  

### LINQ Tasks

- Get highest and lowest salary in each department  
- Count employees per department  
- Filter employees joined after 2020  
- Project anonymous objects with Name and AnnualSalary  

### Concepts

GroupBy, Max, Min, Count, Select, Where  

### Data Model

```csharp
class Employee
{
    public int Id;
    public string Name;
    public string Dept;
    public double Salary;
    public DateTime JoinDate;
}
```

---

# 3. Product Inventory and Sales Query

## Problem Statement

Create Product and Sales collections.

- Product → Id, Name, Category, Price  
- Sales → ProductId, QuantitySold  

### LINQ Tasks

- Join Products with Sales  
- Calculate total revenue per product  
- Get best-selling product  
- List products with zero sales  

### Concepts

Join, GroupJoin, Sum, DefaultIfEmpty  

### Data Model

```csharp
class Product 
{ 
    public int Id; 
    public string Name; 
    public string Category; 
    public double Price; 
}

class Sale 
{ 
    public int ProductId; 
    public int Qty; 
}
```

---

# 4. Library Book Management System

## Problem Statement

Create Book objects with:

- Title  
- Author  
- Genre  
- PublishedYear  
- Price  

### LINQ Tasks

- Find books published after 2015  
- Group by Genre and count books  
- Get most expensive book per Genre  
- Return distinct authors list  

### Concepts

Where, GroupBy, Distinct, OrderByDescending, FirstOrDefault  

### Data Model

```csharp
class Book 
{ 
    public string Title; 
    public string Author; 
    public string Genre; 
    public int Year; 
    public double Price; 
}
```

---

# 5. Customer Order Analysis

## Problem Statement

Create Customer and Order classes.

- Customer → Id, Name, City  
- Order → OrderId, CustomerId, Amount, OrderDate  

### LINQ Tasks

- Get total order amount per customer  
- List customers with no orders  
- Get customers who spent above ₹50,000  
- Sort customers by total spending  

### Concepts

Join, GroupJoin, Sum, OrderBy, SelectMany  

### Data Model

```csharp
class Customer 
{ 
    public int Id; 
    public string Name; 
    public string City; 
}

class Order 
{ 
    public int OrderId; 
    public int CustomerId; 
    public double Amount; 
}
```

---

# 6. Movie Streaming Platform Query System

## Problem Statement

Create a Movie class with:

- Title  
- Genre  
- Rating  
- ReleaseYear  

### LINQ Tasks

- Filter movies with rating > 8  
- Group movies by Genre and get average rating  
- Find latest movie per Genre  
- Get top 5 highest-rated movies  

### Concepts

Where, GroupBy, Average, Take, OrderByDescending  

### Data Model

```csharp
class Movie 
{ 
    public string Title; 
    public string Genre; 
    public double Rating; 
    public int Year; 
}
```

---

# 7. Bank Transaction Analyzer

## Problem Statement

Create a Transaction class with:

- AccountNumber  
- Amount  
- TransactionType (Credit/Debit)  
- Date  

### LINQ Tasks

- Calculate total balance per account  
- List suspicious accounts with total debit > credit  
- Group transactions by month  
- Find highest transaction amount per account  

### Concepts

GroupBy, Sum, Max, Select, Where  

### Data Model

```csharp
class Transaction 
{ 
    public int Acc; 
    public double Amount; 
    public string Type; 
}
```

---

# 8. E-Commerce Cart Processing

## Problem Statement

Create CartItem list with:

- ProductName  
- Category  
- Price  
- Quantity  

### LINQ Tasks

- Calculate total cart value  
- Group by Category and total category cost  
- Apply 10% discount for Electronics category  
- Return cart summary DTO objects  

### Concepts

GroupBy, Select, Sum, Projection, Conditional logic in LINQ  

### Data Model

```csharp
class CartItem 
{ 
    public string Name; 
    public string Category; 
    public double Price; 
    public int Qty; 
}
```

---

# 9. Social Media User Analytics

## Problem Statement

Create User and Post classes.

- User → UserId, Name, Country  
- Post → PostId, UserId, Likes, Comments  

### LINQ Tasks

- Get top users by total likes  
- Group users by country  
- List inactive users (no posts)  
- Calculate average likes per post  

### Concepts

Join, GroupJoin, Sum, Average, SelectMany  

### Data Model

```csharp
class User 
{ 
    public int Id; 
    public string Name; 
    public string Country; 
}

class Post 
{ 
    public int UserId; 
    public int Likes; 
}
```

---

## Overall Concepts Practiced

- LINQ to Objects  
- Filtering and Projection  
- Grouping and Aggregation  
- Sorting and Ranking  
- Joining collections  
- Handling nested collections  
- Anonymous types and DTO projection  
- Real-world data querying scenarios  