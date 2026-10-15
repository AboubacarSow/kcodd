namespace Core.Nodes;

public class RightOuterJoinNode : OuterJoinNode
{
    
    public RightOuterJoinNode(ExpressionNode left,ExpressionNode right)
    {
        Right = right;
        Left = left;
    }
}
