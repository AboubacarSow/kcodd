# Relational Algebra → SQL Engine
## System Architecture

---

## 1. High-Level Pipeline

The system follows a classical compiler architecture:

```
Input Text
   |
  [Lexer]
   |
  [Parser]
   |
  [AST] (Abstract Syntax Tree)
   |
  [Logical Plan]
   |
  [SQL Generator]
   |
SQL Output
```

Each stage transforms the representation into a higher semantic level.  The diagram above is a linear, unidirectional flow where each box is implemented as its own module.

To emphasize flow, here's a horizontal version as well:

```
Input Text -> Lexer -> Parser -> AST -> Logical Plan -> SQL Generator -> SQL Output
```

Both diagrams aim to make the compiler-style pipeline explicit.

---

## 2. Architectural Principles

- Clear separation of concerns
- No circular dependencies
- Each layer depends only on the layer directly below it
- Core logic independent from CLI
- SQL generation independent from parsing logic

---

## 3. Module Responsibilities

### 3.1 Lexer

Responsible for:

- Converting raw input text into tokens
- Handling whitespace
- Identifying symbols (π, σ, ⨝, etc.)
- Producing token stream for parser

The lexer has no knowledge of SQL or semantics.

---

### 3.2 Parser

Responsible for:

- Consuming tokens
- Validating grammar rules
- Constructing Abstract Syntax Tree (AST)
- Reporting syntax errors

The parser produces a structural representation only.

It does not validate schemas or generate SQL.

---

### 3.3 Abstract Syntax Tree (AST)

Represents the syntactic structure of the input.

Each relational operator maps to a node type:

- ProjectionNode
- SelectionNode
- JoinNode
- RenameNode
- UnionNode
- DifferenceNode
- AggregationNode
- RelationNode

AST is syntax-focused, not semantic.

---

### 3.4 Logical Plan

Transforms AST into a semantic representation.

Logical Plan represents:

- Query intent
- Operator hierarchy
- Data flow between operators

Logical nodes include:

- ScanNode
- FilterNode
- ProjectNode
- JoinNode
- AggregateNode
- UnionNode
- DifferenceNode

Logical Plan is independent from SQL syntax.

---

### 3.5 SQL Generator

Transforms Logical Plan into SQL string.

Responsibilities:

- Correct SELECT clause construction
- JOIN clause generation
- WHERE clause placement
- GROUP BY handling
- Alias management
- Subquery generation when needed

SQL Generator does not perform optimization in v1.

---

### 3.6 CLI (Optional Layer)

Provides:

- Interactive input
- Execution pipeline trigger
- SQL output display
- Error reporting

CLI depends on all layers.
No other layer depends on CLI.

---

## 4. Dependency Rules

The following dependencies are allowed:

```
          +-----------+
          |   CLI     |
          +-----------+
             |  ^
             |  |
     +-------+  |      +-----------+
     |          |      | SQLGen    |
     |          |      +-----------+
 +--------+     |           ^
 |Lexer   |     |           |
 +--------+     |      +----+------+
      |         |      |Logical    |
      v         |      |Plan       |
   +-------+    |      +----+------+
   |Parser|---- +           |
   +-------+                v
      |                 +-----+
      v                 | AST |
    +-----+             +-----+
    | AST |
    +-----+
```

In this ASCII diagram, arrows point in the direction of allowed dependencies. The CLI sits at the top and may depend on every other component, but no component depends back on the CLI.  The lexer and AST are leaf nodes with no incoming arrows.

The following dependencies are forbidden (to prevent tight coupling):

- SQLGenerator → Parser
- Parser → SQLGenerator
- LogicalPlan → CLI
- AST → SQLGenerator

Sticking to these rules ensures layers remain acyclic and responsibilities remain clean.

---

## 5. Error Handling Strategy

Two categories of errors:

1. Syntax Errors (Parser stage)
2. Semantic Errors (Logical Plan stage)

SQL generation occurs only after validation passes.

---

## 6. Future Extension Points

The architecture is designed to allow:

```
LogicalPlan
    |
    +-> Optimizer
    |       |
    |       +-> SQLGenerator
    |
    +-> ExecutionEngine
```

or viewed horizontally:

```
AST -> LogicalPlan -> { Optimizer -> SQLGenerator | ExecutionEngine }
```

These extension points make it easy to insert an optimizer phase or switch to a runtime execution engine without altering the parser or lexer.  The diagrams highlight branching possibilities for future development.

---

## 7. Design Philosophy

This engine prioritizes:

- Clarity over premature optimization
- Explicit structure over magic
- Strong separation between syntax and semantics
- Compiler-style transformation pipeline

The goal is educational depth and architectural correctness.