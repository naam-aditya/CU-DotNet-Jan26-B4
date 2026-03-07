# Student Marks Management Using Dictionary

## Objective

Create a system to manage student records and their marks using a **Dictionary** in C#.

The system must ensure:

- Students are not duplicated
- Marks are updated only when the new marks are higher
- The latest data is displayed for all students

---

# Class Design

Create a class:

```
Student
```

### Properties

- StudId  
- SName  

---

# Data Structure

Create a dictionary to store student records and their marks.

```
Dictionary<Student, int>
```

Where:

- **Key** → Student object  
- **Value** → Marks obtained by the student  

---

# Functional Requirements

---

## 1. Add Student

Add a student to the dictionary.

Behavior:

- If the student **does not exist**, insert the new student object with marks.
- If the student **already exists**:
  - Compare the existing marks with the new marks.
  - Update the marks **only if the new marks are greater** (marks improvement).

---

## 2. Avoid Duplicate Students

Ensure that students are not duplicated in the dictionary.

Students should be considered the **same student** if their:

- `StudId` matches.

To achieve this, override:

- `Equals()`
- `GetHashCode()`

in the `Student` class.

---

## 3. Update Marks (Improvement Rule)

If a student already exists:

- Only update the marks if the new marks are **greater than the previous marks**.

Example:

| Student | Existing Marks | New Marks | Action |
|--------|---------------|-----------|--------|
| S1 | 70 | 65 | Ignore |
| S1 | 70 | 85 | Update |

---

## 4. Display Latest Data

Print all student records with their latest marks.

Format:

```
StudId  SName  Marks
```

Example:

```
S1  Rahul   85
S2  Anita   90
S3  Kiran   78
```

---

# Concepts Covered

- Custom class as Dictionary key  
- Overriding `Equals()` and `GetHashCode()`  
- Conditional updates in dictionaries  
- Object comparison  
- Data integrity handling  
- Collection traversal  