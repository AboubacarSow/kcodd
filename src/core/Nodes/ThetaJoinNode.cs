namespace core.Nodes;

public class ThetaJoinNode : ExpressionNode
{
    public ExpressionNode Left { get; }
    public ExpressionNode Right { get; }
    public ConditionNode Condition { get; }

    public ThetaJoinNode(ExpressionNode left, ExpressionNode right, ConditionNode condition)
    {
        Left = left;
        Right = right;
        Condition = condition;
    }
}
