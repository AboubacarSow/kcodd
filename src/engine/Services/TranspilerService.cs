using lexer.Models;
using parser.Models;
using sqlgenerator.Services;

namespace engine.Services;

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
        var ast = parser.ParseExpression();
        var sqlGenerator = new SqlGenerator();
        return sqlGenerator.GenerateSql(ast);
    }
}