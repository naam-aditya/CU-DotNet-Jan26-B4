# Case Study: Simple User Login Message Processor

## Business Scenario
A console application displays login status messages entered by different users.  
Because users type messages manually, the input may contain extra spaces and mixed casing.  
Your task is to clean the message, check its meaning, and display a formatted result.

---

## Input Specification (Single ReadLine Only)

The program receives one line of input in the following format:

```
<UserName>|<LoginMessage>
```

### Example Input
```
Sharad|  LOGIN Successful  
```

---

## Functional Requirements

### 1. String Operations (Entry Level)

Clean the `LoginMessage`:

1. Remove leading and trailing spaces using `Trim()`
2. Convert the message to lowercase using `ToLower()`

Example:

```
"  LOGIN Successful  "
→ "login successful"
```

---

### 2. String Search

Check whether the cleaned message contains the word:

```
"successful"
```

- Use `Contains()` or `IndexOf()`
- Case-insensitive (already handled by `ToLower()`)

---

### 3. String Comparison

The system defines a standard login success message:

```
"login successful"
```

Compare the cleaned message with the standard message using:

```
Equals()
```

Determine:

- Whether the message is standard
- Or custom

---

### 4. Business Rules

- If the keyword **“successful”** is not found  
  → `STATUS: LOGIN FAILED`

- If the keyword is found and message matches standard  
  → `STATUS: LOGIN SUCCESS`

- If the keyword is found but message does not match standard  
  → `STATUS: LOGIN SUCCESS (CUSTOM MESSAGE)`

---

### 5. Formatted Display (Mandatory)

Display the output exactly in this format:

```
User     : Sharad
Message  : login successful
Status   : LOGIN SUCCESS
```

Requirements:

- Labels aligned  
- Clean spacing  
- No extra blank lines  

---

## Constraints (Beginner Friendly)

- ✔ One `Console.ReadLine()` only  
- ✔ Use basic string APIs:
  - `Trim`
  - `ToLower`
  - `Contains` or `IndexOf`
  - `Equals`
- ❌ No regex  
- ❌ No culture APIs  

---

## Sample Test Cases

### Input
```
Amit| Login successful 
```

### Output Status
```
LOGIN SUCCESS
```

---

### Input
```
Neha|login successful today
```

### Output Status
```
LOGIN SUCCESS (CUSTOM MESSAGE)
```

---

### Input
```
Ravi|invalid password
```

### Output Status
```
LOGIN FAILED
```

---

## What This Exercise Helps Learners Practice

| Concept                 | Level        |
|--------------------------|-------------|
| Trim & case conversion   | Basic       |
| String search            | Basic       |
| String comparison        | Basic       |
| Console formatting       | Basic       |
| Real-world thinking      | Introductory|