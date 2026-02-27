# Case Study: Employee Compensation Management System

## Background

An organization is developing a compensation calculation module for different categories of employees.  

All employees share common details, but salary computation rules differ based on employee type.  

Due to architectural constraints, the design must **not use abstract classes or virtual methods**.

---

## Functional Requirements

---

## 1. Base Class: `Employee`

Create a base class named `Employee`.

### Properties

- `EmployeeId` (int)  
- `EmployeeName` (string)  
- `BasicSalary` (decimal)  
- `ExperienceInYears` (int)  

### Constructor

- Initialize all properties using a parameterized constructor.

### Methods

#### `CalculateAnnualSalary()`
- Calculates annual salary using:
  ```
  Annual Salary = BasicSalary × 12
  ```
- Must **not** be marked as `virtual`.

#### `DisplayEmployeeDetails()`
- Displays employee details.
- Displays calculated annual salary.

> Important: These methods must not be marked `virtual`.

---

## 2. Derived Class: `PermanentEmployee`

Inherits from `Employee`.

### Business Rules

- House Rent Allowance (HRA): 20% of BasicSalary  
- Special Allowance: 10% of BasicSalary  
- Loyalty Bonus:
  - ₹50,000 if experience ≥ 5 years  

### Method Implementation

- Implement method hiding:
  ```csharp
  public new decimal CalculateAnnualSalary()
  ```
- Include allowances and bonus in annual salary calculation.

---

## 3. Derived Class: `ContractEmployee`

Inherits from `Employee`.

### Additional Property

- `ContractDurationInMonths` (int)

### Business Rules

- No allowances  
- Contract completion bonus:
  - ₹30,000 if duration ≥ 12 months  

### Method Implementation

- Implement:
  ```csharp
  public new decimal CalculateAnnualSalary()
  ```

---

## 4. Derived Class: `InternEmployee`

Inherits from `Employee`.

### Business Rules

- Fixed stipend  
- No bonus or allowance  
- Annual salary:
  ```
  BasicSalary × 12
  ```

### Method Implementation

- Implement:
  ```csharp
  public new decimal CalculateAnnualSalary()
  ```

---

## Usage Instructions

1. Create objects of:
   - `Employee`
   - `PermanentEmployee`
   - `ContractEmployee`
   - `InternEmployee`

2. Call `CalculateAnnualSalary()` using:
   - Base class references
   - Derived class references

### Example

```csharp
Employee emp1 = new PermanentEmployee(...);
PermanentEmployee emp2 = new PermanentEmployee(...);

Console.WriteLine(emp1.CalculateAnnualSalary()); // Base method
Console.WriteLine(emp2.CalculateAnnualSalary()); // Derived method
```

---

## Learning Focus

- Inheritance without polymorphism  
- Method hiding using `new`  
- Difference between base reference and derived reference  
- Compile-time vs runtime behavior  