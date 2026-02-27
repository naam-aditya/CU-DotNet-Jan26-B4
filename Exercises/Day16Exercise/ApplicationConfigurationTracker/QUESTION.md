# Application Configuration Tracker Using Static Members

## Problem Statement

You are required to design a utility class named `ApplicationConfig` that manages application-level configuration and usage statistics using static members only.

This class should not be instantiated and must rely entirely on static properties, static methods, and a static constructor.

---

## Requirements

### 1. Class Design Constraints

- The class must be named `ApplicationConfig`
- No object creation is allowed for this class
- All members must be static
- Include a static constructor

---

### 2. Static Properties

Create the following static properties:

| Property Name    | Type   | Description                              |
|------------------|--------|------------------------------------------|
| ApplicationName  | string | Stores application name                  |
| Environment      | string | Stores environment name (Dev / QA / Prod)|
| AccessCount      | int    | Tracks number of method calls            |
| IsInitialized    | bool   | Indicates whether config is initialized  |

---

### 3. Static Constructor

The static constructor must:

- Assign default values:
  - `ApplicationName = "MyApp"`
  - `Environment = "Development"`
  - `AccessCount = 0`
  - `IsInitialized = false`
- Display a console message:
  ```
  Static constructor executed
  ```

> Note: The static constructor must execute only once, before the first static member access.

---

### 4. Static Methods

#### a) Initialize(string appName, string environment)

- Sets `ApplicationName` and `Environment`
- Sets `IsInitialized = true`
- Increments `AccessCount`

---

#### b) GetConfigurationSummary()

- Increments `AccessCount`
- Returns a formatted string containing:
  - Application Name
  - Environment
  - Access Count
  - Initialization Status

---

#### c) ResetConfiguration()

- Resets all values back to default
- Sets `IsInitialized = false`
- Increments `AccessCount`

---

## 5. Usage Instructions

In `Main()`:

- Access at least one static property (to trigger static constructor)
- Call `Initialize()`
- Call `GetConfigurationSummary()`
- Call `ResetConfiguration()`
- Print returned outputs to console

---

## Expected Learning Outcomes

By completing this assignment, the learner should be able to:

- Differentiate static vs instance members  
- Understand static constructor execution timing  
- Use static properties for shared state  
- Implement utility-style classes  
- Avoid improper object creation  