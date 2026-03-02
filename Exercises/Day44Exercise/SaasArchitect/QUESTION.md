# The Challenge: The SaaS Architect

## 1. The Foundation (Abstraction & Inheritance)

Create an abstract class:

```
Subscriber
```

### Properties

- Guid ID  
- string Name  
- DateTime JoinDate  

### Abstract Method

```
decimal CalculateMonthlyBill();
```

The base class must not be instantiated directly.

---

### Inheritance

Create two subclasses:

#### BusinessSubscriber

Additional Properties:

- FixedRate  
- TaxRate  

Billing Formula:

```
Total = FixedRate × (1 + TaxRate)
```

---

#### ConsumerSubscriber

Additional Properties:

- DataUsageGB  
- PricePerGB  

Billing Formula:

```
Total = DataUsageGB × PricePerGB
```

---

## 2. Identity & Comparison (Equals & Comparable)

To properly manage subscribers in collections, define identity and sorting rules.

---

### Equality Rules

Override:

- Equals()
- GetHashCode()

Two Subscriber objects are equal if:

```
They have the same GUID ID.
```

This ensures correct behavior when checking existence inside collections.

---

### Sorting Rules

Implement:

```
IComparable<Subscriber>
```

Default sort order:

1. JoinDate (Ascending)
2. If JoinDate is the same → sort alphabetically by Name

---

## 3. The Data Structure (Dictionary & Sorting)

In the main program, initialize:

```
Dictionary<string, Subscriber>
```

- Key → Email (string)  
- Value → Subscriber object  

---

### Required Tasks

1. Add at least 5 mixed subscribers (Business and Consumer).
2. Sort the Dictionary by the subscriber’s monthly bill (Descending).

Important Note:

A standard Dictionary is unordered.

To preserve order for reporting, you must:

- Use LINQ to order results  
  or  
- Convert to a List<KeyValuePair<string, Subscriber>>  
  or  
- Use a SortedDictionary with a custom comparer  

---

## 4. Polymorphic Reporting

Create a class:

```
ReportGenerator
```

With a static method:

```
PrintRevenueReport(IEnumerable<Subscriber> subscribers)
```

### Requirements

- Iterate through the collection.
- Call CalculateMonthlyBill() polymorphically.
- Format output in table style.
- Include the specific type of subscriber:
  - Business  
  - Consumer  

Bonus:

- Use StringBuilder for formatted table output.

---

# Key Requirements Checklist

Feature | Implementation Detail
--------|----------------------
Abstraction | Cannot instantiate Subscriber directly
Polymorphism | CalculateMonthlyBill behaves differently for Business vs Consumer
Equality | Identity based on GUID, not object reference
Sorting | Order dictionary by calculated bill (Descending)
Reporting | Output sorted by monthly bill amount

---

## Suggested Billing Formulas

### Business Subscriber

```
Total = FixedRate × (1 + TaxRate)
```

### Consumer Subscriber

```
Total = DataUsageGB × PricePerGB
```

---

## Concepts Covered

- Abstract classes  
- Inheritance  
- Polymorphism  
- Overriding Equals and GetHashCode  
- Implementing IComparable  
- Dictionary usage  
- LINQ sorting  
- Collection transformation  
- StringBuilder formatting  
- Real-world SaaS billing modeling  