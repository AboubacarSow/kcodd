namespace Core.Nodes;

public abstract class OuterJoinNode : ExpressionNode 
{
    public ExpressionNode Right { get; protected set; } = default!;
    public ExpressionNode Left { get; protected set; } = default!;
}
