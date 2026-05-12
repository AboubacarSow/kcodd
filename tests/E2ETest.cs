using lexer.Models;
using parser.Models;
using sqlgenerator.Services;

namespace tests;

public class E2ETest
{
  [Fact]
  public void Should_Generate_Correct_ProjectionSql()
    {
        string input = "π name (Students)";
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);
        var ast = parser.Parse();
        var sql = new SqlGenerator();
        var result = sql.GenerateSql(ast);
        Assert.Equal(
            "SELECT name FROM Students",
            Normalize(result)
        );
    }

    [Fact]
    public void Union_Expression_Should_Generate_Correct_Sql()
    {
        string input = "Students ∪ Teachers";
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);
        var ast = parser.Parse();
        var sql = new SqlGenerator();
        var result = sql.GenerateSql(ast);
        Assert.Equal(
            "SELECT * FROM Students UNION SELECT * FROM Teachers",
            Normalize(result)
        );
    }

    [Fact]
    public void Intersection_Expression_Should_Generate_Correct_Sql()
    {
        string input = "Students ∩ Teachers";
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);
        var ast = parser.Parse();
        var sql = new SqlGenerator();
        var result = sql.GenerateSql(ast);
        Assert.Equal(
            "SELECT * FROM Students INTERSECT SELECT * FROM Teachers",
            Normalize(result)
        );
    }

    [Fact]
    public void ThetaJoin_Expression_Should_Generate_Correct_Sql()
    {
        string input = "Students ⨝θ[Students.id = Teachers.student_id] Teachers";
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);
        var ast = parser.Parse();
        var sql = new SqlGenerator();
        var result = sql.GenerateSql(ast);
        Assert.Equal(
            "SELECT * FROM Students JOIN Teachers ON (Students.id = 'Teachers.student_id')",
            Normalize(result)
        );
    }

    [Fact]
    public void Difference_Expression_Should_Generate_Correct_Sql()
    {
        string input = "Students - Teachers";
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);
        var ast = parser.Parse();
        var sql = new SqlGenerator();
        var result = sql.GenerateSql(ast);
        Assert.Equal(
            "SELECT * FROM Students EXCEPT SELECT * FROM Teachers",
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
