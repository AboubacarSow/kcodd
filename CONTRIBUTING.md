# Contributing to KCodd

Thank you for your interest in contributing to KCodd.

KCodd is an experimental and evolving relational algebra → SQL transpiler built with .NET and Blazor. Contributions are welcome — including bug fixes, improvements, refactoring, documentation updates, and new features.

---

## Project Structure

```text
kcodd/
├── src/
│   ├── core/           # AST node definitions and core logic
│   ├── lexer/          # Lexical analysis
│   ├── parser/         # Syntax parsing
│   ├── sqlgenerator/   # SQL code generation
│   ├── transpiler/     # Main transpilation service
│   └── grammar/        # Formal grammar definitions
│
├── playground/
│   ├── cli/            # Command-line playground
│   └── webBlazor/      # Blazor web playground
│
├── tests/              # Unit and integration tests
```

---

## Requirements

Before contributing, ensure you have:

- .NET 9 SDK or later
- Git
- Docker *(optional, for containerized development)*

---

## Getting Started

**Clone the repository**

```bash
git clone https://github.com/AboubacarSow/kcodd.git
cd kcodd
```

**Restore dependencies**

```bash
dotnet restore
```

**Run the web playground locally**

```bash
dotnet run --project playground/webBlazor
```

Open: `https://localhost:7200`

**Run the CLI playground**

```bash
dotnet run --project playground/cli
```

---

## Development Workflow

### Branching Strategy

| Branch | Purpose |
|---|---|
| `main` | Stable, production-ready |
| `dev` | Active development |
| `feature/<name>` | Isolated feature work |
| `fix/<name>` | Bug fixes |

**Examples:**

```text
feature/outer-joins
feature/ast-visualization
fix/parser-null-exception
```

---

## Testing

KCodd has a dedicated test project at the root of the solution.

**Please write tests for any feature you add or bug you fix before opening a pull request.**

```bash
dotnet test tests/tests.csproj
```

This applies to:

- New operators or transpilation logic
- Parser and lexer changes
- Bug fixes — add a regression test that would have caught the issue

---

## Pull Request Guidelines

Before opening a pull request:

- Ensure the solution builds successfully (`dotnet build kcodd.sln`)
- Run all tests and confirm they pass
- Keep pull requests focused on a single concern
- Avoid unrelated refactoring in the same PR
- Add or update documentation where relevant

For UI-related changes:

- Include screenshots when possible

---

## Coding Standards

### General

- Prefer clear and maintainable code over clever code
- Keep methods focused and small
- Use meaningful naming

### C#

- Follow standard .NET naming conventions
- Use dependency injection where appropriate
- Prefer async APIs when applicable

### Blazor

- Keep UI components modular
- Separate UI concerns from business logic

---

## Commit Convention

This project follows a lightweight conventional commit style:

```text
feat: add outer join support
fix: resolve parser null exception on nested expressions
docs: update contributing guide
refactor: simplify tokenization logic
test: add regression test for theta join edge case
```

---

## Reporting Issues

When opening an issue, please include:

- A clear description of the problem
- Steps to reproduce
- Expected behavior
- Actual behavior
- Screenshots or logs if relevant

---

## Discussions & Ideas

Suggestions and architectural discussions are welcome. Please open an issue before implementing large changes or new subsystems — it helps align effort and avoid duplicate work.

---

## License

By contributing, you agree that your contributions will be licensed under the [MIT License](LICENSE).