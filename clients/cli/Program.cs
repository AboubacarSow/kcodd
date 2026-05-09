using engine.Services;

namespace cli;

class Program
{
    // ── Palette ────────────────────────────────────────────────────────────────
    static readonly ConsoleColor AccentPrimary   = ConsoleColor.Cyan;
    static readonly ConsoleColor AccentSecondary = ConsoleColor.DarkCyan;
    static readonly ConsoleColor TextDim         = ConsoleColor.DarkGray;
    static readonly ConsoleColor TextBright      = ConsoleColor.White;
    static readonly ConsoleColor SuccessColor    = ConsoleColor.Green;
    static readonly ConsoleColor ErrorColor      = ConsoleColor.Red;
    static readonly ConsoleColor SqlColor        = ConsoleColor.Yellow;
    static readonly ConsoleColor PromptColor     = ConsoleColor.Cyan;

    static int ConsoleWidth => Math.Min(Console.WindowWidth > 0 ? Console.WindowWidth : 120, 80);

    // ── Entry point ────────────────────────────────────────────────────────────
    static void Main(string[] args)
    {
        Console.InputEncoding  = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible  = true;
        Console.ResetColor();
        Console.Clear();

        var service = new TranspilerService();

        PrintBanner();
        PrintHelp();

        while (true)
        {
            Console.WriteLine();
            WriteColored("  ┌─ ", TextDim);
            WriteColored("RA", AccentPrimary);
            WriteColored(" ─────────────────────────────────────", TextDim);
            Console.WriteLine();

            WriteColored("  │ ", TextDim);
            WriteColored("› ", PromptColor);

            Console.ForegroundColor = TextBright;
            string? input = Console.ReadLine();
            Console.ResetColor();

            WriteColored("  └", TextDim);
            WriteColored("─────────────────────────────────────────", TextDim);
            Console.ResetColor();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            string cmd = input.Trim().ToLower();

            if (cmd == "exit" || cmd == "quit" || cmd == ":q")
            {
                PrintGoodbye();
                break;
            }

            if (cmd == "help" || cmd == ":h")
            {
                PrintHelp();
                continue;
            }

            if (cmd == "clear" || cmd == "cls")
            {
                Console.Clear();
                PrintBanner();
                PrintHelp();
                continue;
            }

            try
            {
                string sql = service.Transpile(input);
                PrintSuccess(input, sql);
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }
    }

    // ── Banner ─────────────────────────────────────────────────────────────────
    static void PrintBanner()
    {
        int w = ConsoleWidth;
        string top    = "╔" + new string('═', w - 2) + "╗";
        string bottom = "╚" + new string('═', w - 2) + "╝";
        string blank  = "║" + new string(' ', w - 2) + "║";

        string title    = "RELATIONAL  ALGEBRA  ENGINE";
        string subtitle = "RA  →  SQL  Transpiler";

        Console.WriteLine();
        WriteLineColored(top, AccentPrimary);
        WriteLineColored(blank, AccentPrimary);

        // Title line
        WriteColored("║", AccentPrimary);
        int pad = (w - 2 - title.Length) / 2;
        WriteColored(new string(' ', pad), ConsoleColor.Black);
        WriteColored(title, TextBright);
        WriteColored(new string(' ', w - 2 - pad - title.Length), ConsoleColor.Black);
        WriteLineColored("║", AccentPrimary);

        // Subtitle line
        WriteColored("║", AccentPrimary);
        int pad2 = (w - 2 - subtitle.Length) / 2;
        WriteColored(new string(' ', pad2), ConsoleColor.Black);
        WriteColored(subtitle, AccentSecondary);
        WriteColored(new string(' ', w - 2 - pad2 - subtitle.Length), ConsoleColor.Black);
        WriteLineColored("║", AccentPrimary);

        WriteLineColored(blank, AccentPrimary);
        WriteLineColored(bottom, AccentPrimary);
        Console.WriteLine();

        // Operator chips
        WriteColored("  Operators  ", TextDim);
        string[] ops = ["π", "σ", "⋈", "ρ", "∧", "∨", "¬"];
        foreach (var op in ops)
        {
            WriteColored(" ", TextDim);
            WriteColored(op, AccentPrimary);
        }
        Console.WriteLine();
    }

    // ── Help ───────────────────────────────────────────────────────────────────
    static void PrintHelp()
    {
        Console.WriteLine();
        PrintPanel("COMMANDS", [
            ("help  / :h",  "Show this help"),
            ("clear / cls", "Clear the screen"),
            ("exit  / :q",  "Quit the program"),
        ]);
        Console.WriteLine();
        PrintPanel("EXAMPLE", [
            ("π name,age (σ age>18 (Student))", "Project & select"),
            ("Student ⋈ Enrolled",              "Natural join"),
            ("ρ S/Student (Student)",            "Rename relation"),
        ]);
    }

    static void PrintPanel(string heading, (string key, string val)[] rows)
    {
        int w = ConsoleWidth - 4;
        WriteColored("  ╭─ ", TextDim);
        WriteColored(heading, AccentPrimary);
        WriteColored(" " + new string('─', Math.Max(0, w - heading.Length - 3)) + "╮", TextDim);
        Console.WriteLine();

        foreach (var (k, v) in rows)
        {
            WriteColored("  │  ", TextDim);
            WriteColored($"{k,-30}", SuccessColor);
            WriteColored(v, TextDim);
            Console.WriteLine();
        }

        WriteColored("  ╰" + new string('─', w) + "╯", TextDim);
        Console.WriteLine();
    }

    // ── Success output ─────────────────────────────────────────────────────────
    static void PrintSuccess(string ra, string sql)
    {
        int w = ConsoleWidth - 4;

        // Input echo
        Console.WriteLine();
        WriteColored("  ┌─ ", TextDim);
        WriteColored("INPUT", TextDim);
        WriteColored(" " + new string('─', Math.Max(0, w - 7)) + "┐", TextDim);
        Console.WriteLine();

        WriteColored("  │  ", TextDim);
        WriteColored(ra, ConsoleColor.White);
        Console.WriteLine();

        WriteColored("  └" + new string('─', w) + "┘", TextDim);
        Console.WriteLine();

        // Arrow
        WriteColored("     ↓  transpiled", AccentSecondary);
        Console.WriteLine();

        // SQL output
        WriteColored("  ┌─ ", AccentPrimary);
        WriteColored("SQL RAW", AccentPrimary);
        WriteColored(" " + new string('─', Math.Max(0, w - 5)) + "┐", AccentPrimary);
        Console.WriteLine();

        // Word-wrap SQL inside the box
        var lines = WrapText(sql, w - 4);
        foreach (var line in lines)
        {
            WriteColored("  │  ", AccentPrimary);
            WriteColored(line, SqlColor);
            Console.WriteLine();
        }

        WriteColored("  └" + new string('─', w) + "┘", AccentPrimary);
        Console.WriteLine();
    }

    // ── Error output ───────────────────────────────────────────────────────────
    static void PrintError(string message)
    {
        int w = ConsoleWidth - 4;
        Console.WriteLine();

        WriteColored("  ┌─ ", ErrorColor);
        WriteColored("ERROR", ErrorColor);
        WriteColored(" " + new string('─', Math.Max(0, w - 7)) + "┐", ErrorColor);
        Console.WriteLine();

        var lines = WrapText(message, w - 4);
        foreach (var line in lines)
        {
            WriteColored("  │  ", ErrorColor);
            WriteColored(line, ConsoleColor.White);
            Console.WriteLine();
        }

        WriteColored("  └" + new string('─', w) + "┘", ErrorColor);
        Console.WriteLine();
    }

    // ── Goodbye ────────────────────────────────────────────────────────────────
    static void PrintGoodbye()
    {
        Console.WriteLine();
        WriteLineColored("  ─────────────────────────────────────────────────────────────", TextDim);
        WriteColored("  ", TextDim);
        WriteColored("\t\tGoodbye.", AccentSecondary);
        WriteColored("  Session ended.", TextDim);
        Console.WriteLine();
        WriteLineColored("  ─────────────────────────────────────────────────────────────", TextDim);
        Console.WriteLine();
    }

    // ── Helpers ────────────────────────────────────────────────────────────────
    static void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    static void WriteLineColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    static List<string> WrapText(string text, int maxWidth)
    {
        var result = new List<string>();
        if (maxWidth <= 0) { result.Add(text); return result; }

        while (text.Length > maxWidth)
        {
            int cut = text.LastIndexOf(' ', maxWidth);
            if (cut <= 0) cut = maxWidth;
            result.Add(text[..cut].TrimEnd());
            text = text[cut..].TrimStart();
        }
        if (text.Length > 0) result.Add(text);
        return result;
    }
}