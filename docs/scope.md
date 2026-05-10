# Relational Algebra → SQL Engine
## Scope Definition (v1)

---

## 1. Project Goal

This project implements a compiler-style pipeline that translates
Relational Algebra (RA) expressions into SQL queries.

The system does NOT execute queries.
It generates SQL that can be executed in a PostgreSQL-compatible database.

---

## 2. Supported Operators (v1)

The following Relational Algebra operators are supported:

- Selection (σ)
- Projection (π)
- Natural Join (⨝)
- Theta Join (⨝θ)
- Rename (ρ)


- Union (∪)
- Difference (−)
- Aggregation (γ)

### Operator Notes

- Selection supports simple boolean predicates.
- Join supports only INNER JOIN.
- Aggregation supports GROUP BY and basic aggregate functions (COUNT, SUM, AVG, MIN, MAX).
- Union and Difference require compatible schemas.

---

## 3. SQL Target Dialect

The engine generates SQL compatible with:

- PostgreSQL
- ANSI SQL

Generated SQL will follow standard SQL logical order:

FROM → JOIN → WHERE → GROUP BY → HAVING → SELECT

---

## 4. Input Syntax

The engine uses symbolic Relational Algebra syntax.

Example:

π [name] (
    σ [age > 18] (
        Users
    )
)

Nested expressions are fully supported.

Parentheses are required for explicit structure.

---

## 5. Semantic Model

- Relations are assumed to exist externally in the database.
- Attributes are resolved during semantic validation.
- Relational Algebra follows set semantics conceptually.
- SQL generation respects SQL bag (multiset) semantics.

Duplicate handling may be controlled using DISTINCT when necessary.

---

## 6. Not Supported (v1)

The following features are intentionally excluded:

- Outer joins (LEFT, RIGHT, FULL)
- Correlated subqueries
- Window functions
- Division operator
- Cost-based optimization
- Physical query execution
- Index management
- Storage engine implementation
- Transaction management
- SQL parsing (reverse direction)

These exclusions prevent scope explosion and maintain clarity.

---

## 7. Out of Scope (Future Work)

Future extensions may include:

- Logical optimization (predicate pushdown, projection pruning)
- Cost-based optimization
- In-memory execution engine
- Physical plan generation
- Index simulation
- Statistics-based planning

---

## 8. Versioning Strategy

This document defines Version 1 of the engine.

Any additional feature must require:

- Explicit scope update
- Version increment
- Architectural impact review