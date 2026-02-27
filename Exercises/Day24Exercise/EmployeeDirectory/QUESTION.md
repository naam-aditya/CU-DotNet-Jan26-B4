# Challenge: The Legacy Employee Directory

## Background

You are tasked with maintaining a legacy system for a company.

Because this is an older system, you are required to use:

```
System.Collections.Hashtable
```

instead of the modern `Dictionary<TKey, TValue>`.

---

## Assignment Tasks

---

## 1. Data Entry

Create a `Hashtable` named:

```
employeeTable
```

Add the following data where:

- Key → Employee ID (`int`)
- Value → Employee Name (`string`)

| Employee ID | Name     |
|-------------|----------|
| 101         | Alice    |
| 102         | Bob      |
| 103         | Charlie  |
| 104         | Diana    |

---

## 2. Unique Key Check

- Check whether ID 105 exists in the table.
- If it does not exist:
  - Add `"Edward"` with ID `105`.
- If it exists:
  - Print:
    ```
    ID already exists.
    ```

---

## 3. Data Retrieval & Boxing

- Retrieve the name of employee 102.
- Store it in a `string` variable.

Note:

Since `Hashtable` returns values as `object`, you must demonstrate:

- Casting (Unboxing)
- Proper type conversion

---

## 4. Iteration

Use a `foreach` loop to print all records in the format:

```
ID: [Key], Name: [Value]
```

Hint:

- You must use the `DictionaryEntry` type while iterating through a `Hashtable`.

---

## 5. Deletion

- Remove employee 103 from the table.
- Print the total count of remaining employees.

---

## Concepts Covered

- Legacy collection usage (`Hashtable`)
- Key-value storage
- Boxing and unboxing
- Type casting
- Iteration using `DictionaryEntry`
- Safe key existence checking
- Collection manipulation
