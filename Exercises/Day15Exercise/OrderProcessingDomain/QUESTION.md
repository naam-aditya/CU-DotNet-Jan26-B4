# Order Processing Domain – Non-Static Class Design

## Objective

Design and implement a concrete class that demonstrates correct usage of non-static members, including:

- Instance fields  
- Instance properties  
- Instance methods  
- Non-static constructors  

All logic must rely on object creation and instance interaction.

---

## Application Type

- .NET Console Application  
- Language: C#  
- Target Framework: .NET 6 / .NET 8  

---

## Class to Be Designed

### Class Name (Mandatory)

`Order`

---

## Mandatory Design Requirements

### 1. Constructors (Non-Static Only)

The class must contain **exactly two constructors**:

#### 1. Default Constructor
- Initializes order date to current date  
- Initializes order status to `"NEW"`  

#### 2. Parameterized Constructor
Accepts:
- `int orderId`
- `string customerName`

Initializes mandatory fields.

> Static constructors are not allowed.

---

### 2. Instance Fields (Encapsulation Required)

The class must contain at least 3 private, non-static fields:

| Field          | Purpose                  |
|---------------|--------------------------|
| `_orderId`     | Stores order identifier  |
| `_customerName`| Stores customer name     |
| `_totalAmount` | Stores order total       |

---

### 3. Instance Properties

Create the following non-static properties:

#### 1. OrderId
- Read-only property  

#### 2. CustomerName
- Read/write property  
- Rejects null or empty values  

#### 3. TotalAmount
- Read-only property  
- Value updated internally only  

---

### 4. Instance Methods (Non-Static)

Implement at least 3 instance methods:

| Method | Description |
|--------|------------|
| `AddItem(decimal price)` | Adds item price to total |
| `ApplyDiscount(decimal percentage)` | Applies discount if percentage is between 1–30 |
| `GetOrderSummary()` | Returns formatted order summary |

---

### 5. Validation Rules

- Total amount cannot be negative  
- Customer name must be valid  
- Discount must be applied only once per order  
- All validations must be enforced via instance logic  

---

## Usage Requirement (Mandatory)

In `Main()`:

1. Create multiple objects of `Order`  
2. Use both constructors  
3. Call all instance methods  
4. Display results using returned values  

---

## Sample Usage (Expected)

```csharp
Order order = new Order(101, "Rahul");
order.AddItem(500);
order.AddItem(300);
order.ApplyDiscount(10);

Console.WriteLine(order.GetOrderSummary());
```

---

## Sample Output

```
Order Id: 101
Customer: Rahul
Total Amount: 720
Status: NEW
```