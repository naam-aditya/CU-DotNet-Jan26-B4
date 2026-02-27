# Case Study: Smart Access Control Log Processor

## Business Scenario
A secure facility records entry logs in a compact, single-line format to minimize storage.  
Each access attempt is logged as one line of input in the following format:

```
<GateCode>|<UserInitial>|<AccessLevel>|<IsActive>|<Attempts>
```

### Example Input
```
A3|S|5|true|120
```

---

## Field Meaning & Data Types

| Field        | Description                                      | Expected Type |
|-------------|--------------------------------------------------|--------------|
| GateCode     | Gate identifier (single character + digit)       | string       |
| UserInitial  | First letter of user name                        | char         |
| AccessLevel  | Security level (0–9)                             | byte         |
| IsActive     | Whether the user account is active               | bool         |
| Attempts     | Number of access attempts today                  | byte         |

---

## Functional Requirements

### 1. Input Handling (Mandatory)

- Read all values using a single `Console.ReadLine()`
- Split and parse values correctly
- Do not prompt separately for each value

---

### 2. Validation Rules (Tough Part)

Your program must validate the following:

#### 1. GateCode
- Must be exactly 2 characters  
- First character must be a letter  
- Second character must be a digit  

#### 2. UserInitial
- Must be a single uppercase letter  
- Use char APIs (not regex)  

#### 3. AccessLevel
- Must be between 1 and 7  
- Stored as `byte`  

#### 4. IsActive
- Must accept only `true` or `false` (case-insensitive)  

#### 5. Attempts
- Must be ≤ 200  
- Stored as `byte`  

If any validation fails, print:

```
INVALID ACCESS LOG
```

and exit.

---

### 3. Business Logic

If validation succeeds:

- If `IsActive == false`  
  → `ACCESS DENIED – INACTIVE USER`

- Else if `Attempts > 100`  
  → `ACCESS DENIED – TOO MANY ATTEMPTS`

- Else if `AccessLevel >= 5`  
  → `ACCESS GRANTED – HIGH SECURITY`

- Else  
  → `ACCESS GRANTED – STANDARD`

---

### 4. Output Formatting (Mandatory)

On success, display output exactly in this format:

```
Gate      : A3
User      : S
Level     : 5
Attempts  : 120
Status    : ACCESS DENIED – TOO MANY ATTEMPTS
```

Alignment must be clean, using formatting or padding.

---

## Constraints (Important for Assessment)

- ❌ No regex  
- ❌ No exception swallowing  
- ❌ No multiple ReadLine calls  
- ✔ Must use `byte`, `char`, and `bool`  
- ✔ Must use string formatting (`PadRight`, alignment, or interpolation)  

---

## Sample Test Cases

### Input
```
B7|R|6|true|45
```

### Output
```
ACCESS GRANTED – HIGH SECURITY
```

---

### Input
```
C1|m|3|true|10
```

### Output
```
INVALID ACCESS LOG
```

(Reason: lowercase char)

---

### Input
```
A9|K|2|false|20
```

### Output
```
ACCESS DENIED – INACTIVE USER
```

---

## What This Exercise Evaluates

| Skill                  | Tested |
|------------------------|--------|
| String splitting       | ✔ |
| Multi-input parsing    | ✔ |
| byte range reasoning   | ✔ |
| char validation        | ✔ |
| bool parsing           | ✔ |
| Formatting output      | ✔ |
| Real-world logic       | ✔ |