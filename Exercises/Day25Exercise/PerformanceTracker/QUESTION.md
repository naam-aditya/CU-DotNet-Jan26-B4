# Project Title: Cricket Player Performance Tracker

## Background

The **HackerLand Cricket League** needs a tool to process player statistics stored in a CSV file.  

The tool must:

- Calculate specific performance metrics  
- Handle data irregularities  
- Output the final results into a sorted collection  

---

## 1. Data Structure – The `Player` Class

Create a `Player` class with the following properties:

- `Name` (string)  
- `RunsScored` (int)  
- `BallsFaced` (int)  
- `IsOut` (bool)  
- `StrikeRate` (double) – Calculated  
- `Average` (double) – Calculated  

---

## 2. Business Logic Calculations

### Strike Rate (SR)

The number of runs scored per 100 balls faced.

Formula:

SR = (RunsScored / BallsFaced) × 100

---

### Batting Average (Avg)

Total runs scored divided by the number of times the player was out.

Rule:

- If `IsOut = false`, then:
  
  Average = RunsScored

---

## 3. Requirements

---

### A. File Processing (CSV)

Read a file named:

`players.csv`

Format:

```
Name, Runs, Balls, IsOut
```

Example:

```
Steve Smith, 84, 90, True
Virat Kohli, 29, 35, False
Joe Root, 110, 120, True
```

---

### B. Exception Handling

Handle the following scenarios gracefully:

- `FileNotFoundException`  
  - If the CSV file is missing, print a user-friendly error message.

- `FormatException`  
  - If "Runs" or "Balls" values are not valid integers.

- `DivideByZeroException`  
  - Ensure the program does not crash if `BallsFaced = 0` while calculating Strike Rate.

---

### C. Collections and Sorting

- Store all valid `Player` objects in:
  
  `List<Player>`

- Filter out players who have faced fewer than 10 balls.
- Sort the final list by:
  
  `StrikeRate` in descending order.

---

## 4. Assignment Tasks

1. Create a Console Application that prompts the user for the CSV file path.
2. Parse the file line-by-line using:
   ```csharp
   string.Split(',');
   ```
3. Perform calculations and populate `Player` objects.
4. Display the results in a formatted table in the console.

---

## 5. Sample Output

```
Name            Runs    SR      Avg
---------------------------------------
Joe Root        110     91.67   110.00
Steve Smith     84      93.33   84.00
Virat Kohli     29      82.86   29.00
```

---

## Concepts Covered

- File handling in .NET  
- CSV parsing  
- Exception handling  
- Defensive programming  
- Object modeling  
- List<T> filtering  
- Sorting with custom logic  
- Console table formatting  