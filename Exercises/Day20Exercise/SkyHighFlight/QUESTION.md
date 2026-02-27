# Problem Statement: The SkyHigh Flight Aggregator

---

## Background

You are a software engineer for **"SkyHigh,"** a new flight search engine.

The core engine receives a list of flight options from various airlines, but the raw data is unsorted. To provide a high-quality user experience, the system must present these flights in a logical order.

While most users care about the **cheapest option**, power users often want to prioritize:

- Speed (duration)  
- Timing (departure time)  

---

## Requirements

### 1. The Flight Model

Create a `Flight` class that stores:

- `FlightNumber` (string)  
- `Price` (decimal)  
- `Duration` (TimeSpan)  
- `DepartureTime` (DateTime)  

---

### 2. Natural Sorting (Default Behavior)

Implement the `IComparable<T>` interface within the `Flight` class.

- By default, when a list of flights is sorted without specific instructions:
  - Flights must be ordered by **Price (Ascending)**.

---

### 3. Advanced Sorting (Custom Strategies)

Implement the `IComparer<T>` interface in **two separate classes**:

#### DurationComparer
- Sorts flights from **shortest to longest duration**.

#### DepartureComparer
- Sorts flights from **earliest to latest departure time**.

---

### 4. Edge Case Handling

- Ensure comparison logic handles **null objects gracefully**.
- Sorting should not crash if null values are encountered.

---

## Expected Output

Given a disorganized list of flight data, the system should produce three distinct views:

1. **Economy View**  
   - Cheapest flights at the top.

2. **Business Runner View**  
   - Shortest flights at the top.

3. **Early Bird View**  
   - Earliest departing flights at the top.

---

## Success Criteria

- The `Flight` class implements `IComparable<T>` for price-based sorting.  
- Separate comparer classes implement `IComparer<T>`.  
- `List<T>.Sort()` is used correctly for:
  - Default sorting  
  - Custom sorting strategies  
- Code is clean, readable, and follows C# naming conventions.  

---

## Learning Focus

- Interface implementation  
- Natural vs custom sorting  
- Strategy-based comparison  
- Runtime sorting behavior  
- Clean object-oriented design  