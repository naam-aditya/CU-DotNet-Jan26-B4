# Challenge: The Secure Terminal

## Background

You are building a terminal interface.  
It needs to handle a secret **Access Code** character-by-character and then allow the user to type a **System Message** using a buffer.

---

## Assignment Tasks

### 1. The Masked PIN (Using ReadKey)

The user must enter a **4-digit PIN**.

#### Requirements

- Use a loop and:
  ```csharp
  Console.ReadKey(true);
  ```
- For every key pressed:
  - Print `*` to the console so the PIN remains hidden.
- Store the digits in a string variable.
- After input is complete, display the entered PIN.
- Use the `KeyChar` property to capture the actual character value.

---

## Expected Behavior

Example interaction:

```
Enter PIN: ****
PIN Entered: 1234
```

---

## Concepts Covered

- Console.ReadKey()
- KeyChar property
- Character-by-character input
- Secure input masking
- String accumulation logic
