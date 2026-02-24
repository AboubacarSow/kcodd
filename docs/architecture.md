# Relational Algebra → SQL Engine
## System Architecture

---

## 1. High-Level Pipeline

The system follows a classical compiler architecture:

Input Text
   ↓
Lexer
   ↓
Parser
   ↓
Abstract Syntax Tree (AST)
   ↓
Logical Plan
   ↓
SQL Generator
   ↓
SQL Output

Each stage transforms the representation into a higher semantic level.

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

Lexer → (none)
Parser → Lexer
AST → (none)
LogicalPlan → AST
SQLGenerator → LogicalPlan
CLI → All

The following dependencies are forbidden:

- SQLGenerator → Parser
- Parser → SQLGenerator
- LogicalPlan → CLI
- AST → SQLGenerator

These rules prevent tight coupling.

---

## 5. Error Handling Strategy

Two categories of errors:

1. Syntax Errors (Parser stage)
2. Semantic Errors (Logical Plan stage)

SQL generation occurs only after validation passes.

---

## 6. Future Extension Points

The architecture is designed to allow:

LogicalPlan → Optimizer → SQLGenerator
LogicalPlan → ExecutionEngine

This ensures future evolution without rewriting earlier layers.

---

## 7. Design Philosophy

This engine prioritizes:

- Clarity over premature optimization
- Explicit structure over magic
- Strong separation between syntax and semantics
- Compiler-style transformation pipeline

The goal is educational depth and architectural correctness.