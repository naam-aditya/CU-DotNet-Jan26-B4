# Assignment: The Smart Kitchen Architect

## Scenario

You have been hired by a home automation startup, **AeroCook**, to build the backend logic for their new line of kitchen appliances.

The company wants a system that is:

- Modular  
- Easy to expand  
- Free from code duplication  

---

## The Problem Statement

Design a software system that models three specific kitchen devices.

You must group shared traits appropriately while ensuring each device remains distinct.

---

## The Device Profiles

### 1. Microwave
- Has a timer.
- Must define its own specific cooking process.

### 2. Electric Oven
- Requires a timer.
- Must connect to local WiFi for remote monitoring.
- Requires a preheating stage before cooking.

### 3. Air Fryer
- A simple mechanical device.
- Cooks quickly.
- Does not have a digital timer.
- Does not have WiFi connectivity.

---

# Your Challenge

---

## 1. Identify the Hierarchy

All devices share common hardware properties:

- Model Name  
- Power Consumption (Watts)  

Questions to address:

- How will you design a base class to store these shared properties?
- Should this base class be directly instantiated, or should it act as a template only?

---

## 2. Identify the Behaviors (Interfaces)

Not every device has the same capabilities.

Some devices:
- Have timers
- Have WiFi connectivity

Others:
- Do not support these features

Questions to address:

- How will you model Timer and WiFi capabilities so that devices are not forced to implement features they do not support?
- What programming construct enforces behavioral contracts across different classes?

---

## 3. Logic and Polymorphism

### Cooking

Every device must be able to:

```
Cook()
```

However, each device implements cooking differently.

Questions to address:

- How will you ensure every device implements Cook()?
- How will you allow each device to define its own cooking logic?

---

### Preheating

Most appliances begin cooking immediately.

However:

- The Electric Oven requires a Preheat step.

Questions to address:

- How can you provide a default Preheat behavior?
- How can the Oven override that behavior without affecting other devices?

---

# Deliverables

## 1. Class Diagram

Provide a description or sketch showing:

- Base class relationships  
- Derived classes  
- Interfaces and their implementations  
- How polymorphism is achieved  

---

## 2. The Code

Implement:

- Base class  
- Derived device classes  
- Interfaces for Timer and WiFi capabilities  
- Proper method overriding  

---

## 3. The Test (Main Method)

In the Main method:

1. Store all three devices in a single list (if possible).
2. Loop through the list and call:

   ```
   Cook()
   ```

3. Demonstrate that:
   - The Oven can connect to WiFi.
   - The Microwave and Air Fryer cannot.

---

## Concepts Covered

- Abstract base classes  
- Interfaces and behavioral contracts  
- Polymorphism  
- Method overriding  
- Default vs overridden behavior  
- Clean architecture design  
- Avoiding code duplication  
- Open/Closed Principle  