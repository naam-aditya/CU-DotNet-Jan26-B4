# StreamBuzz Creator Engagement Tracker

## Objective

Develop a **C# Console Application** for **StreamBuzz**, a digital content platform that tracks creators' engagement over **4 weeks**.

Each creator has:

- A name  
- Weekly like counts  

The application allows users to:

- Register creators
- Analyze top-performing posts
- Calculate overall engagement statistics

---

# Class Design

## CreatorStats

Create a class:

```
CreatorStats
```

### Properties

Datatype | Property | Description
--------|----------|------------
string | CreatorName | Name of the content creator
double[] | WeeklyLikes | Array storing likes for 4 weeks

---

## EngagementBoard

A static list is already provided in the code template:

```
public static List<CreatorStats> EngagementBoard
```

Purpose:

- Stores all creator records registered in the system.

---

# Methods to Implement

## 1. RegisterCreator

```
public void RegisterCreator(CreatorStats record)
```

### Description

- Adds a creator record to the `EngagementBoard`.

### Behavior

- Accepts a `CreatorStats` object
- Inserts it into the engagement list

---

## 2. GetTopPostCounts

```
public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
```

### Description

Counts how many weeks each creator achieved likes **greater than or equal to the given threshold**.

### Return Value

```
Dictionary<string, int>
```

Where:

- **Key** → Creator name  
- **Value** → Number of weeks meeting the threshold  

### Rules

- Count weeks where:

```
WeeklyLikes >= likeThreshold
```

- If a creator meets the threshold in multiple weeks, count each week.
- If **no creator meets the threshold even once**, return an **empty dictionary**.

---

## 3. CalculateAverageLikes

```
public double CalculateAverageLikes()
```

### Description

Calculates the **average weekly likes across all creators and all weeks**.

### Return

- Average value of all like counts.

---

# Main Method Requirements

In the **Program class**, implement the following behavior.

---

## 1. Menu Display

Display the following menu:

```
1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
```

Prompt:

```
Enter your choice:
```

---

## 2. Choice 1 – Register Creator

Prompt user for:

```
Enter Creator Name:
```

Then ask for weekly likes:

```
Enter weekly likes (Week 1 to 4):
```

Input values for:

- Week 1
- Week 2
- Week 3
- Week 4

After registration display:

```
Creator registered successfully
```

---

## 3. Choice 2 – Show Top Posts

Prompt user for:

```
Enter like threshold:
```

Call:

```
GetTopPostCounts()
```

Display results:

```
CreatorName - WeekCount
```

Example:

```
Nicky - 4
Roma - 1
```

If the method returns an empty dictionary, display:

```
No top-performing posts this week
```

---

## 4. Choice 3 – Calculate Average Likes

Display:

```
Overall average weekly likes: <average>
```

Example:

```
Overall average weekly likes: 1425
```

---

## 5. Choice 4 – Exit

Display:

```
Logging off - Keep Creating with StreamBuzz!
```

Then terminate the program.

Important rule:

- Do **not** use `Environment.Exit()`.

---

# Important Notes

- Keep all **methods and classes public**.
- Do **not modify the provided code template**.
- Follow method signatures exactly.
- Ensure dictionary results are printed properly.
- Maintain clean console interaction.

---

# Sample Input / Output

### Example Execution

```
1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
1

Enter Creator Name:
Nicky

Enter weekly likes (Week 1 to 4):
1500
1600
1800
2000

Creator registered successfully
```

---

```
1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
1

Enter Creator Name:
Roma

Enter weekly likes (Week 1 to 4):
800
1000
1300
1400

Creator registered successfully
```

---

```
1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
2

Enter like threshold:
1400

Nicky - 4
Roma - 1
```

---

```
1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
2

Enter like threshold:
5000

No top-performing posts this week
```

---

```
1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
3

Overall average weekly likes: 1425
```

---

```
1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
4

Logging off - Keep Creating with StreamBuzz!
```

---

# Concepts Covered

- Arrays  
- Lists  
- Dictionaries  
- Aggregation operations  
- Filtering logic  
- Console-based menu systems  
- Data analysis on collections  
- Real-world engagement analytics  