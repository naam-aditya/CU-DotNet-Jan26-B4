# Problem Statement  
## Utility Billing System – Tariff Calculation Engine

---

## Business Context

A city corporation wants to build a **Utility Billing System** that can calculate monthly bills for different types of utility connections.

Each utility follows common billing rules, but the calculation logic differs based on the utility type.

The system must be **extensible**, so new utility types can be added in the future without modifying existing billing logic.

---

## Design Requirements

---

## 1. Abstract Base Class

Create an abstract class named:

```
UtilityBill
```

This class represents the common structure for all utility bills.

---

## 2. Common Properties (Concrete)

The abstract class must contain the following properties:

- `ConsumerId` (int)  
- `ConsumerName` (string)  
- `UnitsConsumed` (decimal)  
- `RatePerUnit` (decimal)  

---

## 3. Abstract Method (Must Be Overridden)

Declare the following abstract method:

```csharp
public abstract decimal CalculateBillAmount();
```

➡️ Each utility type must implement its own billing calculation logic.

---

## 4. Virtual Method (Optional Override)

Define a virtual method in the abstract class:

```csharp
public virtual decimal CalculateTax(decimal billAmount)
```

### Default Behavior:
- Apply **5% tax** on the bill amount.

Derived classes may override this method if they follow a different tax rule.

---

## 5. Concrete Method (Non-Overridable by Default)

Add a concrete method:

```csharp
public void PrintBill()
```

This method should:

- Display consumer details  
- Display total units  
- Display final payable amount (including tax)  

➡️ This method must internally call:

- `CalculateBillAmount()`  
- `CalculateTax()`  

---

# Derived Classes

---

## 1. ElectricityBill

Inherits from `UtilityBill`.

### Rules:

- Rate per unit is passed during object creation.
- If `UnitsConsumed > 300`, apply **10% surcharge** on the bill amount.
- Override:
  - `CalculateBillAmount()`
- Use default tax logic (do **not** override `CalculateTax()`).

---

## 2. WaterBill

Inherits from `UtilityBill`.

### Rules:

- Flat rate per unit.
- No surcharge.
- Override:
  - `CalculateBillAmount()`
  - `CalculateTax()` → apply **2% tax only**.

---

## 3. GasBill

Inherits from `UtilityBill`.

### Rules:

- Monthly fixed charge of **₹150** added to bill amount.
- Override:
  - `CalculateBillAmount()`
- Tax remains zero:
  - Override `CalculateTax()` to return `0`.

---

# Application Requirements

Create a Console Application that:

1. Creates objects of:
   - `ElectricityBill`
   - `WaterBill`
   - `GasBill`

2. Stores them in a collection of type:

```csharp
List<UtilityBill>
```

3. Iterates through the collection and calls:

```csharp
PrintBill();
```

➡️ Demonstrate **runtime polymorphism** through method overriding.

---

## Learning Objectives Covered

- ✔ Abstract class usage  
- ✔ Abstract method implementation  
- ✔ Virtual method with optional override  
- ✔ Method overriding  
- ✔ Runtime polymorphism  
- ✔ Template Method–style design  
- ✔ Open/Closed Principle (extensible design)  