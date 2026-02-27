# Challenge: The High-Score Leaderboard

## Background

You are developing the backend for a racing game.  
You need to store player names and their fastest lap times.

The requirement is that the list must always remain sorted by lap time (the Key) so that the leaderboard is ready to display at any moment.

---

## Assignment Tasks

### 1. Initialize the Sorted Collection

Create a collection:

`SortedDictionary<double, string> leaderboard`

- **Key** → Lap time in seconds (`double`)
- **Value** → Player name (`string`)

---

### 2. Populate Data

Add the following records:

- `55.42` → "SwiftRacer"
- `52.10` → "SpeedDemon"
- `58.91` → "SteadyEddie"
- `51.05` → "TurboTom"

---

### 3. Automatic Sorting Verification

Print all players and their lap times.

Observation:

Even though **"TurboTom"** was added last, he should appear first in the output because his lap time (`51.05`) is the lowest.

This confirms that `SortedDictionary` maintains automatic sorting by Key.

---

### 4. Range Logic (Find the "Gold Medal" Time)

Retrieve the first entry in the collection (the fastest time) **without using a loop**.

Hint:

You may use:

```csharp
leaderboard.Keys.First();
```

or

```csharp
leaderboard.First();
```

---

### 5. Update a Record

"SteadyEddie" improved his lap time to `54.00`.

Task:

- Remove the old record (`58.91`)
- Add the new record (`54.00`)

Note:

Keys in a `SortedDictionary` are immutable.  
To change a key, you must delete the old entry and re-insert a new one.

---

## Concepts Covered

- SortedDictionary usage  
- Automatic key-based sorting  
- Key immutability  
- Retrieving first element efficiently  
- Updating sorted collections safely  
- Clean collection manipulation  