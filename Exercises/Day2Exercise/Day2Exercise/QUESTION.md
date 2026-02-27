# .NET 8 / C# 12 – Detailed Case Study Exercises  
## Value Types & Type Conversions

These case-study exercises are intentionally detailed to encourage analytical thinking.  

Students are expected to:

- Identify appropriate value data types  
- Apply implicit and explicit conversions  
- Handle precision, overflow, and rounding  
- Justify their design decisions using **C# 12** and **.NET 8** standards  

---

## Exercise 1: Student Attendance & Eligibility System

A college tracks attendance for each subject. Attendance is captured daily as integers.

At the end of the semester, attendance percentage is calculated as a `double`.

University rules state that eligibility must be displayed as an integer percentage.

### Design logic to:

- Store raw attendance data  
- Calculate percentage  
- Convert the value safely for display  
- Explain rounding vs truncation impact  

---

## Exercise 2: Online Examination Result Processing

An online exam system stores marks per subject as `int`.

The final average must be shown with **two decimal places**.

Later, scholarship eligibility requires converting the average into an `int`.

### Design:

- The conversion flow  
- Precision handling strategy  
- Scenarios where precision loss may occur  

---

## Exercise 3: Library Fine Calculation System

- Fine per day is configured as `decimal`  
- Days overdue are stored as `int`  
- Total fine must be displayed as `decimal`  
- Fine is logged as `double` for analytics  

### Explain:

- Why different data types are used  
- How implicit and explicit conversions occur  
- Precision implications  

---

## Exercise 4: Banking Interest Calculation Module

- Account balance is `decimal`  
- Interest rate is `float` from an external API  
- Monthly interest is calculated and added to balance  

### Demonstrate:

- Safe conversion techniques  
- Why implicit conversion may fail  
- Financial precision considerations  

---

## Exercise 5: E-Commerce Order Pricing Engine

- Cart total is accumulated using `double`  
- Tax and discount rules require `decimal`  
- Final payable amount is stored as `decimal`  

### Explain:

- Conversion strategy  
- Precision risks  
- Why mixing `double` and `decimal` must be handled carefully  

---

## Exercise 6: Weather Monitoring & Reporting

- Temperature sensors send readings as `short`  
- Values must be converted to Celsius and stored as `double`  
- Daily average is converted to `int` for dashboard display  

### Discuss:

- Overflow risks  
- Casting concerns  
- Safe conversion methods  

---

## Exercise 7: University Grading Engine

- Final score is calculated as `double`  
- Grades are stored as `byte` values  

### Design logic to:

- Convert score to grade safely  
- Validate score range before casting  
- Justify casting choices  

---

## Exercise 8: Mobile Data Usage Tracker

- Usage is tracked in bytes as `long`  
- Displayed in MB and GB as `double`  
- Monthly summary rounds values to nearest integer  

### Explain:

- Implicit conversions  
- Rounding techniques (`Math.Round`, `Math.Floor`, `Math.Ceiling`)  
- Large number handling  

---

## Exercise 9: Warehouse Inventory Capacity Control

- Item count stored as `int`  
- Maximum capacity stored as `ushort`  
- Conversion required during comparison and reporting  

### Explain:

- Signed vs unsigned conversion risks  
- Overflow possibilities  
- Safe comparison strategies  

---

## Exercise 10: Payroll Salary Computation

- Basic salary is `int`  
- Allowances and deductions are `double`  
- Net salary stored as `decimal`  

### Design:

- Type conversion flow  
- Financial precision handling  
- Justification for final storage type  

---

## Objective of This Case Study Set

These exercises collectively assess:

- Understanding of value types  
- Type safety awareness  
- Implicit vs explicit conversions  
- Precision management  
- Overflow handling  
- Financial vs scientific data type selection  
- Best practices in modern C# 12 / .NET 8 development  
