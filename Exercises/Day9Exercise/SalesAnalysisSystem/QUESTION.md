# Problem Statement: Weekly Sales Analysis System

---

## Background

A retail outlet records daily sales figures for a week (7 days).  
Management requires a small .NET console-based utility to analyze weekly performance using arrays only, without relying on advanced collections or LINQ.

---

## Objective

Develop a **Weekly Sales Analysis System** in .NET that stores, processes, and reports sales data for 7 consecutive days using array-based logic.

---

## Functional Requirements

### 1. Data Capture

- Use a single-dimensional `decimal[]` array of size 7 to store daily sales.
- Accept user input for each day (Day 1 to Day 7).
- Validate input:
  - Sales value must be greater than or equal to zero.
  - Invalid input should prompt re-entry for the same day.

---

### 2. Weekly Sales Analysis

Using array traversal logic, implement the following:

#### 1. Total Weekly Sales
- Calculate the sum of all sales values.

#### 2. Average Daily Sales
- Compute the average using `array.Length`.

#### 3. Highest Sales Day
Identify:
- Highest sales amount
- Corresponding day number (1–7)

#### 4. Lowest Sales Day
Identify:
- Lowest sales amount
- Corresponding day number (1–7)

#### 5. Days Above Average
- Count how many days recorded sales greater than the weekly average.

---

### 3. Sales Categorization (Parallel Array)

- Create a second array (`string[]`) to store sales category for each day:
  - `"Low"` → Sales < 5,000
  - `"Medium"` → Sales between 5,000 and 15,000
  - `"High"` → Sales > 15,000
- The category array must maintain index alignment with the sales array.

---

## 4. Output Report

Display a formatted weekly report similar to:

```
Weekly Sales Report
-------------------
Total Sales        : 82,500.00
Average Daily Sale : 11,785.71

Highest Sale       : 18,200.00 (Day 6)
Lowest Sale        : 3,400.00  (Day 2)

Days Above Average : 3
```

Also display a day-wise sales category summary:

```
Day 1 : Medium
Day 2 : Low
Day 3 : High
...
Day 7 : Medium
```

---

## Technical Constraints

- ✔ Use arrays only  
- ❌ Do NOT use:
  - `List<T>`
  - `LINQ`
  - `Array.Sort()` for finding min/max  
- ✔ Use:
  - `for` loops for processing
  - `foreach` loops only for display (optional)  
- ✔ Follow clean coding practices and meaningful variable names  

---

## Learning Outcomes

After completing this exercise, learners should be able to:

- Apply array indexing effectively  
- Use parallel arrays  
- Perform manual aggregation and comparison logic  
- Translate real-world requirements into array-based solutions  