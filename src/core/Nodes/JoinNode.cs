namespace core.Nodes;

public class JoinNode : ExpressionNode
{
    public ExpressionNode Left;
    public ExpressionNode Right;
    public JoinNode(ExpressionNode left, ExpressionNode right){
        Left = left;
        Right = right;
    }
}

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
