namespace Core.Nodes;

public class LeftOuterJoinNode : OuterJoinNode
{
    
    public LeftOuterJoinNode(ExpressionNode left,ExpressionNode right)
    {
        Left = left;
        Right = right;
    }
}
