# Case Study: Shared Greeting Library

## Business Scenario

A company is building multiple small console tools.

To avoid repeating code, they want a shared library that generates greeting messages, which can be reused by different applications.

Your task is to:

1. Create a **Class Library (.NET Framework)**
2. Add a simple method in the library
3. Reference the library from a Console Application
4. Use the library method to display output

---

## Solution Structure (Mandatory)

Create a **.NET Framework solution** with the following projects:

```
GreetingSolution
│
├── GreetingLibrary   (Class Library – .NET Framework)
└── GreetingApp       (Console App – .NET Framework)
```

---

# Project 1: Class Library – GreetingLibrary

## Step 1: Create the Library Project

- Project Type: **Class Library (.NET Framework)**
- Project Name: `GreetingLibrary`

---

## Step 2: Create a Class

Create a class named:

```
GreetingHelper
```

---

## Step 3: Add a Method

Add the following method to the class:

```csharp
public static string GetGreeting(string name)
```

### Method Rules

- If `name` is empty or null → return `"Hello, Guest!"`
- Otherwise → return `"Hello, <name>!"`

### Examples

```
Hello, Sharad!
Hello, Guest!
```

---

# Project 2: Console App – GreetingApp

## Step 1: Create Console Application

- Project Type: **Console App (.NET Framework)**
- Project Name: `GreetingApp`

---

## Step 2: Add Reference to Library

- Right-click `GreetingApp`
- Select **Add → Project Reference**
- Select `GreetingLibrary`
- Click **OK**

---

## Step 3: Use the Library

Inside `Main()`:

1. Ask the user to enter their name
2. Call `GreetingHelper.GetGreeting()`
3. Display the returned greeting

---

## Expected Console Interaction

### Input

```
Enter your name:
Sharad
```

### Output

```
Hello, Sharad!
```

---

### Input

```
Enter your name:
```

### Output

```
Hello, Guest!
```

---

## What This Exercise Teaches

| Concept                  | Covered |
|--------------------------|----------|
| Class Library creation   | ✔        |
| Project references       | ✔        |
| Reusing code             | ✔        |
| Static methods           | ✔        |
| Clean separation         | ✔        |
