# Technical Challenge: The "Eco-Drive" Vehicle Simulation

## Background

You are developing a vehicle simulation engine for a logistics company.  
The company operates different types of vehicles, and they need a system to calculate fuel consumption and display travel statuses.

Each vehicle type handles **movement differently** based on its mechanical constraints.

---

## Objective

Design a class hierarchy that allows a central controller to manage a fleet of vehicles.

The controller should be able to trigger movement for all vehicles in a single loop, without knowing the specific details of each vehicle type.

---

## Requirements

---

## 1. The Base Class: `Vehicle`

- Define a class `Vehicle` that **cannot be instantiated directly** (use the `abstract` keyword).
- Add a property:
  - `ModelName` (string)
- Add an **Abstract Method**:
  - `Move()`
    - Forces every vehicle to define its own movement logic.
- Add a **Virtual Method**:
  - `GetFuelStatus()`
    - Default implementation should return:
      ```
      Fuel level is stable.
      ```

---

## 2. The Derived Classes

Create three specific vehicle types that inherit from `Vehicle`:

---

### `ElectricCar`

- Override `Move()` to print:
  ```
  [ModelName] is gliding silently on battery power.
  ```
- Override `GetFuelStatus()` to return:
  ```
  [ModelName] battery is at 80%.
  ```

---

### `HeavyTruck`

- Override `Move()` to print:
  ```
  [ModelName] is hauling cargo with high-torque diesel power.
  ```
- Do **not** override `GetFuelStatus()`
  - It should use the default base class behavior.

---

### `CargoPlane`

- Override `Move()` to print:
  ```
  [ModelName] is ascending to 30,000 feet.
  ```
- Override `GetFuelStatus()`:
  - Call the base implementation first.
  - Then add:
    ```
    Checking jet fuel reserves...
    ```
  - Combine both strings into the final output.

---

## 3. The Fleet Controller

In the `Main()` method:

1. Create an array of `Vehicle` containing:
   - One `ElectricCar`
   - One `HeavyTruck`
   - One `CargoPlane`

2. Iterate through the array and, for each vehicle:
   - Call `Move()`
   - Call `GetFuelStatus()` and print the result

---

## Success Criteria

- ❌ No Interfaces  
- ✔ Must achieve functionality strictly through class inheritance  
- ✔ Runtime method selection must work correctly (base, override, or combination using `base`)  
- ✔ The array loop must **not** use `if` or `switch` statements to check vehicle types  
- ✔ Demonstrate abstraction and runtime polymorphism  

---

## Learning Focus

- Abstract classes  
- Method overriding  
- Virtual methods  
- Use of `base` keyword  
- Runtime polymorphism  
- Clean OOP design  