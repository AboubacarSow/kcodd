using Core.Nodes;
using LexerLayer;
using ParserLayer;
using SQLGenerator;
using TransformerLayer;

namespace TranspilerLayer.Services;

public class TranspilerService : ITranspilerService
{
    public string Transpile(string input)
    {
        var lexer = new Lexer(input);

        var parser = new Parser(lexer);

        var ast = parser.Parse();
        var sqlGenerator = new SqlGenerator();
        if( ast is SelectionNode s)
        {
           var new_ast = Transformer.Transform(s); 
           return sqlGenerator.GenerateSql(new_ast);
        }
        return sqlGenerator.GenerateSql(ast);
    }
}