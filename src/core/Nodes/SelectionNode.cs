namespace core.Nodes;

public class SelectionNode : ExpressionNode
{
    public ConditionNode Condition { get; }
    public ExpressionNode Source { get; }

    public SelectionNode(ConditionNode condition, ExpressionNode source)
    {
        Condition = condition;
        Source = source;
    }
}
