# RA CLI Playground

An interactive command-line playground for the **Relational Algebra (RA) Engine**. Test and experiment with relational algebra expressions and see them transpiled to SQL in real-time.

## Overview

The CLI playground provides a user-friendly REPL (Read-Eval-Print Loop) interface for interacting with the RA Engine. Enter relational algebra expressions using standard mathematical notation and instantly get SQL output.

## Getting Started

### Prerequisites

- .NET 8.0 or later
- The RA Engine transpiler service built and available

### Running the CLI

Navigate to the `playground/cli` directory and run:

```bash
dotnet run
```

The CLI will start with a welcome banner and display available commands.

## Usage

### Interactive Mode

Once started, the CLI presents a prompt where you can enter relational algebra expressions:

```
  ┌─ RA ─────────────────────────────────
  │ › 
```

Type your expression and press **Enter** to transpile it to SQL. The output will display both your input expression and the generated SQL query.

### Commands

| Command | Aliases | Description |
|---------|---------|-------------|
| `help` | `:h` | Display available commands and examples |
| `clear` | `cls` | Clear the screen |
| `exit` | `:q` | Exit the program |

## Operators

The CLI supports the following relational algebra operators:

| Operator | Unicode | Math Name | Example |
|----------|---------|-----------|---------|
| `π` | `\u03c0` | Projection | `π name,age (Student)` |
| `σ` | `\u03c3` | Selection | `σ age>18 (Student)` |
| `⋈` | `\u22c8` | Natural Join | `Student ⋈ Enrolled` |
| `⨝θ` | `\u2a1d\u03b8` | Theta Join | `Student ⨝θ age>18 (Enrolled)` |
| `ρ` | `\u03c1` | Rename | `ρ S/Student (Student)` |
| `∧` | `\u2227` | AND (Logical) | Combine conditions |
| `∨` | `\u2228` | OR (Logical) | Combine conditions |
| `¬` | `\u00ac` | NOT (Logical) | Negate conditions |

## Examples

### Basic Projection
```
π name,age (Student)
```
Selects only the `name` and `age` columns from the `Student` relation.

### Selection with Condition
```
σ age>18 (Student)
```
Filters students older than 18.

### Combined: Projection + Selection
```
π name,age (σ age>18 (Student))
```
Finds names and ages of adult students.

### Natural Join
```
Student ⋈ Enrolled
```
Joins the `Student` and `Enrolled` relations on common attributes.

### Theta Join
```
Student ⨝θ age>18 (Enrolled)
```
Joins the `Student` and `Enrolled` relations with a condition (students older than 18).

### Relation Rename
```
ρ S/Student (Student)
```
Renames the `Student` relation to `S`.

### Complex Expression
```
π name,grade (σ grade≥A (Student ⋈ (ρ Course/Courses (Courses))))
```
Advanced multi-step composition with projection, selection, and join.

## Output Format

The CLI provides color-coded output for easy readability:

- **Cyan**: Prompts and operator symbols
- **Green**: Successful transpilation results
- **Red**: Error messages
- **Yellow**: Generated SQL queries
- **Dark Gray**: Structural elements (borders, separators)

## Architecture

The CLI uses the `TranspilerService` from the core RA Engine to:

1. **Parse** the input relational algebra expression
2. **Validate** the expression syntax
3. **Optimize** the AST (Abstract Syntax Tree)
4. **Generate** SQL code

Errors are caught and displayed with descriptive messages to help debug invalid expressions.

## Tips & Tricks

- Use **parentheses** to control operation precedence
- **Whitespace** is flexible—expressions can be written with or without spaces
- Use **:h** for a quick help reminder
- Use **:q** to exit quickly
- Type **clear** to start fresh with a clean screen

## Debugging

If an expression fails to transpile:

1. Check the error message displayed
2. Verify operator symbols are correct
3. Ensure all relations and attributes are spelled correctly
4. Check that parentheses are balanced
5. Revisit the examples or help command

## See Also

- [RA Engine Documentation](../../docs/) — Architecture and design notes
- [Relational Algebra Grammar](../../src/grammar/) — Formal grammar specification
- [Web Playground](../webBlazor/) — Interactive web-based interface
