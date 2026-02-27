# Problem Statement: Employee Class Design

## Objective

Create a class `Employee` with the following members and rules.

---

## Data Members & Properties

### 1. Id
- Type: `int`
- Private data member
- Accessed using explicit getter and setter methods

---

### 2. Name
- Type: `string`
- Auto-implemented property

---

### 3. Department
- Type: `string`
- Full property
- Allowed values only:
  - Accounts
  - Sales
  - IT

---

### 4. Salary
- Type: `int`
- Full property
- Allowed range:
  - Minimum: 50000
  - Maximum: 90000

---

## Methods

### Display()

- Displays all employee details:
  - Id
  - Name
  - Department
  - Salary

---

## Constraints

- Use proper encapsulation.
- Validate Department and Salary inside their respective properties.
- Follow clean coding practices.