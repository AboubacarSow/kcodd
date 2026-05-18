using transpiler.Services;

namespace cli;

class Program
{
    // ── Palette ────────────────────────────────────────────────────────────────
    static readonly ConsoleColor Dim     = ConsoleColor.DarkGray;
    static readonly ConsoleColor Bright  = ConsoleColor.White;
    static readonly ConsoleColor Muted   = ConsoleColor.Gray;
    static readonly ConsoleColor Error   = ConsoleColor.Red;
    static readonly ConsoleColor Sql     = ConsoleColor.DarkGray;
    static readonly ConsoleColor Keyword = ConsoleColor.DarkGreen;
    static readonly ConsoleColor Prompt  = ConsoleColor.DarkYellow;
    static readonly ConsoleColor Version = ConsoleColor.Yellow;

    static int W => Math.Min(Console.WindowWidth > 0 ? Console.WindowWidth : 120, 82);

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
            Write("  ", Dim);
            Write("›  ", Prompt);

            Console.ForegroundColor = Bright;
            string? input = Console.ReadLine();
            Console.ResetColor();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            string cmd = input.Trim().ToLower();

            if (cmd is "exit" or "quit" or ":q")
            {
                PrintGoodbye();
                break;
            }

            if (cmd is "help" or ":h")
            {
                PrintHelp();
                continue;
            }

            if (cmd is "clear" or "cls")
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
        Console.WriteLine();

        // ASCII diamond mark — mirrors the KCodd lattice logo
        //       ◆
        //     ◇   ◇
        //   ◇   ●   ◇
        //     ◇   ◇
        //       ◆
        string[] diamond =
        [
            "          ◆          ",
            "        ◇   ◇        ",
            "      ◇   ●   ◇      ",
            "        ◇   ◇        ",
            "          ◆          ",
        ];

        foreach (var line in diamond)
        {
            Write("  ", Dim);
            Write(line+ "                                                     ", Dim);
            WriteLine(line, Dim);

        }


        Console.WriteLine();

        // Wordmark
        int pad = (W - 5) / 2;
        Write(new string(' ', pad), Dim);
        Write("KCodd", Bright);
        WriteLine(" v1.0.0", Version);
        Console.WriteLine();

        // Tagline
        string tag = "Kern · Codd · RA → SQL";
        int tagPad = (W - tag.Length) / 2;
        Write(new string(' ', tagPad), Dim);
        WriteLine(tag, Dim);

        Console.WriteLine();
        Write("  " + new string('─', W - 4), Dim);
        Console.WriteLine();
        Console.WriteLine();

        // Operator strip
        Write("  operators  ", Dim);
        string[] ops = ["σ", "π", "ρ", "⨝", "⨝θ", "∧", "∨", "¬", "∪", "∩", "-"];
        foreach (var op in ops)
        {
            Write(" ", Dim);
            Write(op, Muted);
        }
        Console.WriteLine();
    }

    // ── Help ───────────────────────────────────────────────────────────────────
    static void PrintHelp()
    {
        Console.WriteLine();
        PrintPanel("commands", [
            ("help  / :h",  "show this help"),
            ("clear / cls", "clear the screen"),
            ("exit  / :q",  "quit"),
        ]);
        Console.WriteLine();
        PrintPanel("examples", [
            ("σ [age > 18] (Student)",                            "selection"),
            ("π [name, gpa] (Student)",                           "projection"),
            ("σ [age > 18 ∧ gpa >= 3] (Student)",                 "compound condition"),
            ("Student ⨝ Enrolled",                                "natural join"),
            ("Student ⨝θ [Student.id = Enrolled.sid] (Enrolled)", "theta join"),
            ("ρ S (Student)",                                     "rename"),
        ]);
    }

    static void PrintPanel(string heading, (string key, string val)[] rows)
    {
        int w = W - 4;

        Write("  ", Dim);
        Write(heading, Muted);
        WriteLine("  " + new string('·', Math.Max(0, w - heading.Length - 2)), Dim);
        Console.WriteLine();

        foreach (var (k, v) in rows)
        {
            Write("    ", Dim);
            Write($"{k,-46}", Muted);
            WriteLine(v, Dim);
        }

        Console.WriteLine();
        Write("  " + new string('─', w), Dim);
        Console.WriteLine();
    }

    // ── Success output ─────────────────────────────────────────────────────────
    static void PrintSuccess(string ra, string sql)
    {
        int w = W - 4;

        Console.WriteLine();

        // Input echo
        Write("  in   ", Dim);
        WriteLine(ra, Prompt);

        // Divider + arrow
        Console.WriteLine();
        Write("  " + new string('·', w), Dim);
        Console.WriteLine();
        Write("  ↓  transpiled", Dim);
        Console.WriteLine();
        Write("  " + new string('·', w), Dim);
        Console.WriteLine();
        Console.WriteLine();

        // SQL output (multi-line wrapped, aligned)
        Write("  out  ", Dim);
        var lines = WrapText(sql, w - 7);
        for (int i = 0; i < lines.Count; i++)
        {
            if (i == 0)
                WriteSql(lines[i], Sql);
            else
            {
                Write("       ", Dim);
                WriteSql(lines[i], Sql);
            }
        }

        Console.WriteLine();
        Write("  " + new string('─', w), Dim);
        Console.WriteLine();
    }

    // ── Error output ───────────────────────────────────────────────────────────
    static void PrintError(string message)
    {
        int w = W - 4;

        Console.WriteLine();
        Write("  error  ", Error);

        var lines = WrapText(message, w - 9);
        for (int i = 0; i < lines.Count; i++)
        {
            if (i == 0)
                WriteLine(lines[i], Muted);
            else
            {
                Write("         ", Dim);
                WriteLine(lines[i], Muted);
            }
        }

        Console.WriteLine();
        Write("  " + new string('─', w), Dim);
        Console.WriteLine();
    }

    // ── Goodbye ────────────────────────────────────────────────────────────────
    static void PrintGoodbye()
    {
        Console.WriteLine();
        Write("  " + new string('─', W - 4), Dim);
        Console.WriteLine();
        Console.WriteLine();

        string msg = "Goodbye.";
        int pad = (W - msg.Length) / 2;
        Write(new string(' ', pad), Dim);
        WriteLine(msg, Dim);

        Console.WriteLine();
        Write("  " + new string('─', W - 4), Dim);
        Console.WriteLine();
        Console.WriteLine();
    }

    // ── Helpers ────────────────────────────────────────────────────────────────
    static void Write(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    static void WriteLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    static void WriteSql(string text, ConsoleColor color)
    {
        var words = text.Split(' ');

        Console.ForegroundColor = color;
        foreach (var word in words)
        {
            
            if (SetOfKeywords.Contains(word.ToUpper()))
            {
                Console.ResetColor();
                Console.ForegroundColor = Keyword;
            }
            Console.Write($"{word} ");
            Console.ResetColor();
        }
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

    static readonly HashSet<string> SetOfKeywords = new(StringComparer.OrdinalIgnoreCase)
    {
        "SELECT", "FROM", "WHERE", "AND", "OR", "NOT", "JOIN", "ON", "AS",
        "INNER", "LEFT", "RIGHT", "FULL", "OUTER", "GROUP BY", "ORDER BY",
        "HAVING", "DISTINCT", "UNION", "ALL", "EXCEPT", "INTERSECT",
        "IN", "IS", "NULL", "LIKE", "BETWEEN"
    };
}