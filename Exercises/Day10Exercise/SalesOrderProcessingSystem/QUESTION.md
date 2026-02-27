# Sales Order Processing System (Methods-Focused)

## Problem Statement

A retail company wants a console-based **Sales Order Processing System** developed in .NET.

The objective is to design and use methods correctly to:

- Process weekly sales data  
- Calculate totals  
- Apply discounts  
- Generate a sales summary  

The emphasis is **not UI**, but **method design, usage, and maintenance-friendly structure**.

---

## Business Scenario

- The company records daily sales amounts for 7 days  
- Discounts depend on total weekly sales  
- Tax must be applied after discount  
- All calculations must be done using well-defined methods  

---

## Functional Requirements

### 1. Sales Data Input

- Accept 7 daily sales values  
- Store them in a `decimal[]`  
- Input validation must be done via a separate method  

#### Method to design:

```csharp
static void ReadWeeklySales(decimal[] sales)
```

**Responsibilities:**

- Read input  
- Validate non-negative values  
- Store in array  

---

### 2. Total and Average Calculation

Calculate:

- Total weekly sales  
- Average daily sales  

#### Methods to design:

```csharp
static decimal CalculateTotal(decimal[] sales)
static decimal CalculateAverage(decimal total, int days)
```

---

### 3. Highest and Lowest Sales Day

Determine:

- Highest sale value and day number  
- Lowest sale value and day number  

#### Methods to design (must use `out` parameters):

```csharp
static decimal FindHighestSale(decimal[] sales, out int day)
static decimal FindLowestSale(decimal[] sales, out int day)
```

---

### 4. Discount Calculation (Method Overloading)

#### Discount Rules:

- If total ≥ 50,000 → 10% discount  
- Otherwise → 5% discount  

#### Method Overloading Requirement:

```csharp
static decimal CalculateDiscount(decimal total)
static decimal CalculateDiscount(decimal total, bool isFestivalWeek)
```

**Festival week rule:**

- Extra 5% discount if `isFestivalWeek == true`

---

### 5. Tax Calculation

- Apply **18% tax** on the discounted amount  

#### Method to design:

```csharp
static decimal CalculateTax(decimal amount)
```

---

### 6. Final Payable Amount

Calculate final amount using:

- Total sales  
- Discount  
- Tax  

#### Method to design:

```csharp
static decimal CalculateFinalAmount(decimal total, decimal discount, decimal tax)
```

---

### 7. Sales Categorization (Parallel Array)

Categorize each day’s sale:

- `"Low"` → < 5,000  
- `"Medium"` → 5,000–15,000  
- `"High"` → > 15,000  

#### Method to design:

```csharp
static void GenerateSalesCategory(decimal[] sales, string[] categories)
```

---

## Output Requirements

Display a clean report:

```
Weekly Sales Summary
--------------------
Total Sales        : 78,500.00
Average Daily Sale : 11,214.29

Highest Sale       : 18,000.00 (Day 6)
Lowest Sale        : 3,200.00  (Day 2)

Discount Applied   : 7,850.00
Tax Amount         : 12,168.30
Final Payable      : 82,818.30

Day-wise Category:
Day 1 : Medium
Day 2 : Low
...
Day 7 : High
```

---

## Technical Constraints (Strict)

Learners must:

- Use methods for every logical operation  
- Use arrays only (**no `List<T>`, no LINQ**)  
- Use:
  - `ref` / `out` where appropriate  
  - Method overloading  
  - Meaningful method names  

Avoid:

- Writing all logic in `Main`  
- Duplicate logic  
- Hard-coded values inside `Main`  

---

## Learning Objectives (What This Tests)

This single exercise validates:

- Method definition vs invocation  
- Parameter vs argument understanding  
- Return types vs `void`  
- `out` parameters  
- Method overloading  
- Separation of concerns  
- Maintainable, readable code structure  
