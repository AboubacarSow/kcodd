# Relational Algebra DSL Grammar (EBNF)

This document defines the formal grammar for a relational algebra language that uses mathematical notation and is later translated into SQL.

The grammar is written in **EBNF (Extended Backus-Naur Form)**.

---

# 1. Expression (Root Rule)

```ebnf
expression ::= projection
             | selection
             | join
             | rename
             | relation
             | "(" expression ")"
````

## Explanation

An `expression` is the **core unit** of the language.

It can be:

* `projection` (π)
* `selection` (σ)
* `join` (⋈)
* `rename` (ρ)
* a `relation` (table name)
* or another expression inside parentheses (for nesting)

## Concept

This rule introduces **recursion**, meaning expressions can be nested infinitely:

Example:

```
π name (σ age > 20 (Students))
```

---

# 2. Projection (π)

```ebnf
projection ::= "π" attribute_list "(" expression ")"
```

## Explanation

Projection selects specific columns from a relation.

Example:

```
π name, age (Students)
```

* `π` → projection operator
* `attribute_list` → selected columns
* `(expression)` → input dataset

## Concept

Projection is a **unary operator**:

* takes one input
* returns transformed output

---

# 3. Selection (σ)

```ebnf
selection ::= "σ" condition "(" expression ")"
```

## Explanation

Selection filters rows based on a condition.

Example:

```
σ age > 20 (Students)
```

* `σ` → selection operator
* `condition` → filtering logic
* `(expression)` → input relation

## Concept

Selection applies **predicate filtering** (true/false evaluation per row).

---

# 4. Join (⋈)

```ebnf
join ::= expression "⋈" condition expression
```

## Explanation

Join combines two relations based on a condition.

Example:

```
Students ⋈ Students.id = Enrollments.student_id Enrollments
```

* left expression → first table
* right expression → second table
* condition → join predicate

## Concept

Join is a **binary operator**:

* takes two inputs
* produces one combined output

---

# 5. Rename (ρ)

```ebnf
rename ::= "ρ" identifier "(" expression ")"
```

## Explanation

Rename assigns an alias to a relation.

Example:

```
ρ S (Students)
```

* `S` becomes the new name for `Students`

## Concept

Rename is used for:

* aliasing
* simplifying complex expressions
* avoiding name conflicts

---

# 6. Relation

```ebnf
relation ::= identifier
```

## Explanation

A relation is a base table.

Examples:

```
Students
Employees
Orders
```

## Concept

This is a **leaf node** in the expression tree (no children).

---

# 7. Attribute List

```ebnf
attribute_list ::= identifier ("," identifier)*
```

## Explanation

A list of column names.

Examples:

```
name
name, age
id, name, salary
```

## Concept

The `*` means:

* zero or more repetitions of `, identifier`

---

# 8. Condition

```ebnf
condition ::= comparison (logical_op comparison)*
```

## Explanation

A condition is one or more comparisons combined using logical operators.

Example:

```
age > 20 AND name = 'John'
```

## Concept

This enables compound filtering logic.

---

# 9. Comparison

```ebnf
comparison ::= operand comparator operand
```

## Explanation

A basic comparison between two values.

Examples:

```
age > 20
name = 'Alice'
```

---

# 10. Operand

```ebnf
operand ::= identifier | literal
```

## Explanation

An operand can be:

* a column name (`age`)
* a constant value (`20`, `'John'`)

---

# 11. Comparator

```ebnf
comparator ::= "=" | "!=" | "<" | ">" | "<=" | ">="
```

## Explanation

Defines comparison operators:

* equality
* inequality
* ordering

---

# 12. Logical Operators

```ebnf
logical_op ::= "AND" | "OR"
```

## Explanation

Used to combine conditions:

* AND → all must be true
* OR → at least one must be true

---

# 13. Identifier

```ebnf
identifier ::= letter (letter | digit | "_")*
```

## Explanation

Defines valid names for:

* tables
* columns
* aliases

Examples:

```
Students
student_id
age2
```

---

# 14. Literal

```ebnf
literal ::= number | string
```

## Explanation

Represents constant values.

Examples:

```
20
'John'
```

---

# Summary (Big Picture)

This grammar defines a language where:

* **Relations (tables)** are inputs
* **Operators (π, σ, ⋈, ρ)** transform data
* **Expressions are composable and recursive**
* Everything forms an **expression tree**

---

# Key Compiler Concepts Introduced

* DSL (Domain Specific Language)
* EBNF grammar notation
* Recursive grammar rules
* Unary vs binary operators
* Expression trees (AST structure)
* Predicate logic (conditions)
* Token structure (identifiers, literals)
* Language composability

