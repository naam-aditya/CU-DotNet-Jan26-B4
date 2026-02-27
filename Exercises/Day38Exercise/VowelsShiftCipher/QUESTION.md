# Problem Statement: The Vowel-Shift Cipher

## Objective

Create a program that transforms a given **lowercase string** based on specific character-mapping rules.

The goal is to obscure the original text by shifting both vowels and consonants according to predefined transformation logic.

---

## Rules of Transformation

The program must process each character in the input string `s` using the following rules:

---

### 1. Vowel Logic

If the character is a vowel:

```
a, e, i, o, u
```

Replace it with the **next vowel** in the sequence.

#### Vowel Sequence

```
a → e  
e → i  
i → o  
o → u  
u → a   (wrap around)
```

If the character is `u`, it must wrap back to `a`.

---

### 2. Consonant Logic

If the character is a consonant:

- Replace it with the next character in the alphabet.

Example:

```
b → c
c → d
```

#### The Vowel Skip Rule

If the next character is a vowel, you must skip it and move to the following character.

Example:

- `d` → next character is `e` (vowel)  
- Skip `e`  
- Result becomes `f`

---

## Example Walkthrough

### Input

```
abcdu
```

### Step-by-Step Transformation

- `a` → vowel → next vowel is `e`  
- `b` → consonant → next char is `c`  
- `c` → consonant → next char is `d`  
- `d` → consonant → next char is `e` (vowel) → skip → `f`  
- `u` → vowel → wrap to `a`  

---

### Output

```
ecdfa
```

---

## Constraints

- Input will contain only lowercase letters.
- Alphabet shifting must respect vowel-skipping logic.
- Vowel shifting must follow strict sequence mapping.

---

## Concepts Covered

- String manipulation  
- Character classification  
- Alphabet traversal logic  
- Conditional transformations  
- Wrap-around logic  
- Rule-based substitution algorithms  