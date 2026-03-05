# The Cargo Manifest Optimizer

## 1. The Data Structure

You are managing a nested structure:

```
List<List<Container>>
```

This represents a **Cargo Bay**.

### Structure Meaning

- The **Outer List** → Represents Rows in the cargo bay  
- The **Inner List** → Represents Containers within that row  
- Each **Container** → Holds its own `List<Item>`  

---

### Class Structure

#### Item

- string Name  
- double Weight  
- string Category  
  - Example categories:
    - "Electronics"
    - "Perishables"
    - "Tech"
    - "Food"
    - "Furniture"

---

#### Container

- string ContainerID  
- List<Item> Items  

---

# 2. The Requirements

## Task A: The "Deep Search" (Filtering)

Create a method:

```
FindHeavyContainers(double weightThreshold)
```

### Requirement

Return:

```
List<string>
```

Containing ContainerIDs where:

- The total weight of all items inside the container  
- Exceeds the specified threshold  

### Important

- You must traverse:
  - All rows  
  - All containers  
- Handle nested structure properly  

---

## Task B: The "Category Audit" (Grouping)

Create a method:

```
GetItemCountsByCategory()
```

### Requirement

Return:

```
Dictionary<string, int>
```

Where:

- Key → Category name  
- Value → Total count of items in that category  
- Across the entire nested structure  

---

## Task C: The "Structural Reorganizer" (Transformation)

This is the most complex task.

Create a method:

```
FlattenAndSortShipment()
```

### Step 1 – Flatten

Convert:

```
List<List<Container>>
```

Into:

```
List<Item>
```

---

### Step 2 – Remove Duplicates

- Remove duplicate items  
- Duplicates are identified by:
  - Item Name  

---

### Step 3 – Sort

Sort the final list by:

1. Category (Alphabetical)  
2. Weight (Descending)  

---

# 3. Constraints & Complexity


## Null Safety

Your implementation must safely handle:

- A row that is empty  
- A container that has no items  
- Potential null references  

Your code must not throw:

```
NullReferenceException
```

---

## Performance Requirement

For Task C:

- Use LINQ `SelectMany()` to flatten nested collections  
- Compare clarity against triple nested `foreach` loops  

---

# Example Data for Testing

The Cargo Bay includes:

---

## Row 0 – High-Value Tech Row

Containers:

- C001
  - Laptop (2.5, Tech)
  - Monitor (5.0, Tech)
  - Smartphone (0.5, Tech)

- C104
  - Server Rack (45.0, Tech)  ← Heavy Item
  - Cables (1.2, Tech)

---

## Row 1 – Mixed Consumer Goods

Containers:

- C002
  - Apple (0.2, Food)
  - Banana (0.2, Food)
  - Milk (1.0, Food)

- C003
  - Table (15.0, Furniture)
  - Chair (7.5, Furniture)

---

## Row 2 – Fragile & Perishables

Containers:

- C205
  - Vase (3.0, Decor)
  - Mirror (12.0, Decor)

- C206
  - No items (Empty container – Edge Case)

---

## Row 3 – Edge Case

- Row exists  
- No containers inside  

---

# Concepts Covered

- Nested collections  
- Deep traversal  
- LINQ SelectMany  
- Aggregation with Sum  
- Grouping with GroupBy  
- Dictionary projection  
- Sorting with multiple keys  
- Duplicate elimination  
- Null safety handling  
- Performance optimization  
- Real-world hierarchical data processing  