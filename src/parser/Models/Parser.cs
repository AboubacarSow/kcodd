using core.Nodes;
using lexer.Models;


namespace parser.Models;

public class Parser
{
    private readonly Lexer _lexer;
    private Token _current;

    public Parser(Lexer lexer)
    {
        _lexer = lexer;
        _current = _lexer.NextToken();
    }

    private void Eat(TokenType type)
    {
        if (_current.Type == type)
            _current = _lexer.NextToken();
        else
            throw new Exception($"Expected {type}, got {_current.Type}");
    }

    public ExpressionNode ParseExpression()
    {
        return _current.Type switch
        {
            TokenType.PROJECT => ParseProjection(),
            TokenType.SELECT => ParseSelection(),
            TokenType.JOIN => ParseJoin(),
            TokenType.RENAME => ParseRename(),
            TokenType.LPAREN => ParseParenthesized(),
            TokenType.IDENTIFIER => ParseRelation(),
            _ => throw new Exception($"Unexpected token: {_current.Type}")
        };
    }

    private ExpressionNode ParseRename()
    {
        Eat(TokenType.RENAME);

        var alias = _current.Value;

        Eat(TokenType.IDENTIFIER);

        Eat(TokenType.LPAREN);
        var source = ParseExpression();
        Eat(TokenType.RPAREN);

        return new RenameNode(alias,source);
    }
    private ExpressionNode ParseProjection()
    {
        Eat(TokenType.PROJECT);

        var attributes = ParseAttributeList();

        Eat(TokenType.LPAREN);
        var source = ParseExpression();
        Eat(TokenType.RPAREN);

        return new ProjectionNode(attributes, source);
    }

    private List<string> ParseAttributeList()
    {
        var list = new List<string>
        {
            _current.Value
        };
        Eat(TokenType.IDENTIFIER);

        while (_current.Type == TokenType.COMMA)
        {
            Eat(TokenType.COMMA);
            list.Add(_current.Value);
            Eat(TokenType.IDENTIFIER);
        }

        return list;
    }

    private ExpressionNode ParseRelation()
    {
        string name = _current.Value;
        Eat(TokenType.IDENTIFIER);

        return new RelationNode(name);
    }

    private ExpressionNode ParseParenthesized()
    {
        Eat(TokenType.LPAREN);
        var expr = ParseExpression();
        Eat(TokenType.RPAREN);

        return expr;
    }

    private ExpressionNode ParseSelection()
    {
        Eat(TokenType.SELECT);

        var condition = ParseCondition();

        Eat(TokenType.LPAREN);
        var source = ParseExpression();
        Eat(TokenType.RPAREN);

        return new SelectionNode(condition, source);
    }

    private ConditionNode ParsePrimaryCondition()
    {
        if (_current.Type == TokenType.LPAREN)
        {
            Eat(TokenType.LPAREN);
            var condition = ParseCondition();
            Eat(TokenType.RPAREN);

            return condition;
        }

        return ParseComparison();
    }



    private ExpressionNode ParseJoin()
    {
        Eat(TokenType.JOIN);

        Eat(TokenType.LPAREN);

        var left = ParseExpression();

        Eat(TokenType.COMMA);

        var right = ParseExpression();

        Eat(TokenType.COMMA);

        var condition = ParseCondition();

        Eat(TokenType.RPAREN);

        return new JoinNode(left, right, condition);
    }



    private ConditionNode ParseComparison()
    {
        string left = _current.Value;
        Eat(TokenType.IDENTIFIER);

        TokenType opType = _current.Type;

        if (opType != TokenType.EQ &&
            opType != TokenType.NEQ &&
            opType != TokenType.LT &&
            opType != TokenType.GT &&
            opType != TokenType.LTE &&
            opType != TokenType.GTE)
        {
            throw new Exception($"Invalid comparator: {_current.Type}");
        }

        string op = _current.Value;
        Eat(opType);

        string right = _current.Value;

        if (_current.Type == TokenType.STRING)
            Eat(TokenType.STRING);
        else if (_current.Type == TokenType.NUMBER)
            Eat(TokenType.NUMBER);
        else
            throw new Exception("Expected literal");

        return new ComparisonNode(left, op, right);
    }



    private ConditionNode ParseCondition()
    {
        return ParseDisjunction();
    }



    private ConditionNode ParseDisjunction()
    {
        var left = ParseConjunction();

        while (_current.Type == TokenType.OR)
        {
            Eat(TokenType.OR);
            var right = ParseConjunction();

            left = new OrNode(left, right);
        }

        return left;
    }



    private ConditionNode ParseConjunction()
    {
        var left = ParseNegation();

        while (_current.Type == TokenType.AND)
        {
            Eat(TokenType.AND);
            var right = ParseNegation();

            left = new AndNode(left, right);
        }

        return left;
    }



    private ConditionNode ParseNegation()
    {
        if (_current.Type == TokenType.NOT)
        {
            Eat(TokenType.NOT);
            var inner = ParseNegation();

            return new NotNode(inner);
        }

        return ParsePrimaryCondition();
    }


}