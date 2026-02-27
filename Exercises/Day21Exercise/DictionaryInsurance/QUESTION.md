# Problem Statement: The "SafeDrive" Policy Optimizer

---

## Background

You are a software engineer at **SafeDrive Insurance**.  

The company manages thousands of auto insurance policies and needs a high-performance system to handle annual renewals and risk assessments.

Management has noted that inflation and changes in driver behavior require a bulk update to their records.

---

## Objective

Develop a **PolicyTracker** system that uses a `Dictionary` to store policy data:

- **Key** → Unique string `Policy ID`
- **Value** → `Policy` object

The system must process financial updates and filter data based on specific criteria **without losing data integrity**.

---

## Requirements

---

## 1. Data Structure

Create a `Policy` object that tracks:

- `HolderName` (string)  
- `Premium` (decimal)  
- `RiskScore` (int, range 1–100)  
- `RenewalDate` (DateTime)  

---

## 2. Mandatory Operations

### The "Bulk Adjustment"

Implement a method that:

- Increases the `Premium` of all policies by **5%**
- Only if `RiskScore > 75`

---

### The "Clean-Up"

Implement a method that:

- Removes all policies from the dictionary  
- Where `RenewalDate` is older than **3 years**

This ensures compliance with data retention laws.

---

### The "Security Check"

Implement a safe lookup method that:

- Retrieves a policy’s details using its Policy ID
- If the ID does not exist:
  - Return a custom **"Not Found"** message
  - Do not allow the program to crash

---

## Constraints & Rules

### 1. Financial Accuracy
- All premium calculations must use the `decimal` type.
- Avoid floating-point precision errors.

### 2. Collection Safety
- You **cannot modify a Dictionary while iterating using foreach**.
- Use a safe workaround:
  - Example: Store keys to remove in a separate list, then delete them.

### 3. Efficiency
- Lookup operations must leverage the average **O(1)** time complexity of `Dictionary`.

---

## Learning Focus

- Dictionary usage with key-value pairs  
- Safe collection modification  
- Financial calculation accuracy using `decimal`  
- Bulk data processing  
- Efficient lookup strategies  
- Clean defensive programming  