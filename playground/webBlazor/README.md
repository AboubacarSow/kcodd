# RA Web Playground

A modern web-based interface for the **Relational Algebra (RA) Engine**. Experience the power of relational algebra transpilation through an interactive web application built with Blazor Server.

## Overview

The web playground provides a user-friendly web interface for experimenting with relational algebra expressions. Featuring a clean, responsive design with dark/light theme support, it offers an intuitive way to write RA expressions and instantly see their SQL equivalents.

## Features

- **Interactive Compiler**: Real-time transpilation of RA expressions to SQL
- **Visual Operator Palette**: Click-to-insert operators and symbols
- **Code Snippets**: Pre-built examples for common operations
- **Expression History**: Track and reuse previous expressions
- **Copy to Clipboard**: Easily copy generated SQL queries
- **Theme Support**: Toggle between light and dark modes
- **Keyboard Shortcuts**: Ctrl+Enter to run expressions
- **Responsive Design**: Works on desktop and mobile devices
- **Performance Metrics**: View transpilation timing information

## Getting Started

### Prerequisites

- .NET 8.0 or later
- The RA Engine transpiler service built and available

### Running the Web Playground

Navigate to the `playground/webBlazor` directory and run:

```bash
dotnet run
```

The application will start and be available at `https://localhost:5001` (or the configured port).

### Alternative: Run from Solution

You can also run it from the root solution directory:

```bash
dotnet run --project playground/webBlazor/webBlazor.csproj
```

## Interface Overview

### Home Page

The landing page introduces the RA Engine with:
- **Logo and branding** for the RA Engine
- **Operator showcase** displaying available RA operators
- **Call-to-action** button to access the compiler

### Compiler Page

The main transpilation interface consists of:

#### Navigation Bar
- **Brand link** to return home
- **Status indicator** showing engine readiness
- **Theme toggle** button (sun/moon icon)
- **Back to home** link

#### Sidebar
- **Operators Grid**: Clickable operator buttons for easy insertion
- **Comparison Operators**: Common comparison symbols (=, >, <, ≥, ≤, ≠)
- **Code Snippets**: Pre-built RA expressions with descriptions
- **Expression History**: Recently used expressions (up to 8 items)

#### Main Panel

##### Input Area
- **Expression textarea**: Multi-line input for RA expressions
- **Run button**: Primary action to transpile (disabled when empty)
- **Clear button**: Reset input and results
- **Keyboard hint**: Ctrl+Enter shortcut reminder

##### Output Area
- **Results list**: Chronological display of transpilation results
- **Result cards** showing:
  - Status (SQL generated / Error)
  - Timestamp and elapsed time
  - Original RA expression
  - Generated SQL or error message
  - Copy button for successful SQL generation

## Operators

The web playground supports the same relational algebra operators as the CLI:

| Operator | Name | Description |
|----------|------|-------------|
| `σ` | Select | Filter rows with conditions |
| `π` | Project | Select specific columns |
| `ρ` | Rename | Rename relations or attributes |
| `⨝` | Natural Join | Join on common attributes |
| `⨝θ` | Theta Join | Join with custom conditions |
| `∧` | AND | Logical conjunction |
| `∨` | OR | Logical disjunction |
| `(` | Left Parenthesis | Group expressions |
| `)` | Right Parenthesis | Group expressions |

### Comparison Operators
- `=` (equals)
- `>` (greater than)
- `<` (less than)
- `≥` (greater than or equal)
- `≤` (less than or equal)
- `≠` (not equal)

## Usage Examples

### Basic Selection
```
σ age > 18 (Student)
```
Filter students older than 18.

### Projection
```
π name, age (Student)
```
Select only name and age columns from Student.

### Natural Join
```
Student ⨝ Enrolled
```
Join Student and Enrolled relations on common attributes.

### Theta Join
```
Student ⨝θ age > 18 (Enrolled)
```
Join with a specific condition.

### Complex Expression
```
π name, grade (σ grade ≥ A ∧ age > 18 (Student ⨝ Enrolled))
```
Multi-step operation combining projection, selection, and join.

## Keyboard Shortcuts

- **Ctrl + Enter**: Run the current expression
- **Tab**: Navigate between interface elements
- **Enter**: Activate buttons and insert operators/snippets

## Features in Detail

### Real-time Transpilation
- Expressions are transpiled server-side using the RA Engine
- Results appear instantly with timing information
- Errors are clearly displayed with descriptive messages

### Expression History
- Automatically saves recent expressions
- Click any history item to reuse it
- Maintains up to 8 recent expressions

### Copy Functionality
- One-click copying of generated SQL
- Visual feedback when SQL is copied
- Works with standard clipboard operations

### Theme Support
- Automatic system preference detection
- Manual toggle between light and dark modes
- Persistent theme preference in browser storage

### Responsive Design
- Adapts to different screen sizes
- Touch-friendly interface for mobile devices
- Optimized layouts for tablets and desktops

## Architecture

The web playground is built with:

- **Blazor Server**: Real-time web UI framework
- **Server-side rendering**: Interactive components with SignalR
- **Dependency injection**: Scoped TranspilerService
- **JavaScript interop**: Theme persistence and clipboard operations
- **CSS styling**: Custom responsive design with retro aesthetic

### Key Components

- **App.razor**: Root component with HTML shell
- **Routes.razor**: Client-side routing configuration
- **Home.razor**: Landing page component
- **Compiler.razor**: Main transpilation interface
- **Tools.cs**: Operator and snippet definitions
- **Result.cs**: Data transfer object for results

## Development

### Project Structure
```
webBlazor/
├── Components/
│   ├── App.razor          # Root component
│   ├── Routes.razor       # Routing configuration
│   └── Pages/
│       ├── Home.razor     # Landing page
│       └── Compiler.razor # Main compiler interface
├── Utils/
│   ├── Op.cs             # Operator record
│   ├── Snippet.cs        # Snippet record
│   └── Tools.cs          # Static operator/snippet data
├── Dtos/
│   └── Result.cs         # Result data transfer object
└── wwwroot/
    ├── css/              # Stylesheets
    └── js/               # JavaScript utilities
```

### Building and Running

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run in development mode
dotnet run --environment Development

# Publish for production
dotnet publish -c Release
```

## Browser Support

- **Chrome/Edge**: Full support with all features
- **Firefox**: Full support
- **Safari**: Full support (including iOS)
- **Mobile browsers**: Responsive design optimized

## Troubleshooting

### Common Issues

**Expression not transpiling:**
- Check for syntax errors in the RA expression
- Ensure all operators and parentheses are balanced
- Verify relation and attribute names are spelled correctly

**Theme not persisting:**
- Check browser localStorage support
- Clear browser cache if issues persist

**Slow performance:**
- Large expressions may take longer to transpile
- Check network connection for Blazor Server

**Mobile interface issues:**
- Ensure viewport meta tag is set correctly
- Try refreshing the page

## See Also

- [RA Engine Documentation](../../docs/) — Architecture and design notes
- [CLI Playground](../cli/) — Command-line interface
- [Relational Algebra Grammar](../../src/grammar/) — Formal grammar specification
- [Core Engine](../../src/) — Transpiler implementation