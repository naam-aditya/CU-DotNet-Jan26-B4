# Case Study: Loan EMI Calculation System

## Background

A bank is building an EMI (Equated Monthly Installment) calculation module for different loan products.

All loans share common data, but EMI calculation rules differ by loan type.  
Due to legacy constraints, the system must **not use polymorphism** (`virtual` / `override`).

---

## Functional Requirements

---

## 1. Base Class: `Loan`

Create a base class named `Loan`.

### Properties

- `LoanNumber` (string)  
- `CustomerName` (string)  
- `PrincipalAmount` (decimal)  
- `TenureInYears` (int)  

### Constructor

- Initialize all properties using a parameterized constructor.

### Method

Create a method:

```
CalculateEMI()
```

- Returns EMI calculated using:
  - **10% simple interest**

---

## 2. Derived Class: `HomeLoan`

Inherit from `Loan`.

### Business Rules

- Flat interest rate: **8%**
- One-time processing fee: **1% of PrincipalAmount**

---

## 3. Derived Class: `CarLoan`

Inherit from `Loan`.

### Business Rules

- Flat interest rate: **9%**
- Insurance charge: **₹15,000** (added to principal)

---

## Important Concept

Can we create a member again in the derived class with the same name as in base class?  

**Yes.**

⚠ Warning: This is called **Hiding Base Class Member**.

---

## Usage Requirement

- Create **2 HomeLoan objects**
- Create **2 CarLoan objects**
- Add all objects into an array
- Use a loop to call:

```
CalculateEMI()
```

on all objects.

---

## Key Observation

In this design:

- Since polymorphism is not used,
- Each object calls the **base class method**
- Method hiding does not achieve runtime polymorphism

---

## Learning Focus

- Inheritance without polymorphism  
- Method hiding  
- Base vs derived method behavior  
- Object behavior inside arrays  