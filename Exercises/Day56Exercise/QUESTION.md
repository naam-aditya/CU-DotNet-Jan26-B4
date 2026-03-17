# Problem Statement: The "QuickLoan" In-Memory Tracker


## Background

QuickLoan Financial Services is a small startup that needs a **prototype loan management system**.

Before investing in a full database infrastructure, they want a **proof-of-concept web application** that can manage their current lending portfolio.

To keep the system lightweight, all loan data will be stored **in memory** using a **Static List** instead of a database.

---

# The Challenge

Build an **ASP.NET Core MVC application** that manages a **Loan entity**.

The application must support full **CRUD operations**:

- Create
- Read
- Update
- Delete

All operations should be performed through a **web interface**.

---

# 1. The Model (The Blueprint)

Create a class:

```
Loan
```

Inside the **Models** folder.

---

## Properties

Property | Type | Description
---------|------|------------
Id | int | Unique identifier for each loan
BorrowerName | string | Name of the borrower
LenderName | string | Name of the lender
Amount | double | Loan amount
IsSettled | bool | Indicates whether the loan is settled

---

## Validation Rules

Use **Data Annotations** to enforce the following:

- **BorrowerName**
  - Required field

- **Amount**
  - Must be between **1 and 500,000**

- Display Name

```
Borrower Name
```

instead of:

```
BorrowerName
```

on the UI.

---

# 2. The Controller (The Logic)

Create a controller:

```
LoanController
```

The controller will simulate a database using:

```
private static List<Loan>
```

---

## Required Action Methods

### Index

Purpose:

- Display all loans in a table.

Route:

```
/Loan
```

---

### Add

Two actions are required:

#### GET

- Displays the form to create a new loan.

#### POST

- Saves the loan to the static list.

---

### Edit

Two actions are required:

#### GET

- Retrieve an existing loan using its **Id**.
- Populate the form with existing values.

#### POST

- Update the loan details in the list.

---

### Delete

Purpose:

- Remove a loan from the list using its **Id**.

---

# 3. The Views (The Interface)

Create Razor views using **Bootstrap** for a clean UI.

---

## Index View

Display loans in a **table**.

Each row must include:

- Edit button
- Delete button

---

## Add / Edit Views

Create forms using **Tag Helpers**:

Examples:

```
asp-for
asp-validation-for
```

The form should allow users to enter:

- Borrower Name
- Lender Name
- Loan Amount
- Settlement status

---

## Validation Summary

If the user enters invalid data (e.g., invalid loan amount):

- Display validation errors
- The page should **not crash**
- Errors must appear using validation messages

---

# Requirements & Constraints

---

## No Entity Framework

Do **not use**:

- DbContext
- Migrations
- Database connections

All data must be stored in a **static list** so that it persists while the application is running.

---

## Strongly Typed Views

Every view must use the Razor directive:

```
@model
```

---

## Tag Helpers

Use Tag Helpers for navigation and form actions:

Examples:

```
asp-action
asp-route-id
```

---

## Post-Redirect-Get Pattern

After:

- Adding a loan
- Editing a loan

The user must be redirected back to the:

```
Index
```

action.

---

# Success Criteria

The assignment is successful if the following conditions are met.

---

## 1. Read

When navigating to:

```
/Loan
```

the user can see a **list of all loans**.

---

## 2. Create

The user can:

- Add a new loan
- See the new loan appear in the table.

---

## 3. Update

The user can:

- Edit an existing loan
- Change the **Lender Name**.

---

## 4. Delete

The user can:

- Remove a loan from the list.

---

## 5. Validation

If the user tries to save a loan with:

- Negative amount
- Amount outside allowed range

the system must:

- Show validation errors
- Prevent saving the record.

---

# Concepts Covered

- ASP.NET Core MVC architecture  
- CRUD operations  
- Static in-memory data storage  
- Data annotations for validation  
- Strongly typed Razor views  
- Tag Helpers  
- Bootstrap UI integration  
- Post-Redirect-Get pattern  