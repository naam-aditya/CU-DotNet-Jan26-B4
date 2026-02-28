# Employee Annual Performance Bonus Calculation System

## Background

A multinational organization calculates employee annual performance bonuses using a rule-based system.

The bonus depends on multiple factors:

- Performance rating  
- Years of experience  
- Attendance percentage  
- Department multiplier  
- Progressive tax deduction rules  

Your task is to implement a C# class with a **calculated property** that computes the employee’s final net annual bonus according to the business rules defined below.

The property must contain the complete calculation logic (no external service class).

Students will then write **unit tests using NUnit** to validate all scenarios.

---

# Requirements

Create a class:

```
public class EmployeeBonus
```

---

## Required Properties

Property | Type | Description
---------|------|------------
BaseSalary | decimal | Annual base salary
PerformanceRating | int | Rating from 1 to 5
YearsOfExperience | int | Total completed years
DepartmentMultiplier | decimal | Multiplier based on department (e.g., 1.2)
AttendancePercentage | double | Annual attendance (0–100)

---

# Calculated Property

Create a read-only property:

```
public decimal NetAnnualBonus { get; }
```

This property must compute the bonus using the business rules below.

---

# Business Rules

---

## Step 1: Base Bonus Percentage (Based on Rating)

Rating | Bonus % of Base Salary
-------|-----------------------
5 | 25%
4 | 18%
3 | 12%
2 | 5%
1 | 0%

If rating is outside 1–5 → throw `InvalidOperationException`.

---

## Step 2: Experience Bonus

Additional percentage applied on BaseSalary:

- More than 10 years → +5%  
- More than 5 years → +3%  
- Otherwise → No additional bonus  

---

## Step 3: Attendance Penalty

If:

```
AttendancePercentage < 85%
```

Reduce the current bonus by 20%.

---

## Step 4: Department Multiplier

Multiply the bonus by:

```
DepartmentMultiplier
```

---

## Step 5: Maximum Cap

The total bonus **before tax** must not exceed:

```
40% of BaseSalary
```

If exceeded, cap it to 40%.

---

## Step 6: Tax Deduction

Apply tax on bonus (after cap):

Bonus Amount | Tax Rate
-------------|----------
≤ 150,000 | 10%
> 150,000 and ≤ 300,000 | 20%
> 300,000 | 30%

---

## Step 7: Final Output

Return:

- Final bonus after tax  
- Rounded to 2 decimal places using:

```
Math.Round(value, 2)
```

---

# Edge Case Handling

1. If `BaseSalary ≤ 0` → Return 0.  
2. Invalid rating → Throw exception.  
3. Attendance must be between 0–100 (optional validation).  

---

# Testing Requirements (Using NUnit)

Students must write comprehensive unit tests covering:

### 1. Normal Calculation Case
Valid rating, no penalties, no cap triggered.

### 2. Experience Bonus Trigger
Test both:
- 5 years  
- 10 years  

### 3. Attendance Penalty
Attendance < 85%.

### 4. Cap Rule
Scenario where calculated bonus exceeds 40%.

### 5. All Tax Slabs
Test:
- 10% slab  
- 20% slab  
- 30% slab  

### 6. Invalid Rating Test
Verify exception is thrown.

### 7. Zero Salary Case
Bonus must return 0.

### 8. Precision Test
Verify rounding behavior for long decimal values.

---

# Sample Test Cases

---

## Test Case 1 – Normal High Performer (No Cap Triggered)

Input:

Property | Value
---------|-------
BaseSalary | 500,000
PerformanceRating | 5
YearsOfExperience | 6
DepartmentMultiplier | 1.1
AttendancePercentage | 95

Expected Output:

```
123200.00
```

---

## Test Case 2 – Attendance Penalty Applied

Input:

Property | Value
---------|-------
BaseSalary | 400,000
PerformanceRating | 4
YearsOfExperience | 8
DepartmentMultiplier | 1.0
AttendancePercentage | 80

Expected Output:

```
60480.00
```

---

## Test Case 3 – Cap Triggered

Input:

Property | Value
---------|-------
BaseSalary | 1,000,000
PerformanceRating | 5
YearsOfExperience | 15
DepartmentMultiplier | 1.5
AttendancePercentage | 95

Expected Output:

```
280000.00
```

---

## Test Case 4 – Zero Salary

Input:

```
BaseSalary = 0
```

Expected Output:

```
0.00
```

---

## Test Case 5 – Low Performer (Rating 2)

Input:

Property | Value
---------|-------
BaseSalary | 300,000
PerformanceRating | 2
YearsOfExperience | 3
DepartmentMultiplier | 1.0
AttendancePercentage | 90

Expected Output:

```
13500.00
```

---

## Test Case 6 – Exact 150,000 Tax Boundary

Input:

Property | Value
---------|-------
BaseSalary | 600,000
PerformanceRating | 3
YearsOfExperience | 0
DepartmentMultiplier | 1.0
AttendancePercentage | 100

Expected Output:

```
64800.00
```

---

## Test Case 7 – High Tax Slab (>300k Without Cap)

Input:

Property | Value
---------|-------
BaseSalary | 900,000
PerformanceRating | 5
YearsOfExperience | 11
DepartmentMultiplier | 1.2
AttendancePercentage | 100

Expected Output:

```
226800.00
```

---

## Test Case 8 – Rounding Precision Case

Input:

Property | Value
---------|-------
BaseSalary | 555,555
PerformanceRating | 4
YearsOfExperience | 6
DepartmentMultiplier | 1.13
AttendancePercentage | 92

Expected Output:

```
118649.88
```

---

# Concepts Covered

- Calculated properties  
- Complex rule-based logic  
- Progressive tax calculation  
- Capping logic  
- Conditional branching  
- Exception handling  
- Financial rounding  
- Unit testing with NUnit  
- Edge case validation  
- Business rule modeling  