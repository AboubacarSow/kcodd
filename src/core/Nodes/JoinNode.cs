namespace core.Nodes;

public class JoinNode : ExpressionNode
{
    public ExpressionNode Left{ get; }
    public ExpressionNode Right{get;}
    public JoinNode(ExpressionNode left, ExpressionNode right){
        Left = left;
        Right = right;
    }
}
