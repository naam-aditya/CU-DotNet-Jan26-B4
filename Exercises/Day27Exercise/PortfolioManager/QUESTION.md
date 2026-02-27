# Assignment: The Loan Portfolio Manager

## Objective

Create a `Loan` class, save a list of loan objects to a CSV file, and then read them back to identify which loans are **High Risk**, **Medium Risk**, or **Low Risk** based on their interest rate.

---

## Step 1: Define the Data Structure

Create a simple class to represent a loan.

```csharp
public class Loan
{
    public string ClientName { get; set; }
    public double Principal { get; set; }
    public double InterestRate { get; set; } // e.g., 5.5 for 5.5%
}
```

---

## Step 2: Implementation Requirements

The application must:

1. Write multiple `Loan` objects to a CSV file.
2. Read them back from the file.
3. Parse each row into a `Loan` object.
4. Classify each loan based on risk level.

---

## Critical Concepts to Note

### 1. The CSV Format

Use:

```csharp
line.Split(',')
```

This converts a CSV row into an array of values.

---

### 2. The Header Row

- Write a header row so the file is readable in Excel.
- While reading:
  
  Call `reader.ReadLine()` once before processing the loop  
  to skip the header row.

This prevents parsing errors.

---

### 3. Currency Formatting

Using:

```csharp
{principal:C}
```

inside string interpolation automatically formats numbers as local currency.

---

## Risk Classification Logic

Classify loans based on `InterestRate`:

- High Risk → InterestRate > 10%
- Medium Risk → InterestRate between 5% and 10%
- Low Risk → InterestRate < 5%

---

## Your Challenge Tasks

### 1. Append Mode

Modify the write section so that:

- Instead of overwriting the file,
- The program asks the user for a new loan’s details,
- Appends the new loan to the existing CSV file.

Use append mode in `StreamWriter`.

---

### 2. Calculated Field

While reading the file:

- Calculate total interest amount:

  Total Interest = Principal × InterestRate / 100

- Display it alongside the client name.

---

### 3. Data Safety

Prevent crashes due to invalid numeric values:

- Wrap `double.Parse()` inside a try-catch block  
  or
- Use `double.TryParse()` for safer parsing.

This ensures the program does not crash if the CSV contains incorrect data.

---

## Display Format (Example)

```
CLIENT      |     PRINCIPAL |     INTEREST | RISK LEVEL
--------------------------------------------------------
Rahul       |   $100,000.00 |   $12,500.00 | HIGH
Anita       |    $50,000.00 |    $3,500.00 | MEDIUM
John        |    $80,000.00 |    $2,800.00 | LOW
```

---

## Concepts Covered

- File writing and reading  
- CSV parsing  
- Data validation  
- Append vs overwrite mode  
- Risk classification logic  
- Currency formatting  
- Defensive programming  
- Collection handling  