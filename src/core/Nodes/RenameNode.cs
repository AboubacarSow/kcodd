namespace core.Nodes;

public class RenameNode : ExpressionNode
{
    public string Alias { get; }
    public ExpressionNode Source { get; }

    public RenameNode(string alias, ExpressionNode source)
    {
        Alias = alias;
        Source = source;
    }
}
