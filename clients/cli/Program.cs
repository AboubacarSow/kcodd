using engine.Services;

namespace cli;

class Program
{
    static void Main(string[] args)
    {
        Console.ResetColor();
        var service = new TranspilerService();
        Console.WriteLine("\t\t╔══════════════════════════════════════╗");
        Console.WriteLine("\t\t║      Relational Algebra Engine       ║");
        Console.WriteLine("\t\t╚══════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("\t\tOperators: π σ ⋈ ρ ∧ ∨ ¬");
        Console.WriteLine("\t\tType 'exit' to quit.");
        Console.WriteLine();

        while (true)
        {
            Console.Write("RA > ");

            Console.ForegroundColor = ConsoleColor.Green;

            string? input = Console.ReadLine();
            Console.ResetColor();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input.ToLower() == "exit")
                break;

            try
            {
                string sql = service.Transpile(input);

                Console.WriteLine();
                Console.Write("SQL:");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(sql);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }
}