# Coding Question: Height Addition Program

## Objective

Create a C# program to represent and add the heights of two people.

---

## Requirements

### 1. Create a Class `Height`

Include the following properties:

- `Feet`
- `Inches`

---

### 2. Implement the Following

#### Constructors

- **Default Constructor**
  - Initialize height to:
    - 0 feet
    - 0.0 inches

- **Parameterized Constructor**
  - Initialize `Feet` and `Inches` with given values.

---

#### Method

**AddHeights(Height h2)**

- Adds the current object's height with another `Height` object.
- Returns a new `Height` object containing the total height.
- If inches ≥ 12:
  - Convert extra inches into feet.

---

#### Override `ToString()`

Display height in the format:

```
Height - X feet Y inches
```

---

### 3. In `Main()` Method

- Create two `Height` objects (`person1`, `person2`) using the parameterized constructor.
- Call `AddHeights()` to calculate the combined height.
- Print:
  - Height of person1
  - Height of person2
  - Total height

---

## Sample Output

```
Height - 5 feet 6.5 inches
Height - 5 feet 7.5 inches
Height - 11 feet 2.0 inches
```

---

## Concepts Practiced

- Class and object creation  
- Constructors  
- Method returning objects  
- Basic arithmetic logic  
- Method overriding (`ToString`)  