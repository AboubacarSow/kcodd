using lexer.Models;
using parser.Models;
using sqlgenerator.Services;

namespace Tests;

public class EndToEndTest
{
    [Fact]
    public void Should_Generate_Correct_Sql()
    {
        string input = "π name (Students)";

        var lexer = new Lexer(input);
        var parser = new Parser(lexer);

        var ast = parser.ParseExpression();
        var sql = new SqlGenerator();
        var result = sql.GenerateSql(ast);
        

        Assert.Equal(
            "SELECT name FROM Students",
            Normalize(result)
        );
    }

    private string Normalize(string  sql)
    {
        return sql
            .Replace("\r", "")
            .Replace("\n", "")
            .Replace("  ", " ")
            .Trim();
    }
}