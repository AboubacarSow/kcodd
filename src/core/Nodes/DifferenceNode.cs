namespace core.Nodes;

public class DifferenceNode: ExpressionNode
{
    public ExpressionNode Left;
    public ExpressionNode Right;

    public DifferenceNode ( ExpressionNode left, ExpressionNode right)
    {
        Left = left;
        Right = right;
    }
}
