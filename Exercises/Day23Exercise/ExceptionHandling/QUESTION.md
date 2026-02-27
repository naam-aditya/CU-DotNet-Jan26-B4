# Exception Handling in .NET (Built-in & Custom)

## Objective

Students must demonstrate understanding of:

- Common built-in exceptions in .NET  
- Creating and using custom exceptions  
- `try–catch–finally`  
- `InnerException` and exception propagation  

---

# Problem Statement

You are developing a **Student Enrollment System**.

The system must validate student data and handle runtime errors using both built-in and custom exceptions.

---

# Part 1 – Built-in Exception Handling

## 🔹 Task 1

Create a console application that performs the following operations and handles exceptions:

### 1. Divide Two Numbers
- Accept two numbers from the user.
- Perform division.
- Handle:
  - `DivideByZeroException`

---

### 2. Convert User Input to Integer
- Take user input.
- Convert it to integer.
- Handle:
  - `FormatException`

---

### 3. Access Array Index
- Create an array.
- Ask the user for an index.
- Access the array element.
- Handle:
  - `IndexOutOfRangeException`

---

## Sample Requirements

- Use **separate try-catch blocks** for each scenario.
- Display meaningful error messages.
- Use a `finally` block to print:

```
Operation Completed
```

---

# Part 2 – Custom Exception

## 🔹 Task 2 – Create Custom Exceptions

Create the following custom exception classes:

- `InvalidStudentAgeException`
- `InvalidStudentNameException`

### Validation Rules

- Student age must be between **18 and 60**.
- Keep asking for age input until correct value is given.
- Validate student name appropriately.
- Throw custom exceptions when validation fails.

---

# Part 3 – InnerException Demonstration

## 🔹 Task 4

Wrap the custom exception inside another exception.

Print:

- Outer Exception Message  
- InnerException Message  

---

# Part 4 – Logging Exception Details

## 🔹 Task 5

When handling exceptions, print the following properties:

- `Message`
- `StackTrace`
- `InnerException`

---

## Learning Focus

- Built-in exception handling  
- Custom exception creation  
- Exception propagation  
- Using `finally` block  
- Understanding `InnerException`  
- Debugging using stack trace  
- Defensive programming practices  