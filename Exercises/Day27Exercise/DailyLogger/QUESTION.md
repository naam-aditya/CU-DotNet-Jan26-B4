# Assignment: The Daily Logger

## Objective

Create a console application that prompts the user for a **Daily Reflection**.

Each time the program runs:

- The user’s input must be saved to a file named:

  `journal.txt`

- The program must **not overwrite previous entries**.
- New reflections must be appended to the end of the file.

---

## Functional Requirements

1. Prompt the user to enter their daily reflection.
2. Open or create the file `journal.txt`.
3. Append the reflection to the file.
4. Ensure previous content remains intact.

---

## Step 1: Code Implementation Requirement

Use the `StreamWriter` constructor that accepts:

- A file path
- A boolean value to enable append mode

Example pattern:

```csharp
new StreamWriter(filePath, true)
```

---

## Key Technical Details

### The Boolean Toggle

In:

```csharp
new StreamWriter(filePath, true)
```

- `true` → Appends content to the end of the file.
- `false` (or omitted) → Overwrites the file.

This boolean flag controls whether the system:

- Seeks to the end of the file before writing  
  **or**
- Clears the file and starts fresh.

---

## Expected Behavior

If the program runs multiple times:

Run 1:
```
Today I learned about file handling.
```

Run 2:
```
Practiced exception handling.
```

Final `journal.txt` content:

```
Today I learned about file handling.
Practiced exception handling.
```

---

## Concepts Covered

- File handling in .NET  
- StreamWriter usage  
- Append vs overwrite behavior  
- Console input handling  
- Persistent data storage  