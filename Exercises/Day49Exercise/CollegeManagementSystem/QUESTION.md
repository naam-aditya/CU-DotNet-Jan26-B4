# Problem Statement: College Management System

## Objective

Design a **College Management System** that manages students, their subjects, and marks.  
The system must support operations for adding students, removing students, identifying subject toppers, and calculating student averages.

---

# Functional Requirements

---

## 1. Add Student

Method:

```
addStudent(string studentId, string subject, int marks)
```

### Behavior

- Add an entry for the student with the given subject and marks.
- If the student is **already registered for that subject**:
  - Update the marks **only if the new marks are greater than the previous marks**.
- Otherwise, insert the new subject record.

---

## 2. Remove Student

Method:

```
removeStudent(string studentId)
```

### Behavior

- Remove all records associated with the given student.
- This includes:
  - Student subject records
  - Subject-wise student records

---

## 3. Top Subject

Method:

```
topSubject(string subject)
```

### Behavior

- Return the **top scorer(s)** for the given subject.
- If multiple students have the same highest marks:
  - Return them in the **order they were inserted**.

---

## 4. Result

Method:

```
result()
```

### Behavior

- Calculate the **average marks of each student across all subjects**.
- Print each student's result in the format:

```
StudentId AverageMarks
```

- Average must be printed with **2 decimal places**.

---

# Sample Input

```
ADD S1 Math 80
ADD S2 Math 90
ADD S3 Math 90
ADD S1 Phy 90
TOP Math
RESULT
REMOVE S1
```

---

# Sample Output

```
S2 90
S3 90
S1 85.00
S2 90.00
S3 90.00
```

---

# Boilerplate Code

Use the following base structure to implement the system:

```csharp
public class Program
{
    class CollageManagement
    {
        Dictionary<string, Dictionary<string, int>> studentRecords = 
            new Dictionary<string, Dictionary<string, int>>();

        Dictionary<string, LinkedList<KeyValuePair<string, int>>> studentSubjectsOrder = 
            new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();


        Dictionary<string, Dictionary<string, int>> subjectsRecords = 
            new Dictionary<string, Dictionary<string, int>>();

        Dictionary<string, LinkedList<KeyValuePair<string, int>>> subjectsStudentsOrder = 
            new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();

        public int AddStudent(string studentId, string subject, int marks) { }

        public int RemoveStudent(string studentId) { }

        public string TopStudent(string subject) { }

        public string Result() { }
    }

    public static void Main() { }
}
```

---

# Data Structures Used

Structure | Purpose
----------|--------
Dictionary<string, Dictionary<string, int>> | Store student → subjects → marks
Dictionary<string, LinkedList<KeyValuePair<string,int>>> | Preserve insertion order
Subject dictionary | Track subject-wise students and marks
LinkedList | Maintain stable order for tie cases

---

# Key Rules

- Marks should update **only if the new marks are higher**.
- Subject toppers must preserve **insertion order in case of ties**.
- Student removal must clean up **all related records**.
- Average marks must be displayed with **two decimal precision**.

---

# Concepts Covered

- Nested dictionaries  
- LinkedList usage for insertion order  
- Data synchronization across structures  
- Aggregation (average calculation)  
- Conditional updates  
- Multi-index data storage  
- Handling ties with stable ordering  