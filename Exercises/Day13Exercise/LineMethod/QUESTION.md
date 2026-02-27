# Problem Statement: Line Display Method (Method Overloading)

## Objective

Create a method that displays a horizontal line in the console.  
The method must support multiple ways of being called using different parameters.

---

## Functional Requirements

### 1. Default Call (No Parameters)

If the method is called without passing any arguments:

- It should display a line of **40 dash (`-`) characters**

Example Output:

```
----------------------------------------
```

---

### 2. Single Parameter (Character Only)

If the method is called by passing a character (for example `+`):

- It should display **40 repetitions** of the given character.

Example:

Input:
```
'+'
```

Output:
```
++++++++++++++++++++++++++++++++++++++++
```

---

### 3. Two Parameters (Character and Length)

If the method is called by passing:

- A character (e.g., `$`)
- A number (e.g., `60`)

It should display the given character repeated the specified number of times.

Example:

Input:
```
'$', 60
```

Output:
```
$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
```

---

## Constraints

- Use method overloading.
- The default length should be 40 characters.
- The method should only handle display logic.
- Do not use advanced collections or external libraries.

---

## Learning Outcomes

By completing this exercise, learners will:

- Understand method overloading.
- Practice parameter handling.
- Work with loops for repetitive output.
- Apply clean and reusable method design.