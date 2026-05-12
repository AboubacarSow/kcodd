namespace core.Nodes;

public class NaturalJoinNode : ExpressionNode
{
    public string Left { get; }
    public string Right { get; }

    public NaturalJoinNode(string left, string right)
    {
        Left = left;
        Right = right;
    }
}
