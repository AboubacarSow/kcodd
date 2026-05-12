namespace core.Nodes;

public class IntersectionNode : ExpressionNode
{
    public ExpressionNode Left { get; }
    public ExpressionNode Right { get; }

    public IntersectionNode(ExpressionNode left, ExpressionNode right)
    {
        Left = left;
        Right = right;
    }

}
