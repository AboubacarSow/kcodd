using lexer.Models;
using parser.Models;
using sqlgenerator.Services;

namespace transpiler.Services;

public interface ITranspilerService
{
    string Transpile(string input);
}

public class TranspilerService : ITranspilerService
{
    public string Transpile(string input)
    {
        var lexer = new Lexer(input);

        var parser = new Parser(lexer);

        var ast = parser.Parse();
        var show_ast = ast;
        var sqlGenerator = new SqlGenerator();
        return sqlGenerator.GenerateSql(ast);
    }
}