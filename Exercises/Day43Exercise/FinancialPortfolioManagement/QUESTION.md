# Financial Portfolio Management & Reporting System

## Objective

Design a Console-based Financial Portfolio Management System that:

- Manages multiple financial instruments  
- Processes transactions  
- Generates detailed portfolio reports  

---

# Functional Requirements

---

## 1. Financial Instruments

The system must support the following instrument types:

- Equity  
- Bond  
- FixedDeposit  
- MutualFund  

Each instrument must contain:

- InstrumentId (unique)  
- Name  
- Currency  
- PurchaseDate  
- Quantity / Units  
- PurchasePrice  
- MarketPrice  

---

## 2. OOP Design Constraints (Mandatory)

### Abstraction

Create an abstract base class:

```
abstract class FinancialInstrument
```

It must include:

- Common properties  
- Abstract method:

```
public abstract decimal CalculateCurrentValue();
```

- Virtual method:

```
public virtual string GetInstrumentSummary();
```

---

### Inheritance

All instruments must inherit from `FinancialInstrument`.

Each instrument:

- Must override `CalculateCurrentValue()`  
- May override `GetInstrumentSummary()`  

---

### Interfaces

Create at least two interfaces:

```
interface IRiskAssessable
{
    string GetRiskCategory();
}

interface IReportable
{
    string GenerateReportLine();
}
```

At least two instruments must implement both interfaces.

---

### Encapsulation Rules

- Use private backing fields where validation is required.  
- Prevent negative quantity or price.  
- Throw custom exception if violated.  

---

## 3. Custom Exception

Create:

```
class InvalidFinancialDataException : Exception
```

Throw this exception when:

- Quantity is negative  
- Price is negative  
- Currency format is invalid (must be 3-letter code)  

---

## 4. Portfolio Management

Create a `Portfolio` class.

### Responsibilities

Store instruments in:

- `List<FinancialInstrument>`  
- `Dictionary<string, FinancialInstrument>` (key = InstrumentId)  

### Required Methods

- AddInstrument()  
- RemoveInstrument()  
- GetTotalPortfolioValue()  
- GetInstrumentById()  
- GetInstrumentsByRisk(string risk)  

### LINQ Usage

Use LINQ for:

- Calculating total portfolio value  
- Risk-based filtering  
- Grouping by instrument type  

---

## 5. Transactions Module

Create class:

```
class Transaction
```

### Properties

- TransactionId  
- InstrumentId  
- Type (Buy/Sell)  
- Units  
- Date  

### Storage Requirements

- Store transactions initially in an Array  
- Convert to List for processing  

### Responsibilities

- Validate transactions  
- Update portfolio holdings  
- Prevent invalid sell operations  

---

## 6. Reporting Engine

Create class:

```
class ReportGenerator
```

### Console Report

Display:

- Portfolio Summary  
- Grouped by instrument type  
- Total investment  
- Total current value  
- Profit/Loss  
- Risk category distribution  

---

### File Report

Generate:

```
PortfolioReport_YYYYMMDD.txt
```

Include:

- Header  
- Instrument details  
- Aggregated summary  
- Timestamp  

Use:

- StreamWriter  
- Proper exception handling  
- Ensure file is closed correctly  

---

## 7. String Handling Requirements

- Format currency using:

```
value.ToString("C")
```

- Parse instrument input from CSV line:

```
EQ001,Equity,INFY,INR,100,1500,1650
```

- Validate string formats  
- Use `string.Split()`  
- Use string interpolation  

---

## 8. Arrays & Collections Usage

Must include:

- Array of transactions  
- List of instruments  
- Dictionary for lookup  
- LINQ operations:
  - OrderByDescending  
  - GroupBy  
  - Sum  
  - Where  

---

## 9. Polymorphism Requirement

Portfolio must store instruments as:

```
List<FinancialInstrument>
```

And call:

```
instrument.CalculateCurrentValue();
```

Without knowing the concrete type.

---

## 10. Edge Cases to Handle

- Selling more units than owned  
- Duplicate instrument ID  
- File write permission error  
- Invalid CSV format  
- Currency mismatch in portfolio  

---

# Sample Console Report Output

```
===== PORTFOLIO SUMMARY =====

Instrument Type: Equity
Total Investment: ₹500,000
Current Value: ₹575,000
Profit/Loss: ₹75,000

Instrument Type: Bond
Total Investment: ₹200,000
Current Value: ₹210,000

Overall Portfolio Value: ₹785,000

Risk Distribution:
Low: 2
Medium: 1
High: 1
```

---

## Concepts Covered

- Abstraction and Inheritance  
- Interfaces and Polymorphism  
- Custom Exceptions  
- Encapsulation and Validation  
- LINQ Queries  
- Dictionary and List usage  
- Array to List conversion  
- File I/O with StreamWriter  
- Financial calculations with decimal  
- Defensive programming  