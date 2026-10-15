namespace LexerLayer.Models;

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = SetValue(value);
    }

    private string SetValue(string value)
    {
        if(Type==TokenType.NEQ){
            return  "<>";
        }
        return value;
    }
}