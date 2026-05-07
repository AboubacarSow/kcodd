
namespace tests;

public class E2ETest
{
  [Fact]
  public void Should_Generate_Correct_ProjectionSql()
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

    private string Normalize(string result)
    {
        return result
            .Replace("\r", "")
            .Replace("\n", "")
            .Replace("  ", " ")
            .Trim();
    }
}
