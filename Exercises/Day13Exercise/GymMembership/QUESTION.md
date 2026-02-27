# Problem Statement: Gym Membership Monthly Amount Calculator

## Objective

Write a method to calculate the **monthly membership amount** for joining a Gym, depending on the various services selected.

At least one optional service must be selected.  
If no optional service is selected, an additional ₹200 should be added.

---

## Pricing Details

### Fixed Charges (Mandatory)

- Monthly Base Fee: **₹1000**

---

### Optional Services

| Service          | Cost (₹) |
|------------------|----------|
| Tread-Mill       | 300      |
| Weight Lifting   | 500      |
| Zumba Classes    | 250      |

---

### Tax

- GST: **5%** on the total amount (after adding services and any penalty).

---

## Functional Requirements

1. The method should calculate:
   - Base fee
   - Selected optional services
   - Additional ₹200 charge if no service is selected
   - GST (5%)
   - Final payable amount

2. At least one service is mandatory.
   - If no optional service is selected:
     - Add ₹200 penalty before calculating GST.

3. The method should return or display:
   - Base amount
   - Service charges
   - GST amount
   - Final payable amount

---

## Example Scenario 1

Selected:
- Tread-Mill
- Zumba Classes

Calculation:
- Base: ₹1000
- Services: ₹300 + ₹250 = ₹550
- Subtotal: ₹1550
- GST (5%): ₹77.50
- Final Amount: ₹1627.50

---

## Example Scenario 2

Selected:
- No optional services

Calculation:
- Base: ₹1000
- Penalty: ₹200
- Subtotal: ₹1200
- GST (5%): ₹60
- Final Amount: ₹1260

---

## Constraints

- Use method parameters to represent selected services.
- Do not use advanced collections.
- Keep calculation logic separate from display logic.
- Ensure proper decimal calculations for GST.

---

## Learning Outcomes

By completing this exercise, learners will:

- Practice conditional logic
- Perform percentage calculations
- Apply business rule validation
- Structure financial calculations correctly
- Design reusable methods