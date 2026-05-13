namespace lexer.Models;

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = setValue(value);
    }

    private string setValue(string value)
    {
        if(Type==TokenType.NEQ){
            return  "<>";
        }
        return value;
    }
}