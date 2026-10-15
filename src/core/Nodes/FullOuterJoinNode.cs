namespace Core.Nodes;

public class FullOuterJoinNode : OuterJoinNode
{
    public FullOuterJoinNode(ExpressionNode left,ExpressionNode right)
    {
        Left = left;
        Right = right;
    }
}