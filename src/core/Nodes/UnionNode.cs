namespace core.Nodes;

public class UnionNode : ExpressionNode
{
    public ExpressionNode Left { get; }
    public ExpressionNode Right { get; }

    public UnionNode(ExpressionNode left, ExpressionNode right)
    {
        Left = left;
        Right = right;
    }
}
