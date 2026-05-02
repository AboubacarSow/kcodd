namespace core.Nodes;

public class JoinNode : ExpressionNode
{
    public ExpressionNode Left { get; }
    public ExpressionNode Right { get; }
    public ConditionNode Condition { get; }

    public JoinNode(ExpressionNode left, ExpressionNode right, ConditionNode condition)
    {
        Left = left;
        Right = right;
        Condition = condition;
    }
}
