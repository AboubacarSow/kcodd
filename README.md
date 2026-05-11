
# KCodd

<image src="docs/kcodd_logo.svg" style="text-align:center">

<br>

> **Kern** — the core beneath everything.  
> **Codd** — Edgar F. Codd, who gave the relational model to the world.

A relational algebra engine born from curiosity about database internals — how queries really work beneath the surface of SQL. KCodd parses RA expressions, builds an Abstract Syntax Tree, performs logical optimizations, and transpiles to readable SQL.

---

## What it is

Most developers write SQL without ever thinking about what it means mathematically. KCodd goes the other direction — start from the algebra, understand the primitives, and let the SQL fall out as a consequence.

This is a tool for people who want to understand databases deeply, not just use them.

---

## Features

### ✅ Currently supported

- **Selection (σ)** — filter rows with complex boolean conditions
  - Comparison operators: `=`, `≠`, `>`, `≥`, `<`, `≤`
  - Logical operators: `∧` (AND), `∨` (OR), `¬` (NOT)
  - Nested conditions with parentheses
  - String and numeric literals

- **Projection (π)** — select specific columns with duplicate elimination
  - Single or multiple attribute lists
  - Set semantics (DISTINCT in SQL)

- **Natural Join (⋈)** — join relations on all common attribute names
  - Automatic attribute matching
  - Multi-table join chains

- **Theta Join (⋈θ)** — join with arbitrary conditions
  - Generalises natural join
  - Supports any boolean condition

- **Rename (ρ)** — alias relations for disambiguation
  - Essential for self-joins and complex queries

- **Complex expressions** — full composition of operators
  - Nested and chained operations
  - Proper operator precedence

### 🔜 Planned

- Set operations: Union (∪), Intersection (∩), Difference (−)
- Cartesian Product (×)
- Outer Joins: Left (⟕), Right (⟖), Full (⟗)
- Extended operators: Division (÷), Duplicate Elimination (δ)
- Aggregation & Grouping (γ)
- Sorting (τ)

---

## Quick start

### CLI playground

```bash
cd playground/cli
dotnet run
```

```
σ [age > 18] (Student)
π [name, gpa] (Student)
Student ⋈ Enrolled
Student ⋈θ [Student.id = Enrolled.student_id] (Enrolled)
```

### Web playground

```bash
cd playground/webBlazor
dotnet run
```

Open `https://localhost:7200` for the interactive Blazor interface.

---

## Grammar

```ebnf
expression ::= projection
             | selection
             | join
             | rename
             | relation
             | "(" expression ")"

projection ::= ("π" | "PROJECT") "[" attribute_list "]" "(" expression ")"

selection  ::= ("σ" | "SELECT")  "[" condition "]"      "(" expression ")"

natural-join ::= expression ("⋈" | "JOIN") expression

theta-join   ::= expression ("⋈" | "JOIN") "[" condition "]" expression

rename ::= ("ρ" | "RENAME") identifier "(" expression ")"

condition    ::= disjunction
disjunction  ::= conjunction (("∨" | "or") conjunction)*
conjunction  ::= negation_expr (("∧" | "and") negation_expr)*
negation_expr ::= ("¬" | "NOT") negation_expr | comparison | "(" condition ")"
comparison   ::= operand comparator operand
comparator   ::= "=" | "≠" | "<" | ">" | "<=" | ">="
```

See [`src/grammar/grammar.ebnf`](src/grammar/grammar.ebnf) for the complete formal grammar.

---

## Examples

### Selection with complex condition
```
σ [age > 18 ∧ gpa ≥ 3.0] (Student)
```
```sql
SELECT *
FROM Student
WHERE age > 18 AND gpa >= 3.0;
```

### Projection after selection
```
π [name, gpa] (σ [age > 18] (Student))
```
```sql
SELECT DISTINCT name, gpa
FROM Student
WHERE age > 18;
```

### Natural join
```
Student ⋈ Enrolled
```
```sql
SELECT *
FROM Student
NATURAL JOIN Enrolled;
```

### Theta join
```
Student ⋈θ [Student.id = Enrolled.student_id] Enrolled
```
```sql
SELECT *
FROM Student
JOIN Enrolled ON Student.id = Enrolled.student_id;
```

### Multi-table pipeline
```
π [name, title]
  (Student
   ⋈θ [Student.id = Enrolled.student_id] Enrolled
   ⋈θ [Enrolled.course_id = Course.id] Course)
```
```sql
SELECT DISTINCT name, title
FROM Student
JOIN Enrolled ON Student.id = Enrolled.student_id
JOIN Course   ON Enrolled.course_id = Course.id;
```

---

## Architecture

```
kcodd/
├── src/
│   ├── core/           # AST node definitions and core logic
│   ├── lexer/          # Lexical analysis
│   ├── parser/         # Syntax parsing
│   ├── sqlgenerator/   # SQL code generation
│   ├── transpiler/     # Main transpilation service
│   └── grammar/        # Formal grammar definitions
├── playground/
│   ├── cli/            # Command-line interface
│   └── webBlazor/      # Blazor web playground
├── tests/              # Unit and integration tests
└── docs/
    ├── RELATIONAL_ALGEBRA.md   # Operator reference with SQL equivalents
    ├── architecture.md         # System design notes
    └── learning-roadmap.md     # Development progression
```

### Pipeline

```
Expression string
      ↓  Lexer
    Tokens
      ↓  Parser
      AST
      ↓  Optimizer   (selection pushdown, projection pruning)
Optimized AST
      ↓  SQL Generator
    SQL string
```

### Design principles

- **Composability** — operators nest arbitrarily
- **Set semantics** — pure RA behavior, duplicate elimination
- **Optimization** — logical rewrites before generation
- **Extensibility** — clean architecture for new operators
- **Standards compliance** — targets standard SQL dialects

---

## Building

```bash
# Build everything
dotnet build kcodd.sln

# Run tests
dotnet test tests/tests.csproj

# CLI
dotnet run --project playground/cli/cli.csproj

# Web
dotnet run --project playground/webBlazor/webBlazor.csproj
```

**Prerequisites:** .NET 9 or later.

---

## Why KCodd

The name carries the two ideas that motivated the project:

**Kern** — the innermost layer. The impulse to go below the surface, past the ORM, past the query planner, down to the mathematical primitives that make relational databases work.

**Codd** — Edgar F. Codd (1923–2003), IBM researcher who published *A Relational Model of Data for Large Shared Data Banks* in 1970. Every SQL database in existence traces back to that paper. KCodd is a small act of understanding what he actually built.

---

## Documentation

- [Relational Algebra Reference](docs/RELATIONAL_ALGEBRA.md) — complete operator guide with SQL equivalents
- [Architecture Notes](docs/architecture.md) — system design and implementation details
- [Learning Roadmap](docs/learning-roadmap.md) — development progression and milestones

---

## License

Open source. See LICENSE for details.