# Problem Statement: OLA Driver Ride Management System

## Objective

Design a small application to manage OLA drivers and their rides.  

Each driver can take multiple rides, and the system should display ride details along with total earnings per driver.

---

## Class Design

---

## 1. Class: `OLADriver`

### Properties

- `Id`
- `Name`
- `VehicleNo`
- `Rides` → `List<Ride>`

> A driver can take multiple rides, so use a `List<Ride>` to store ride details.

---

## 2. Class: `Ride`

### Properties

- `RideID`
- `From`
- `To`
- `Fare`

---

## Application Requirements

1. Create multiple drivers using:
   ```
   List<OLADriver>
   ```

2. Each driver must have multiple rides.

3. For every driver:
   - Display driver details
   - Display all rides taken by that driver
   - Calculate and display total fare earned by the driver

---

## Expected Output Format (Example)

```
Driver Id   : 101
Name        : Rahul
Vehicle No  : KA01AB1234

Ride Details:
Ride 1 : Bangalore → Mysore   | Fare: 1500
Ride 2 : Mysore → Hassan      | Fare: 900

Total Fare Earned: 2400
--------------------------------------------------
```

---

## Concepts Practiced

- Class composition (Driver has Ride list)  
- List<T> usage  
- Iterating nested collections  
- Aggregation (calculating total fare)  
- Clean object-oriented design  