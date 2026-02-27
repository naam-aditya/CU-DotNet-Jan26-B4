# Problem Statement: Automated Trip Expense Equalizer

---

## Context

When groups of friends or colleagues travel together, expenses are often paid by different individuals at different times.

At the end of the trip, the total expenditure must be shared equally among all participants.

Manually calculating "who owes whom" becomes complex, especially when multiple people have paid different amounts and others have paid nothing at all.

---

## Objective

Develop a settlement engine that:

- Accepts a list of individuals and their total spending  
- Calculates the fair share for each person  
- Generates a minimal number of transactions to settle all debts  

---

## Functional Requirements

### 1. Calculate Fair Share

- Compute the total group expenditure.
- Divide by the number of participants.

Fair Share Formula:

```
Fair Share = Total Spent / Number of Participants
```

---

### 2. Identify Balances

Determine each participant’s net balance:

- **Debtors**
  - Those who spent less than the fair share.
  - Owe money to the group.

- **Creditors**
  - Those who spent more than the fair share.
  - Must be reimbursed.

---

### 3. Algorithmic Settlement

Implement a settlement algorithm that:

- Matches debtors with creditors.
- A debtor pays their remaining balance to a creditor.
- Continue until:
  - The debtor’s balance becomes zero, or
  - The creditor is fully reimbursed.
- Repeat until all balances are settled.

Goal:

- Generate the **minimal number of transactions** required to balance the group.

---

### 4. Data Export

Output the settlement results in CSV format:

```
Payer,Receiver,Amount
```

Requirements:

- Currency values rounded to **two decimal places**
- Ensure financial accuracy

---

## Example Scenario

### Input Data

- Aman: Paid 900  
- Soman: Paid 0  
- Kartik: Paid 1290  

---

### Calculations

1. Total Spent:

   2190

2. Fair Share (Per Person):

   2190 / 3 = 730

3. Net Balances:

- Aman: +170 (Creditor)  
- Soman: -730 (Debtor)  
- Kartik: +560 (Creditor)  

---

### Expected Output (CSV)

```
Payer,Receiver,Amount
Soman,Aman,170.00
Soman,Kartik,560.00
```

---

## Concepts Covered

- Aggregation and average calculation  
- Financial rounding and precision  
- Debt settlement algorithms  
- Collection processing  
- CSV generation  
- Real-world financial modeling  
- Algorithmic matching (Debtor–Creditor pairing)  