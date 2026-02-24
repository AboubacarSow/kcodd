# RA Engine

A small, composable relational-algebra engine that parses relational algebra expressions and transpiles them into SQL queries.

## Goal

Provide a lightweight tool that accepts relational algebra expressions, builds an AST, performs simple logical rewrites/optimizations, and emits readable SQL (targeting standard SQL dialects).

## Features

- Parse textual relational-algebra expressions into an AST
- Support core operators: projection, selection, rename, join, union, difference, intersection
- Support aggregation, grouping, sorting and limits
- Transpile AST to SQL with simple optimizations (push selections, remove redundant projections)
- CLI for quick testing and integration

## Project layout

- `src/core` — core algebra types, AST, and optimizer
- `src/parser` — parser from expression text to AST
- `src/sqlgenerator` — SQL generation from AST
- `src/cli` — small CLI wrapper to run expressions from the command line
- `tests` — unit and integration tests

## Quick start (CLI)

Build and run the CLI project from the repository root:

```powershell
dotnet build src/cli
dotnet run --project src/cli -- "π_{name,age}(σ_{age>30}(employees))"
```

Expected output: a SQL query that selects `name` and `age` from `employees` where `age > 30`.

## Example

Relational algebra:

```
π_{name,age}(σ_{age>30}(employees))
```

Transpiled SQL (illustrative):

```sql
SELECT name, age
FROM employees
WHERE age > 30;
```

## Design notes

- The parser produces a typed AST describing operators and schemas.
- An optimizer performs local rewrites (selection pushdown, projection pruning) before SQL generation.
- The SQL generator targets standard SQL; dialect-specific extensions can be added per backend.
- The engine is intended as a transpiler (relational algebra -> SQL), not a full execution engine.

## Roadmap

1. Parser: support full grammar for expressions, parentheses, and aliases
2. AST & core: define node types and schema propagation
3. SQL generator: produce valid SQL for core operators
4. Optimizer: implement selection pushdown and projection pruning
5. Tests: add end-to-end tests and sample expressions
6. Documentation: extend examples and developer guide



